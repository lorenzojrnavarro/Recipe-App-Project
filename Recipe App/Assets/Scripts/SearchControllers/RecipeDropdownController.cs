using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

public class RecipeDropdownController : SearchableDropdownController<Recipe>
{
    protected override async void Awake()
    {
        base.Awake();

        HttpClient client = new HttpClient();

        try
        {
            HttpResponseMessage response = await client.GetAsync("http://localhost:4444/");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            Recipe recipes = JsonConvert.DeserializeObject<Recipe>(responseBody);

            SetSearchableList(new List<Recipe>() { recipes });
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }


        client.Dispose();
    }    
}
