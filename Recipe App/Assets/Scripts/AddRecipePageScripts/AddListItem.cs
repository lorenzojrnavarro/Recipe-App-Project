using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class AddListItem : MonoBehaviour
{
    public Action<string[]> OnAddButtonClicked;

    private TMP_InputField[] inputFieldColumns;

    private void Awake()
    {
        inputFieldColumns = GetComponentsInChildren<TMP_InputField>();
    }

    public void AddButtonClicked()
    {
        if (inputFieldColumns.Count(x => x.text == string.Empty) > 0) return;

        string[] columnValues = inputFieldColumns.Select(x => x.text).ToArray();
        OnAddButtonClicked.Invoke(columnValues);
        ClearValues();
    }

    private void ClearValues()
    {
        foreach (TMP_InputField item in inputFieldColumns)
        {
            item.text = string.Empty;
        }
    }
}
