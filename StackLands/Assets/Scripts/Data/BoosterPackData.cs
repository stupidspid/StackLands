using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoosterPack")]
public class BoosterPackData : ScriptableObject
{
    [field:SerializeField] public Sprite icon { get; private set; }
    [field:SerializeField] public string name { get; private set; }
    [field:SerializeField] public int cost { get; private set; }
    public List<BoosterPack> boosterPackContent;

}

[Serializable]
public struct BoosterPack
{
    public CardData card;
    public int dropChance;
    public BoosterPackType boosterPackType;
}

public enum BoosterPackType
{
    DEFAULT = 0,
    TUTORIAL = 1,
}
