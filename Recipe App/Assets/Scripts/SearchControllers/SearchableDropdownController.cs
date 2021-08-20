using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class SearchableDropdownController<T> : MonoBehaviour where T : ISearchable
{
    //public Action<SearchableDropdownController> ValueSelected;
    public Action SearchModified; 

    [HideInInspector] public TMP_InputField inputField;
    [HideInInspector] public NonBlockingDropdown<T> dropdown;

    private List<T> completeOptionsList;
    private List<T> searchingList;

    protected virtual void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
        dropdown = GetComponentInChildren<NonBlockingDropdown<T>>();

        inputField.onValueChanged.AddListener(UpdateOptions);
        dropdown.onValueChanged.AddListener(UpdateInputFieldText);

        //SetSearchableList(new List<string>() { "Cheesecake", "Spaghetti", "Coffee", "Cum" });
    }

    private void RefreshOptions()
    {
        if (dropdown is null) return;
        
        dropdown.Hide();
        dropdown.ClearOptions();
        dropdown.AddOptions(searchingList.Select(x => x.Name).ToList());
        dropdown.RefreshShownValue();
    }

    //public void SetSearchableList(List<string> listItems)
    //{
    //    for (int i = 0; i < listItems.Count; i++)
    //    {
    //        listItems[i] = Regex.Replace(listItems[i], "(\\B[A-Z])", " $1");
    //    }

    //    completeOptionsList = listItems;
    //    searchingList = listItems;

    //    RefreshOptions();
    //}
        

    public void SetSearchableList(List<T> listItems)
    {
        completeOptionsList = listItems;
        searchingList = listItems;

        RefreshOptions();        
    }

    private IEnumerator PopulateCustomData(List<T> listItems)
    {
        yield return null;

        dropdown.PopulateCustomDropdown(listItems.ToArray());
    }

    private void UpdateOptions(string text)
    {
        searchingList = completeOptionsList.FindAll((x) => Regex.IsMatch(x.Name, Regex.Escape(text), RegexOptions.IgnoreCase));

        //-1 works here because there is a valid placeholder
        dropdown.SetValueWithoutNotify(-1);
        
        RefreshOptions();
        dropdown.Show();
        inputField.Select();
        inputField.selectionAnchorPosition = 1;
        SearchModified?.Invoke();

        StartCoroutine(PopulateCustomData(searchingList));
    }

    public void UpdateInputFieldText(int index)
    {
        inputField.SetTextWithoutNotify(searchingList[index].Name);
        dropdown.SetValueWithoutNotify(index);

        //ValueSelected?.Invoke(this);
    }

    public string GetValue()
    {
        return Regex.Replace(inputField.text, "(\\s+)", "");
    }

    public void SetValue(string value)
    {
        inputField.text = Regex.Replace(value, "(\\B[A-Z])", " $1");
        
        dropdown.Hide();
    }

    private void OnDestroy()
    {
        inputField.onValueChanged.RemoveListener(UpdateOptions);
        dropdown.onValueChanged.RemoveListener(UpdateInputFieldText);
    }
}