using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class CollisionService : MonoBehaviour
{
    [SerializeField] private float offsetBetweenCardsInStack;
    private CardController _cardController;
    private CardController _otherCard;
    private StackController _stackController;

    public CardController OtherCard
    {
        get => _otherCard;
        set => _otherCard = value;
    }
    
    [Inject]
    private void Construct(StackController stackController)
    {
        _stackController = stackController;
    }
    
    private void Start()
    {
        _cardController = GetComponent<CardController>();
        _cardController.OnDragEnded += SetNewPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<CardController>(out var card))
        {
            _otherCard = card;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _otherCard = null;
    }

    private void SetNewPosition()
    {
        if (_otherCard != null)
        {
            CardController newParent;
            CardController[] cardControllers = _otherCard.GetComponentsInChildren<CardController>();
            newParent = cardControllers.Last();
            
            var verticalOffset = _cardController.CardIcon.bounds.size.y * offsetBetweenCardsInStack;
            var newPosition = new Vector3(newParent.transform.position.x, 
                newParent.transform.position.y - verticalOffset, 0);
            
            transform.position = newPosition;
            transform.SetParent(newParent.transform);
            
            Transform currentParent = _otherCard.transform;
            CardController oldestParent = null;
            while (currentParent != null)
            {
                var parentComponent = currentParent.GetComponent<CardController>();
                if (parentComponent != null)
                {
                    oldestParent = parentComponent;
                }
                currentParent = currentParent.parent;
            }

            CardController[] stackCards = oldestParent.GetComponentsInChildren<CardController>();
            var cardTypesList = stackCards.Select(x => x.CardType).ToList();
            _stackController.CheckIfRecipeExists(cardTypesList, oldestParent.transform);
            
            foreach (var card in stackCards)
                card.transform.GetComponent<CollisionService>().OtherCard = null;
        }
    }

    private void OnDestroy()
    {
        _cardController.OnDragEnded -= SetNewPosition;
    }
}
