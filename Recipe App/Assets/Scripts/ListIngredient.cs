using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ListIngredient : MonoBehaviour
{
    public string ingName;
    public int index;

    private Text itemText;

    private void Start()
    {
        itemText = GetComponentInChildren<Text>();
        itemText.text = ingName;
    }

    public void SetObjectInfo(string name, int index)
    {
        this.ingName = name;
        this.index = index;
    }

}
