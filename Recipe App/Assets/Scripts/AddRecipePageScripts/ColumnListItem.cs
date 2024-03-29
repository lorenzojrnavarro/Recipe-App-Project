using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;
using System.Text;

public class ColumnListItem : MonoBehaviour
{
    public delegate int DeleteItem(ColumnListItem item);
    public DeleteItem OnDeleteButtonClicked;
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

    public string GetValue()
    {
        StringBuilder value = new StringBuilder();

        for (int i = 0; i < columnTexts.Length; i++)
        {
            value.Append(columnTexts[i].text);
            value.Append("&");
        }

        return value.ToString();
    }
}
