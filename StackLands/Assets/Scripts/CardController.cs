using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class CardController : MonoBehaviour, IDraggableItem
{
    [field: SerializeField] public SpriteRenderer CardIcon { get; private set; }
    [SerializeField] private TextMeshPro _cardCost;

    private CardType _cardType;
    private LayersService _layersService;
    private Transform _startSpawnPoint;
    
    [Inject]
    private void Construct(LayersService layersService)
    {
        _layersService = layersService;
    }

    public Action OnDragEnded;
    
    public void SetCard(Sprite cardIcon, int cost, CardType cardType, Transform spawnPoint)
    {
        _cardType = cardType;
        CardIcon.sprite = cardIcon;
        _cardCost.text = cost.ToString();
        transform.SetParent(spawnPoint);
        _startSpawnPoint = spawnPoint;
    }
    
    public void OnClick() { }

    public void OnDrag()
    {
        transform.SetParent(_startSpawnPoint);
        CardIcon.sortingOrder = _layersService.GetMaxSortingOrder();
    }

    public void OnDragEnd()
    {
        OnDragEnded?.Invoke();
        CardIcon.sortingOrder = _layersService.GetAvailableSortingOrder(transform);
    }
}

public class CardsFactory : PlaceholderFactory<CardController>
{
    private LayersService _layersService;
    
    [Inject]
    private void Construct(LayersService layersService)
    {
        _layersService = layersService;
    }
    
    public override CardController Create()
    {
        var newCard = base.Create();
        _layersService.RegisterCard(newCard);
        return newCard;
    }
}
