using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CardController : MonoBehaviour, IDraggableItem
{
    [SerializeField] private SpriteRenderer _cardIcon;
    [SerializeField] private TextMeshPro _cardCost;

    private CardType _cardType;
    
    public void SetCard(Sprite cardIcon, int cost, CardType cardType)
    {
        _cardType = cardType;
        _cardIcon.sprite = cardIcon;
        _cardCost.text = cost.ToString();
    }
    
    public void OnClick()
    {
        
    }
}

public class CardsFactory : PlaceholderFactory<CardController>
{
    
}
