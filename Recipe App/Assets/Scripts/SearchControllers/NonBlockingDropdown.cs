using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NonBlockingDropdown<T> : TMP_Dropdown
{
    private List<CustomDropdownItem<T>> customDropdownItems;

    protected override GameObject CreateBlocker(Canvas rootCanvas)
    {
        return null;
    }

    public void PopulateCustomDropdown(T[] data)
    {
        customDropdownItems = GetComponentsInChildren<CustomDropdownItem<T>>().ToList();

        for (int i = 0; i < data.Length; i++)
        {
            customDropdownItems[i].Populate(data[i]);

            if(i == 0) customDropdownItems[i].ItemClicked += OnClick;
        }
    }

    private void OnClick(CustomDropdownItem<T> customDropdownItem)
    {
        value = -1;
        value = customDropdownItems.IndexOf(customDropdownItem);
    }
}

public abstract class CustomDropdownItem<T> : MonoBehaviour
{
    public Action<CustomDropdownItem<T>> ItemClicked;

    public abstract void Populate(T data);
    public abstract string GetName();
}