using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ListManager : MonoBehaviour
{
    public Transform content;
    public GameObject AddScreen;
    public Button inputDone;
    public GameObject ListPrefab;

    string filePath;

    private List<ListIngredient> listIng = new List<ListIngredient>();

    private InputField[] addIn;

    public class ListItem
    {
        public string ingName;
        public int index;

        public ListItem(string name, int index)
        {
            this.ingName = name;
            this.index = index;
        }
    }

    private void Start()
    {
        filePath = Application.persistentDataPath + "/ingredient.txt";
        LoadJSONData();
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

    void CreateIngList(string name, int loadIndex = 0, bool loading = false)
    {
        GameObject ing = Instantiate(ListPrefab);
        ing.transform.SetParent(content);
        ListIngredient ingObject = ing.GetComponent<ListIngredient>();
        int index = loadIndex;
        if (!loading)
            index = listIng.Count;
        ingObject.SetObjectInfo(name, index);
        listIng.Add(ingObject);
        ListIngredient temp = ingObject;
        ingObject.GetComponent<Toggle>().onValueChanged.AddListener(delegate { IngCheck(temp); });
        if (!loading)
        {
            SaveJSONData();
            SwitchMode(0);
        }
    }

    void IngCheck(ListIngredient ing)
    {
        listIng.Remove(ing);
        SaveJSONData();
        Destroy(ing.gameObject);
    }

    void SaveJSONData()
    {
        string contents = "";

        for(int i = 0; i<listIng.Count; i++)
        {
            ListItem temp = new ListItem(listIng[i].ingName, listIng[i].index);
            contents += JsonUtility.ToJson(temp) + "\n";
        }
        File.WriteAllText(filePath, contents);
    }

    void LoadJSONData()
    {
        if(File.Exists(filePath))
        {
            string contents = File.ReadAllText(filePath);

            string[] splitContents = contents.Split('\n');

            foreach(string content in splitContents)
            {
                if(content.Trim() != "") {
                    ListItem temp = JsonUtility.FromJson<ListItem>(content.Trim());
                    CreateIngList(temp.ingName, temp.index, true);
                }
            }
        }
        else
        {
            Debug.Log("No file");
        }
    }
}
