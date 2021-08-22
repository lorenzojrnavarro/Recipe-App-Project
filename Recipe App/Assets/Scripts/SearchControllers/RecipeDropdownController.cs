using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using UnityEngine;

public class RecipeDropdownController : SearchableDropdownController<Recipe>
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void UpdateOptions(string text)
    {
        Dictionary<string, string> param = new Dictionary<string, string>();
        param.Add("recipeName", text);        

        completeOptionsList = ApiCommunicator.MakeRequest<Recipe[]>("recipeSearch", param).ToList();

        base.UpdateOptions(text);
    }
}
