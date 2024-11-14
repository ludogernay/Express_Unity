using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class APIRequest : MonoBehaviour
{
    private string baseURL = "http://localhost:3000"; 

    public void GetMessageFromAPI()
    {
        StartCoroutine(GetRequest("/api/weapons"));
    }

   
    public void SendDataToAPI()
    {
        string jsonData = "{\"name\": \"Unity\", \"type\": \"Game Engine\"}";

        StartCoroutine(PostRequest("/api/weapons", jsonData));
    }

    // Coroutine for GET request
    private IEnumerator GetRequest(string endpoint)
    {
        UnityWebRequest request = UnityWebRequest.Get(baseURL + endpoint);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("GET Request Successful! Response: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("GET Request Failed! Error: " + request.error);
        }
    }

    // Coroutine for POST request
    private IEnumerator PostRequest(string endpoint, string jsonData)
    {
        UnityWebRequest request = new UnityWebRequest(baseURL + endpoint, "POST");

        // Convert string data to byte array
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);

        // Set the content type header
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Wait for the request to complete
        yield return request.SendWebRequest();

        // Check if the request was successful
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("POST Request Successful! Response: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("POST Request Failed! Error: " + request.error);
        }
    }
}
