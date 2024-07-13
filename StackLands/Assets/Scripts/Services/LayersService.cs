using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LayersService
{
    private List<CardController> registeredCards = new();
    public int GetMaxSortingOrder()
    {
        return registeredCards.Max(sr => sr.CardIcon.sortingOrder) + 1;
    }

    public int GetAvailableSortingOrder(Transform itemTransform)
    {
        int count = 1;
        Transform currentParent = itemTransform.parent;
        while(!currentParent.TryGetComponent<CardsSpawnPoint>(out var newParent))
        {
            currentParent = currentParent.parent;
            count++;
        }

        return count;
    }

    public void RegisterCard(CardController card)
    {
        registeredCards.Add(card);
    }
}
