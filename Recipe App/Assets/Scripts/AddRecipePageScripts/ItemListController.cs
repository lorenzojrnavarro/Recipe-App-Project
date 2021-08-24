using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemListController : MonoBehaviour
{
    [SerializeField]
    private GameObject listItemPrefab;

    [SerializeField]
    private Transform contentTransform;

    private List<ColumnListItem> listItems;
    private AddListItem addListItem;

    protected virtual void Awake()
    {
        listItems = new List<ColumnListItem>();
        addListItem = GetComponentInChildren<AddListItem>();
        addListItem.OnAddButtonClicked += AddButtonClicked;
    }

    protected void AddItem(string[] columns)
    {
        GameObject listItemGO = Instantiate(listItemPrefab, contentTransform);
        ColumnListItem listItem = listItemGO.GetComponent<ColumnListItem>();
        listItem.OnDeleteButtonClicked += DeleteButtonClicked;
        listItem.Populate(columns);

        listItems.Add(listItem);
    }

    protected void AddItem(string column)
    {
        AddItem(new string[] { column });
    }


    protected virtual void AddButtonClicked(string[] columns)
    {
        AddItem(columns);
        addListItem.transform.SetAsLastSibling();
    }

    protected virtual int DeleteButtonClicked(ColumnListItem listItem)
    {
        int itemIndex = listItems.IndexOf(listItem);

        listItems.Remove(listItem);
        Destroy(listItem.gameObject);

        return itemIndex;
    }

    public List<string> GetValues()
    {
        return listItems.Select(x => x.GetValue()).ToList();
    }
}
