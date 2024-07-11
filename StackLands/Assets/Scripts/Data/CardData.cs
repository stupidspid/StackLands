using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData")]
public class CardData : ScriptableObject
{
    [field:SerializeField] public Sprite icon { get; private set; }
    [field:SerializeField] public string name { get; private set; }
    [field:SerializeField] public int cost { get; private set; }
    [field:SerializeField] public int unitsOfFood { get; private set; }
    [field:SerializeField] public int healthPoints { get; private set; }
    [field:SerializeField] public CardType type { get; private set; }
}
