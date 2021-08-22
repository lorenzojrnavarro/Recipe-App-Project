using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NonBlockingDropdown<T> : TMP_Dropdown
{
    protected override GameObject CreateBlocker(Canvas rootCanvas)
    {
        return null;
    }

    public void PopulateCustomDropdown(T[] data)
    {
        CustomDropdownItem<T>[] customItems = GetComponentsInChildren<CustomDropdownItem<T>>();

        for (int i = 0; i < data.Length; i++)
        {
            customItems[i].Populate(data[i]);
        }
    }
}

public abstract class CustomDropdownItem<T> : MonoBehaviour
{
    public abstract void Populate(T data);
    public abstract string GetName();
}