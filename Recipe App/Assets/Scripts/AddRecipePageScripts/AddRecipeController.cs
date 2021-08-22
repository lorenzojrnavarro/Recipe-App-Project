using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AddRecipeController : MonoBehaviour
{
    [SerializeField]
    private RawImage buttonImage;

    private string imageFilePath;

    private void Awake()
    {
        //byte[] bytes = File.ReadAllBytes(@"F:\\Pictures\\Perter.png");
        //Dictionary<string, string> param = new Dictionary<string, string>();

        //param.Add("recipeImageBytes", Convert.ToBase64String(bytes));
        //param.Add("recipeImageName", "Perter");

        //ApiCommunicator.MakePut("recipeUpload", param);
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
        //UnityWebRequest unityWebRequest = UnityWebRequest.Get(imageFilePath);
        //yield return unityWebRequest.SendWebRequest();

        //image.texture = DownloadHandlerTexture.GetContent(unityWebRequest) as Texture2D;

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imagePath);
        yield return request.SendWebRequest();
        Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        image.texture = texture;
    }
}
