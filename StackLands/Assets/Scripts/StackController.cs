using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using Zenject;

public class StackController
{
    private List<Recipe> recipes = new();

    public Action<CardType, Transform> OnCardCreate;
    
    public void CreateNewRecipe(Recipe recipe)
    {
        recipes.Add(recipe);
    }

    public void CheckIfRecipeExists(List<CardType> recipeCards, Transform parent)
    {
        foreach (var recipe in recipes)
        {
            recipe.cards.Sort();
            recipeCards.Sort();
            
            if (recipe.cards.SequenceEqual(recipeCards))
            {
                OnCardCreate?.Invoke(recipe.result, parent);
            }
        }
    }

   
}
