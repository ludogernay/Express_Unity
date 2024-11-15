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

    // Références aux éléments UI
    public TextMeshProUGUI weaponNameText;
    public TextMeshProUGUI weaponPriceText;
    public Image weaponImage;

    void Start()
    {
        // Appel de la fonction pour charger et afficher l'arme
        StartCoroutine(GetWeaponData("/api/weapons/67355f9e5ff4d27cecea2e8d")); // Exemple d'ID d'arme
    }

    private IEnumerator GetWeaponData(string endpoint)
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
            StartCoroutine(LoadImage(weapon.url));
        }
        else
        {
            Debug.LogError("Erreur lors de la récupération des données de l'arme : " + request.error);
        }
    }


    private IEnumerator LoadImage(string imageUrl)
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
