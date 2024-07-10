using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData")]
public class CardData : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private string name;
    [SerializeField] private int cost;
    [SerializeField] private int unitsOfFood;
    [SerializeField] private int healthPoints;
}
