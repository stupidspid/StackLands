using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Zenject;

public class CardsSpawnPoint : MonoBehaviour
{
    [SerializeField] private AllCardsConfig _allCardsConfig;

    private StackController _stackController;
    private CardsFactory _cardsFactory;
    
    [Inject]
    private void Construct(StackController stackController, CardsFactory cardsFactory)
    {
        _stackController = stackController;
        _cardsFactory = cardsFactory;
    }
    
    private void Start()
    {
        _stackController.OnCardCreate += CreateCard;
    }

    private void CreateCard(CardType cardType, Transform spawnPoint)
    {
        var newCard = _cardsFactory.Create();
        var currentCard = _allCardsConfig.GetCardByType(cardType);
        newCard.transform.position = SpawnAroundService.GetNextPosition(1,
            1, spawnPoint.position, 2f);
        newCard.SetCard(currentCard.icon, currentCard.cost, currentCard.type, transform);
    }
}
