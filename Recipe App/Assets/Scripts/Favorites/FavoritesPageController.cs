using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FavoritesPageController : MonoBehaviour
{    
    [SerializeField]
    private FavoritesListController<Recipe> listController;

    private List<Recipe> recipeList;    

    private void Awake()
    {   
        if (!PlayerPrefs.HasKey("favorites")) PlayerPrefs.SetString("favorites", "[]");
        else
        {
            List<int> recipeIDs = FavoritesList();
            recipeList = new List<Recipe>();

            for (int i = 0; i < recipeIDs.Count; i++)
            {   
                var recipe = GetRecipe(recipeIDs[i]);
                recipeList.Add(recipe);
            }

            listController.SetListItemData(recipeList);
        }
    }

    private List<int> FavoritesList()
    {
        return JsonConvert.DeserializeObject<List<int>>(PlayerPrefs.GetString("favorites"));        
    }

    private Recipe GetRecipe(int id)
    {
        Dictionary<string, string> param = new Dictionary<string, string>();

        param.Add("recipeID", id.ToString());
        return ApiCommunicator.MakeRequest<Recipe>("recipe", param);
    }
}  

