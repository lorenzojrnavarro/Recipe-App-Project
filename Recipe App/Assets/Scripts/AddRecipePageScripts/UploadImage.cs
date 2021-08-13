using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class UploadImage : MonoBehaviour
{
    private const string awsBucketName = "morgothscookbookbucket";
    private const string awsAccessKey = "AKIAUTMAXTBVPHNVOVGE";
    private const string awsSecretKey = "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";

    private string awsURLBaseVirtual = "";

    void Awake()
    {
        awsURLBaseVirtual = "https://" + awsBucketName + ".s3.amazonaws.com/";
    }

    public void UploadFileToAWS3 (string fileName, string filePath)
    {
        string currentAWS3Date = System.DateTime.UtcNow.ToString("ddd, dd MMM yyyy HH:mm:ss ") + "GMT";
        string canonicalString = "PUT\n\n\nx-amz-date:" + currentAWS3Date + "\n/" + awsBucketName + "/" + fileName;

        UTF8Encoding encode = new UTF8Encoding();
        HMACSHA1 signature = new HMACSHA1();

        signature.Key = encode.GetBytes(awsSecretKey);
        byte[] bytes = encode.GetBytes(canonicalString);
        byte[] moreBytes = signature.ComputeHash(bytes);
        string encodedCanonical = Convert.ToBase64String(moreBytes);

        string aws3Header = "AWS " + awsAccessKey + ":" + encodedCanonical;

        string url3 = awsURLBaseVirtual + fileName;

        WebRequest requestS3 = (HttpWebRequest)WebRequest.Create(url3);
        requestS3.Headers.Add("Authorization", aws3Header);
        requestS3.Headers.Add("x-amz-date", currentAWS3Date);

        byte[] fileRawBytes = File.ReadAllBytes(filePath);
        requestS3.ContentLength = fileRawBytes.Length;

        requestS3.Method = "PUT";

        Stream s3Stream = requestS3.GetRequestStream();
        s3Stream.Write(fileRawBytes, 0, fileRawBytes.Length);
        Debug.Log("Sent bytes: " + requestS3.ContentLength + ", for file: " + fileName);

        s3Stream.Close();
    }
}
