using System;
using System.Collections;
using System.Collections.Generic;
using GoogleSheetsToUnity;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class GoogleSheetsReader : MonoBehaviour
{
    [SerializeField] string associatedSheet;
    [SerializeField] string associatedWorksheet;
    [SerializeField] private AllCardsConfig allCardsConfig;

    private RecipeController _recipeController;
    
    [Inject]
    private void Construct(RecipeController recipeController)
    {
        _recipeController = recipeController;
    }
    
    private void Start()
    {
        UpdateStats();
    }

    void UpdateStats()
    {
        SpreadsheetManager.Read(new GSTU_Search(associatedSheet, associatedWorksheet), ReadFromSheet);
    }

    void ReadFromSheet(GstuSpreadSheet ss)
    {
        foreach (var cell in ss.Cells)
        {
            if(cell.Value.Row() <= 1) 
                continue;
            
            _recipeController.AddValuesToRecipe(cell.Value.Row(), cell.Value.value);
        }
        _recipeController.ConvertGotValuesToRecipeFormat(allCardsConfig);
    }

    
}
