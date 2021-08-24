using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using UnityEngine;
using UnityEngine.UI;

public class RecipeDropdownController : SearchableDropdownController<Recipe>
{
    [SerializeField]
    private GameObject recipeAllergenContents;

    private Toggle[] recipeAllergens;

    protected override void Awake()
    {
        base.Awake();
        recipeAllergens = recipeAllergenContents.GetComponentsInChildren<Toggle>();
    }

    protected override void UpdateOptions(string text)
    {
        Dictionary<string, string> param = new Dictionary<string, string>();
        param.Add("recipeName", text);
        param.Add("recipeAllergens", string.Join<int>(",", GetAllergenList()));

        completeOptionsList = ApiCommunicator.MakeRequest<Recipe[]>("recipeSearch", param).ToList();

        base.UpdateOptions(text);
    }

    private List<int> GetAllergenList()
    {
        List<int> allergenIds = new List<int>();
        for (int i = 0; i < recipeAllergens.Length; i++)
        {
            if (recipeAllergens[i].isOn)
            {
                int allergenId = recipeAllergens[i].GetComponent<AllergenIDs>().allergenID;
                allergenIds.Add(allergenId);
            }
        }

        return allergenIds;
    }
}
