using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ScenarioController : MonoBehaviour
{
    [SerializeField] private BoosterPackData _boosterPackData;
    private BoostersPackFactory _boostersPackFactory;
    
    [Inject]
    private void Construct(BoostersPackFactory boostersPackFactory)
    {
        _boostersPackFactory = boostersPackFactory;
    }
    private void Start()
    {
        var boostersPack = _boostersPackFactory.Create();
        boostersPack.SetBoosterData(_boosterPackData);
    }
}
