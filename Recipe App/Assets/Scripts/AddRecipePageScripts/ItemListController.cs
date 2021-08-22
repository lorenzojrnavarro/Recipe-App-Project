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

    private void Awake()
    {
        listItems = new List<ColumnListItem>();
        addListItem = GetComponentInChildren<AddListItem>();
        addListItem.OnAddButtonClicked += AddButtonClicked;
    }

    private void AddButtonClicked(string[] columns)
    {
        GameObject listItemGO = Instantiate(listItemPrefab, contentTransform);
        ColumnListItem listItem = listItemGO.GetComponent<ColumnListItem>();
        listItem.OnDeleteButtonClicked += DeleteButtonClicked;
        listItem.Populate(columns);

        addListItem.transform.SetAsLastSibling();
        listItems.Add(listItem);
    }

    private void DeleteButtonClicked(ColumnListItem listItem)
    {
        listItems.Remove(listItem);
        Destroy(listItem.gameObject);
    }

    public List<string> GetValues()
    {
        return listItems.Select(x => x.GetValue()).ToList();
    }
}
