using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPageController : MonoBehaviour
{
    [SerializeField]
    private RecipeDropdownController recipeDropdownController;    

    private void Awake()
    {
        recipeDropdownController.ValueSelected += OnRecipeSelected;
    }

    private void OnRecipeSelected(Recipe recipe)
    {
        RecipeManager.CurrentRecipe = recipe;
        SceneChanger.AddScene("Recipe Page");
    }

    public void OnRandomRecipeButtonSelected()
    {
        RecipeManager.CurrentRecipe = ApiCommunicator.MakeRequest<Recipe>("recipeRandom");
        SceneChanger.AddScene("Recipe Page");
    }
}
