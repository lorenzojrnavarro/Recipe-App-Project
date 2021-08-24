using SFB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
    
public static class FileBrowser
{
    public static void BrowseImage(NativeGallery.MediaPickCallback pathCallback)
    {
        #if (UNITY_EDITOR || UNITY_STANDALONE)            
            BrowseImagePC(pathCallback);
        #else
            BrowseMobile(pathCallback);
        #endif
    }

    private static void BrowseImagePC(NativeGallery.MediaPickCallback pickCallback)
    {
        var extensions = new[] {
            new ExtensionFilter("Image Files", "png", "jpg", "jpeg" )
        };

        string[] path = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, false);
        string filePath = (path.Length > 0) ? path[0] : string.Empty;

        pickCallback.Invoke(filePath);
    }

    private static void BrowseMobile(NativeGallery.MediaPickCallback pathCallback)
    {
        NativeGallery.GetImageFromGallery(pathCallback);
    }
}
