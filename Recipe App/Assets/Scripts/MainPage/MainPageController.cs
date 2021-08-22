using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPageController : MonoBehaviour
{
    public Recipe selectedRecipe;

    [SerializeField]
    private RecipeDropdownController recipeDropdownController;    

    private void Awake()
    {
        recipeDropdownController.ValueSelected += OnRecipeSelected;
    }

    private void OnRecipeSelected(Recipe recipe)
    {
        selectedRecipe = recipe;
        SceneChanger.AddScene("Recipe Page");
    }
}
