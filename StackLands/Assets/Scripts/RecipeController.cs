using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using ModestTree;
using UnityEngine;
using Zenject;

public class RecipeController
{
    private Dictionary<int, List<string>> newRecipe = new();
    private StackController _stackController;
    
    [Inject]
    private void Construct(StackController stackController)
    {
        _stackController = stackController;
    }
    
    public void AddValuesToRecipe(int rowNum, string rowValue)
    {
        if (!newRecipe.ContainsKey(rowNum))
        {
            newRecipe[rowNum] = new();
        }
        newRecipe[rowNum].Add(rowValue);
    }

    public void ConvertGotValuesToRecipeFormat(AllCardsConfig allCardsConfig)
    {
        foreach (var recipe in newRecipe)
        {
            List<CardType> recipeConsistent = new();
            for (int i = 0; i < WC.maxRecipeIngredientsCount; i++)
            {
                if(recipe.Value[i].IsEmpty())
                    continue;

                recipeConsistent.Add(allCardsConfig.GetCardByName(recipe.Value[i]));
            }
            
            int nextValue = WC.maxRecipeIngredientsCount;
            float craftTime = float.Parse(recipe.Value[nextValue]);
            nextValue++;
            
            //float dropChance = float.Parse(recipe.Value[nextValue]);
            nextValue++;

            CardType result = allCardsConfig.GetCardByName(recipe.Value[nextValue]);
            _stackController.CreateNewRecipe(new Recipe(recipeConsistent, craftTime, result, 100));
        }
    }
}

public class Recipe
{
    public List<CardType> cards;
    public float spawnTime;
    public CardType result;
    public float spawnChance;

    public Recipe(List<CardType> cards, float spawnTime, CardType result, float spawnChance)
    {
        this.cards = cards;
        this.spawnTime = spawnTime;
        this.result = result;
        this.spawnChance = spawnChance;
    }
}
