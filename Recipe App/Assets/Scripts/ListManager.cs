using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListManager : MonoBehaviour
{
    public Transform content;
    public GameObject AddScreen;
    public Button inputDone;
    public GameObject ListPrefab;

    string filePath;

    private List<ListIngredient> listIng = new List<ListIngredient>();

    private InputField[] addIn;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/ingredient.txt";
        addIn = AddScreen.GetComponentsInChildren<InputField>();

        inputDone.onClick.AddListener(delegate { CreateIngList(addIn[0].text); });
    }

    public void SwitchMode(int mode)
    {
        switch (mode)
        {
            case 0:
                AddScreen.SetActive(false);
                break;
            case 1:
                AddScreen.SetActive(true);
                break;
        }
    }

    void CreateIngList(string name)
    {
        GameObject ing = Instantiate(ListPrefab);
        ing.transform.SetParent(content);
        ListIngredient ingObject = ing.GetComponent<ListIngredient>();
        int index = 0;
        if(listIng.Count > 0)
            index = listIng.Count - 1;
        ingObject.SetObjectInfo(name, index);
        listIng.Add(ingObject);
        ListIngredient temp = ingObject;
        ingObject.GetComponent<Toggle>().onValueChanged.AddListener(delegate { IngCheck(temp); });

        SwitchMode(0);
    }

    void IngCheck(ListIngredient ing)
    {
        listIng.Remove(ing);
        Destroy(ing.gameObject);
    }

}
