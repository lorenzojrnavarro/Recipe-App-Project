using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RecipePageController : MonoBehaviour
{
    [SerializeField]
    private Toggle favoriteToggle;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI caloriesText;
    public TextMeshProUGUI ingredientsText;
    public TextMeshProUGUI directionsText;
    public RawImage foodImage;

    private Recipe recipe;

    private async void Awake()
    {
        recipe = GameObject.FindObjectOfType<MainPageController>().selectedRecipe;

        try
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("recipeID", recipe.ID.ToString());
            recipe = ApiCommunicator.MakeRequest<Recipe>("recipe", param);
                        
            nameText.text = recipe.Name;
            caloriesText.text = "Calories: " + recipe.Calories;
            ingredientsText.text = recipe.Ingredients.Replace("@", "<br>").Replace('&', ' ');
            directionsText.text = recipe.Instructions;

            StartCoroutine(LoadImage(foodImage, recipe.ImageURL));

            if (PlayerPrefs.HasKey("favorites"))
            {
                List<int> favorites = JsonConvert.DeserializeObject<List<int>>(PlayerPrefs.GetString("favorites"));
                favoriteToggle.SetIsOnWithoutNotify(favorites.Contains(recipe.ID));
            }            
        }
        catch(Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    private IEnumerator LoadImage(RawImage image, string imageURL)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageURL);
        yield return request.SendWebRequest();
        Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        image.texture = texture;
    }

    public void AddToFavorites(bool addToFavorites)
    {
        if (!PlayerPrefs.HasKey("favorites")) PlayerPrefs.SetString("favorites", "[]");

        List<int> favorites = JsonConvert.DeserializeObject<List<int>>(PlayerPrefs.GetString("favorites"));

        if (addToFavorites) favorites.Add(recipe.ID);
        else favorites.Remove(recipe.ID);
        
        PlayerPrefs.SetString("favorites", JsonConvert.SerializeObject(favorites));
        PlayerPrefs.Save();
    }
}
