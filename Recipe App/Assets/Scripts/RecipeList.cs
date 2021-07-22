using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeList : MonoBehaviour
{
    public List<string> recipeNames;

    // Start is called before the first frame update
    void Start()
    {
        SearchableDropdownController searchableDropdown = transform.GetComponent<SearchableDropdownController>();
        searchableDropdown.SetSearchableList(recipeNames);
    }

}
