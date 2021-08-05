using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TMPro;
using UnityEngine;

public class RecipePageController : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI ingredientsText;
    public TextMeshProUGUI directionsText;

    private async void Awake()
    {
        HttpClient client = new HttpClient();

        try
        {
            HttpResponseMessage response = await client.GetAsync("http://localhost:4444/");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            Recipe[] recipes = JsonConvert.DeserializeObject<Recipe[]>(responseBody);
            nameText.text = recipes[0].Name;
            ingredientsText.text = string.Join("<br>", recipes[0].Ingredients);
            directionsText.text = string.Join("<br><br>", recipes[0].Directions);
        }
        catch(Exception e)
        {
            Debug.LogError(e.Message);
        }

        client.Dispose();
    }
}
