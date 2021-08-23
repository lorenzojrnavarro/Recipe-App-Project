using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class AddRecipeController : MonoBehaviour
{
    [SerializeField]
    private RawImage buttonImage;

    [SerializeField]
    private TMP_InputField recipeName;

    [SerializeField]
    private ItemListController recipeIngredients;

    [SerializeField]
    private ItemListController recipeAllergens;

    [SerializeField]
    private ItemListController recipeProcedure;

    [SerializeField]
    private TMP_InputField recipeCalories;

    private string imageFilePath;

    private void Awake()
    {

    }

    public void SelectPhotoToUpload()
    {
        FileBrowser.BrowseImage(OnImagePathChosen);
    }

    private void OnImagePathChosen(string path)
    {
        imageFilePath = path;
        StartCoroutine(LoadImageLocally(buttonImage, path));
    }

    private IEnumerator LoadImageLocally(RawImage image, string imagePath)
    {     
        #if (UNITY_ANDROID || UNITY_IOS)
            imagePath = "file://" + imageFilePath;
        #endif

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imagePath);
        yield return request.SendWebRequest();
        Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        image.texture = texture;
    }

    public void AcceptButtonClicked()
    {
        byte[] bytes = File.ReadAllBytes(imageFilePath);
        Dictionary<string, string> param = new Dictionary<string, string>();
        
        param.Add("recipeName", recipeName.text);
        param.Add("recipeImageBytes", Convert.ToBase64String(bytes));
        param.Add("recipeIngredients", string.Join("@", recipeIngredients.GetValues()));
        param.Add("recipeAllergens", string.Join(",", recipeAllergens.GetValues()).Replace("&", ""));
        param.Add("recipeProcedure", string.Join("<br><br>", recipeProcedure.GetValues()).Replace("&", ""));
        param.Add("recipeCalories", recipeCalories.text);

        ApiCommunicator.MakePut("recipeUpload", param);
    }
}
