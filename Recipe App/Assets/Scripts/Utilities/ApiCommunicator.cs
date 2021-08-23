using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using UnityEngine;

public static class ApiCommunicator
{
    private static string ApiBaseUri = "http://76.175.108.117:25565/";
    //private static string ApiBaseUri = "http://192.168.4.42:25565/";

    public static T MakeRequest<T>(string route, Dictionary<string, string> postParams = null)
    {
        using (var client = new HttpClient())
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(new HttpMethod("POST"), $"{ApiBaseUri}/{route}");

            if (postParams != null)
            {
                string payload = JsonConvert.SerializeObject(postParams);
                requestMessage.Content = new StringContent(payload, Encoding.UTF8, "application/json");
            }

            HttpResponseMessage response = client.SendAsync(requestMessage).Result;

            string apiResponse = response.Content.ReadAsStringAsync().Result;
            try
            {
                // Attempt to deserialise the reponse to the desired type, otherwise throw an expetion with the response from the api.
                if (apiResponse != "")
                    return JsonConvert.DeserializeObject<T>(apiResponse);
                else
                    throw new Exception();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error ocurred while calling the API. It responded with the following message: {response.StatusCode} {response.ReasonPhrase}");
            }
        }
    }

    public static void MakePut(string route, Dictionary<string, string> postParams = null)
    {
        using (var client = new HttpClient())
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(new HttpMethod("PUT"), $"{ApiBaseUri}/{route}");

            if (postParams != null)
            {
                string payload = JsonConvert.SerializeObject(postParams);
                requestMessage.Content = new StringContent(payload, Encoding.ASCII, "application/json");
            }

            HttpResponseMessage response = client.SendAsync(requestMessage).Result;

            string apiResponse = response.Content.ReadAsStringAsync().Result;            
        }
    }
}
