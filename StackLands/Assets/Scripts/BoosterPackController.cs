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
    private Transform _spawnCardsPoint;
    
    [Inject]
    private void Construct(CardsFactory cardsFactory)
    {
        _cardsFactory = cardsFactory;
    }

    public void SetBoosterData(BoosterPackData boosterPackData, Transform spawnCardsPoint)
    {
        _boosterPackData = boosterPackData;
        _boosterIcon.sprite = boosterPackData.icon;
        _spawnCardsPoint = spawnCardsPoint;
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
        newCard.transform.position = SpawnAroundService.GetNextPosition(_boosterPackData.boosterPackContent.Count,
            _counter, transform.position, 3f);
        newCard.SetCard(currentCardData.icon, currentCardData.cost, currentCardData.type, _spawnCardsPoint);
        
        _counter++;
    }
    
    public void OnDrag() { }

    public void OnDragEnd() { }
}

public class BoostersPackFactory : PlaceholderFactory<BoosterPackController>
{
    
}
