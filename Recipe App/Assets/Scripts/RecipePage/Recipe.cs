using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : ISearchable
{
    [JsonProperty]
    public string Name { get; set; }
    
    [JsonProperty]
    public string Calories { get; /*private*/ set; }

    [JsonProperty]
    public string ImageURL { get; /*private*/ set; }

    [JsonProperty]
    public string Ingredients { get; private set; }

    [JsonProperty]
    public string Instructions { get; private set; }
}
