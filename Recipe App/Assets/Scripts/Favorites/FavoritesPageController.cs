using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FavoritesPageController : MonoBehaviour
{
    List<int> recipeIds;
    private void Awake()
    {
        recipeIds = FavoritesList();
    }

    private List<int> FavoritesList()
    {
        return JsonConvert.DeserializeObject<List<int>>(PlayerPrefs.GetString("favorites"));
    }
}
