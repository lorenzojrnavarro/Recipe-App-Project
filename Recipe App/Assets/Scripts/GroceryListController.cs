using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;

public class GroceryListController : ItemListController
{
    private List<string> ingredientsList;

    protected override void Awake()
    {
        base.Awake();

        if (!PlayerPrefs.HasKey("groceryList")) PlayerPrefs.SetString("groceryList", "[]");


        ingredientsList = JsonConvert.DeserializeObject<List<string>>(PlayerPrefs.GetString("groceryList"));
        for (int i = 0; i < ingredientsList.Count; i++)
        {
            AddItem(ingredientsList[i]);
        }        
    }

    protected override void AddButtonClicked(string[] columns)
    {
        base.AddButtonClicked(columns);
        ingredientsList.Add(string.Join(" ", columns));
        ListChanged();
    }

    protected override int DeleteButtonClicked(ColumnListItem listItem)
    {        
        int index = base.DeleteButtonClicked(listItem);
        ingredientsList.RemoveAt(index);
        ListChanged();

        return index;
    }

    private void ListChanged()
    {
        PlayerPrefs.SetString("groceryList", JsonConvert.SerializeObject(ingredientsList));
        PlayerPrefs.Save();
    }
}
