using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FavoritesListController<T> : MonoBehaviour
{
    [SerializeField]
    private GameObject listItemPrefab;

    [SerializeField]
    private Transform contentTransform;

    private List<FavoritesItem> favoriteListItems;  

    protected virtual void Awake()
    {
        
    }

    public void SetListItemData(List<Recipe> listItem)
    {
        favoriteListItems = new List<FavoritesItem>();

        for (int i = 0; i < listItem.Count; i++)
        {
            FavoritesItem item = Instantiate(listItemPrefab, contentTransform).GetComponent<FavoritesItem>();
            item.PopulateFavoritesData(listItem[i]);
            item.ItemClicked += ItemSelected;
            item.DeleteButtonClicked += DeleteButtonClicked;
            favoriteListItems.Add(item);
        }
    }

    public void ItemSelected(FavoritesItem selectedItem)
    {
        RecipeManager.CurrentRecipe = selectedItem.recipe;

        SceneChanger.AddScene("Recipe Page");
    }

    private void DeleteButtonClicked(FavoritesItem selectedItem)
    {    
        List<int> favorites = JsonConvert.DeserializeObject<List<int>>(PlayerPrefs.GetString("favorites"));
        
        favorites.Remove(selectedItem.recipe.ID);

        PlayerPrefs.SetString("favorites", JsonConvert.SerializeObject(favorites));
        PlayerPrefs.Save();

        favoriteListItems.Remove(selectedItem);
        Destroy(selectedItem.gameObject);
    }
}
