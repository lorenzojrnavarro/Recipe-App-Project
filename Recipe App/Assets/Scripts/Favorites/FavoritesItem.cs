using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FavoritesItem : MonoBehaviour
{
    public Action<FavoritesItem> ItemClicked;
    public Action<FavoritesItem> DeleteButtonClicked;

    public Recipe recipe;

    [SerializeField]
    private TextMeshProUGUI recipeName;

    [SerializeField]
    private TextMeshProUGUI recipeCalories;

    [SerializeField]
    private RawImage recipeImage;

    [SerializeField]
    private Button recipeButton;

    [SerializeField]
    private Button deleteButton;

    private void Awake()
    {
        recipeButton.onClick.AddListener(OnClick);
        deleteButton.onClick.AddListener(OnDeleteButtonClicked);
    }

    public void PopulateFavoritesData(Recipe recipe)
    {
        this.recipe = recipe;
        recipeName.text = recipe.Name;
        recipeCalories.text = recipe.Calories.ToString();
        StartCoroutine(LoadImage(recipeImage, recipe.ImageURL));
    }

    private IEnumerator LoadImage(RawImage image, string imageURL)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageURL);
        yield return request.SendWebRequest();
        Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        image.texture = texture;
    }

    private void OnClick()
    {
        ItemClicked?.Invoke(this);
    }

    private void OnDeleteButtonClicked()
    {
        DeleteButtonClicked?.Invoke(this);
    }
}

