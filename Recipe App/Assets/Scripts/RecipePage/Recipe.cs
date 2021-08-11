using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Recipe
{
    [JsonProperty]
    public string Name { get; private set; }

    [JsonProperty]
    public string ImageURI { get; private set; }

    [JsonProperty]
    public string[] Ingredients { get; private set; }

    [JsonProperty]
    public string[] Directions { get; private set; }
}
