using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class RecipeDropdownItem : CustomDropdownItem<Recipe>
{
    [SerializeField]
    private RawImage recipeImage;

    [SerializeField]
    private TextMeshProUGUI recipeName;

    [SerializeField]
    private TextMeshProUGUI recipeCalorieCount;

    public string RecipeName { get { return recipeName.text; } set { recipeName.text = value; } }

    public string RecipeCalorieCount { get { return recipeCalorieCount.text; } set { recipeCalorieCount.text = value; } }    

    void Awake()
    {
        
    } 

    public override void Populate(Recipe data)
    {
        RecipeName = data.Name;
        RecipeCalorieCount = data.Calories;

        StartCoroutine(LoadImage(recipeImage, data.ImageURL));
    }

    public override string GetName()
    {
        return RecipeName;
    }

    private IEnumerator LoadImage(RawImage image, string imageURL)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageURL);
        yield return request.SendWebRequest();
        Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        image.texture = texture;
    }
}
