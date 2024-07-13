using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData")]
public class AllCardsConfig : ScriptableObject
{
    public List<CardData> cards = new();

    public CardType GetCardByName(string name)
    {
        return cards.FirstOrDefault(x => x.name == name)!.type;
    }
    
    public CardData GetCardByType(CardType cardType)
    {
        return cards.FirstOrDefault(x => x.type == cardType);
    }
}
