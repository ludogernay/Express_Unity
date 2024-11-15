using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class APIRequest : MonoBehaviour
{
    [System.Serializable]
    public class WeaponData
    {
        public string _id;
        public string name;
        public string category;
        public string price;
        public string url;
        public string createdAt;
        public string updatedAt;
        public int __v;  // Pour capturer la valeur de "__v" si nécessaire
    }
    [System.Serializable]
    public class WeaponArrayWrapper
    {
        public WeaponData[] weapons;
    }
    private string baseURL = "http://localhost:3000";
    public RawImage weaponImage1;
    public RawImage weaponImage2;
    public RawImage weaponImage3;

    public TextMeshProUGUI nameWeapon1;
    public TextMeshProUGUI nameWeapon2;
    public TextMeshProUGUI priceWeapon1;
    public TextMeshProUGUI priceWeapon2;
    public TextMeshProUGUI categoryWeapon1;
    public TextMeshProUGUI categoryWeapon2;
    public TextMeshProUGUI nameWeapon3;
    public TextMeshProUGUI categoryWeapon3;
    public TextMeshProUGUI priceWeapon3;


    public void GetMessageFromAPI()
    {
        StartCoroutine(GetRequest("/api/weapons"));
    }

    private IEnumerator DownloadImage(string url, RawImage weaponImage)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url); // Crée une requête pour télécharger l'image
        yield return request.SendWebRequest(); // Attends que la requête soit terminée

        if (request.result == UnityWebRequest.Result.Success)
        {
            // Récupère la texture depuis la réponse et l'applique au RawImage
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            weaponImage.texture = texture;
        }
        else
        {
            Debug.LogError("Failed to load image: " + request.error);
        }
    }

    // public void SendDataToAPI()
    // {
    //     string jsonData = "{\"name\": \"Unity\", \"type\": \"Game Engine\"}";

    //     StartCoroutine(PostRequest("/api/weapons", jsonData));
    // }

    // Coroutine for GET request
    private IEnumerator GetRequest(string endpoint)
    {
        UnityWebRequest request = UnityWebRequest.Get(baseURL + endpoint);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string response = request.downloadHandler.text;

            // Vérifier si la réponse est sous forme de tableau JSON ou besoin d'enveloppe
            WeaponData[] weaponData = null;
            string wrappedJson = "{\"weapons\":" + response + "}";

            try
            {
                // Si la réponse est déjà un tableau JSON, on peut le désérialiser directement
                weaponData = JsonUtility.FromJson<WeaponArrayWrapper>(wrappedJson).weapons;
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error during deserialization: " + ex.Message);
            }

            if (weaponData != null && weaponData.Length > 0)
            {
                // Afficher les données dans l'interface utilisateur
                nameWeapon1.text = weaponData[0].name;
                nameWeapon2.text = weaponData[1].name;
                priceWeapon1.text = weaponData[0].price;
                priceWeapon2.text = weaponData[1].price;
                categoryWeapon1.text = weaponData[0].category;
                categoryWeapon2.text = weaponData[1].category;
                nameWeapon3.text = weaponData[2].name;
                categoryWeapon3.text = weaponData[2].category;
                priceWeapon3.text = weaponData[2].price;
                StartCoroutine(DownloadImage(weaponData[0].url, weaponImage1));
                StartCoroutine(DownloadImage(weaponData[1].url, weaponImage2));
                StartCoroutine(DownloadImage(weaponData[2].url, weaponImage3));
            }
            else
            {
                Debug.LogWarning("No weapon data found or response is empty.");
            }
        }
        else
        {
            Debug.LogError("GET Request Failed! Error: " + request.error);
        }
    }


    // Coroutine for POST request
    // private IEnumerator PostRequest(string endpoint, string jsonData)
    // {
    //     UnityWebRequest request = new UnityWebRequest(baseURL + endpoint, "POST");

    //     // Convert string data to byte array
    //     byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);

    //     // Set the content type header
    //     request.uploadHandler = new UploadHandlerRaw(jsonToSend);
    //     request.downloadHandler = new DownloadHandlerBuffer();
    //     request.SetRequestHeader("Content-Type", "application/json");

    //     // Wait for the request to complete
    //     yield return request.SendWebRequest();

    //     // Check if the request was successful
    //     if (request.result == UnityWebRequest.Result.Success)
    //     {
    //         Debug.Log("POST Request Successful! Response: " + request.downloadHandler.text);
    //     }
    //     else
    //     {
    //         Debug.LogError("POST Request Failed! Error: " + request.error);
    //     }
    // }
}
