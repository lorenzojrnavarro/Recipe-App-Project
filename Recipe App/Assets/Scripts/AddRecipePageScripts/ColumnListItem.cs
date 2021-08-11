using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

public class ColumnListItem : MonoBehaviour
{
    public Action<ColumnListItem> OnDeleteButtonClicked;
    private TextMeshProUGUI[] columnTexts;

    private void Awake()
    {
        columnTexts = GetComponentsInChildren<TextMeshProUGUI>(); 
    }

    public void Populate(string[] columns)
    {
        for (int i = 0; i < columns.Length; i++)
        {
            columnTexts[i].text = columns[i];
        }
    }

    public void DeleteButtonClicked()
    {
        OnDeleteButtonClicked.Invoke(this);
    }
}
