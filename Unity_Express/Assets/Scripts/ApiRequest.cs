using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class APIRequest : MonoBehaviour
{
    private string baseURL = "http://localhost:3000";

    [System.Serializable]
    public class WeaponData
    {
        public string name;
        public string type;
        public string price;
    }

    public void GetMessageFromAPI()
    {
        StartCoroutine(SendRequest("/api/weapons", "GET"));
    }

    public void SendDataToAPI()
    {
        WeaponData data = new WeaponData { name = "Unity", type = "Game Engine" };
        string jsonData = JsonUtility.ToJson(data);
        StartCoroutine(SendRequest("/api/weapons", "POST", jsonData));
    }

    private IEnumerator SendRequest(string endpoint, string method = "GET", string jsonData = null)
    {
        UnityWebRequest request;
        if (method == "POST")
        {
            request = new UnityWebRequest(baseURL + endpoint, method);
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.SetRequestHeader("Content-Type", "application/json");
        }
        else
        {
            request = UnityWebRequest.Get(baseURL + endpoint);
        }
        request.downloadHandler = new DownloadHandlerBuffer();
        request.timeout = 10;

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"{method} Request Successful! Response: {request.downloadHandler.text}");

            if (method == "GET")
            {
                WeaponData weapon = JsonUtility.FromJson<WeaponData>(request.downloadHandler.text);
                Debug.Log("Weapon Name: " + weapon.name + ", Type: " + weapon.type);
            }
        }
        else
        {
            Debug.LogError($"{method} Request Failed! Error: " + request.error);
        }
    }
}
