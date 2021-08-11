using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemListController : MonoBehaviour
{
    [SerializeField]
    private GameObject listItemPrefab;

    [SerializeField]
    private Transform contentTransform;

    private AddListItem addListItem;

    private void Awake()
    {
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
    }

    private void DeleteButtonClicked(ColumnListItem listItem)
    {
        Destroy(listItem.gameObject);
    }
}
