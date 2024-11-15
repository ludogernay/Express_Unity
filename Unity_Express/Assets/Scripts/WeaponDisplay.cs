using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class WeaponDisplay : MonoBehaviour
{
    private string baseURL = "http://localhost:3000";

    [System.Serializable]
    public class WeaponData
    {
        public string name;
        public string category;
        public string price;
        public string url;
    }

    public class ApiResponse
    {
        public WeaponData weapon;
    }

    // Références aux éléments UI pour 3 armes
    public TextMeshProUGUI weapon1NameText;
    public TextMeshProUGUI weapon1PriceText;
    public Image weapon1Image;

    public TextMeshProUGUI weapon2NameText;
    public TextMeshProUGUI weapon2PriceText;
    public Image weapon2Image;

    public TextMeshProUGUI weapon3NameText;
    public TextMeshProUGUI weapon3PriceText;
    public Image weapon3Image;

    void Start()
    {
        // Appel de la fonction pour charger et afficher les armes
        StartCoroutine(GetWeaponData("/api/weapons/67355f9e5ff4d27cecea2e8d", weapon1NameText, weapon1PriceText, weapon1Image));
        StartCoroutine(GetWeaponData("/api/weapons/67355ffd1c8b3c3130463815", weapon2NameText, weapon2PriceText, weapon2Image));
        StartCoroutine(GetWeaponData("/api/weapons/6736b7a701706e33a770bb50", weapon3NameText, weapon3PriceText, weapon3Image)); 
    }

    private IEnumerator GetWeaponData(string endpoint, TextMeshProUGUI weaponNameText, TextMeshProUGUI weaponPriceText, Image weaponImage)
    {
        UnityWebRequest request = UnityWebRequest.Get(baseURL + endpoint);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // Désérialiser la réponse JSON en un objet ApiResponse
            ApiResponse response = JsonUtility.FromJson<ApiResponse>(request.downloadHandler.text);

            // Accéder aux données de weapon
            WeaponData weapon = response.weapon;

            // Afficher les informations dans les éléments UI
            weaponNameText.text = weapon.name;
            weaponPriceText.text = $"Price: {weapon.price}";

            // Charger l'image depuis l'URL
            StartCoroutine(LoadImage(weapon.url, weaponImage));
        }
        else
        {
            Debug.LogError("Erreur lors de la récupération des données de l'arme : " + request.error);
        }
    }

    private IEnumerator LoadImage(string imageUrl, Image weaponImage)
    {
        Debug.Log("Image URL: " + imageUrl);

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            weaponImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
        else
        {
            Debug.LogError("Erreur lors du chargement de l'image : " + request.error);
        }
    }
}
