using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class FileBrowser
{
    public static void BrowseImage(NativeGallery.MediaPickCallback pathCallback)
    {
        //#if (UNITY_EDITOR || UNITY_STANDALONE)
        //    BrowseImagePC(out filePath);
        //#else 
        //    BrowseMobile(out filePath);
        //#endif

        BrowseMobile(pathCallback);
    }

    //private static void BrowseImagePC(out string filePath)
    //{
    //    var extensions = new[] {
    //        new ExtensionFilter("Image Files", "png", "jpg", "jpeg" )
    //    };

    //    string[] path = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, false);
    //    filePath = (path.Length > 0) ? path[0] : string.Empty;
    //}

    private static void BrowseMobile(NativeGallery.MediaPickCallback pathCallback)
    {
        NativeGallery.GetImageFromGallery(pathCallback);
    }
}
