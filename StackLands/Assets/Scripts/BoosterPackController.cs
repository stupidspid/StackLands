using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BoosterPackController : MonoBehaviour, IDraggableItem
{
    [SerializeField] private SpriteRenderer _boosterIcon;
    
    private CardsFactory _cardsFactory;
    private int _counter;
    private BoosterPackData _boosterPackData;
    
    [Inject]
    private void Construct(CardsFactory cardsFactory)
    {
        _cardsFactory = cardsFactory;
    }

    public void SetBoosterData(BoosterPackData boosterPackData)
    {
        _boosterPackData = boosterPackData;
        _boosterIcon.sprite = boosterPackData.icon;
    }
    
    public void OnClick()
    {
        if (_boosterPackData.boosterPackContent.Count <= _counter)
        {
            Destroy(gameObject);
            return;
        }
        
        var newCard = _cardsFactory.Create();
        var currentCardData = _boosterPackData.boosterPackContent[_counter].card;
        newCard.SetCard(currentCardData.icon, currentCardData.cost, currentCardData.type);
        
        _counter++;
    }
}

public class BoostersPackFactory : PlaceholderFactory<BoosterPackController>
{
    
}
