using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoosterPack")]
public class BoosterPackData : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private string name;
    [SerializeField] private int cost;
    [SerializeField] private List<BoosterPack> boosterPackContent;

}

[Serializable]
public struct BoosterPack
{
    public CardData card;
    public int dropChance;
}
