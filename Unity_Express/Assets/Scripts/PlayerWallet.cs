using JetBrains.Annotations;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerWallet : MonoBehaviour
{
    // L'URL de ton API (ajustez en fonction de ton environnement)
    private string apiUrl = "http://localhost:3000/api/players/";
    private int getWallet;

    // ID du joueur (tu devras avoir ce ID de quelque manière dans ton jeu)
    [System.Serializable]
    public class PlayerData
    {
        public string name;
        public int wallet;
    }

    public class ApiResponse
    {
        public PlayerData player;
    }


    // Méthode pour augmenter le portefeuille du joueur de 1
    public void IncreaseWallet()
    {
        StartCoroutine(GetPlayerData(apiUrl,"6735d8e64afb5a08e4d5ddc5"));
        // Récupérer le portefeuille actuel et l'augmenter de 1
        StartCoroutine(UpdatePlayerWallet("6735d8e64afb5a08e4d5ddc5",getWallet+1));
    }

    private IEnumerator GetPlayerData(string apiUrl,string endpoint )
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl + endpoint);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // Désérialiser la réponse JSON en un objet ApiResponse
            ApiResponse response = JsonUtility.FromJson<ApiResponse>(request.downloadHandler.text);

            // Accéder aux données de weapon
            PlayerData player = response.player;
            getWallet = player.wallet;
        }
        else
        {
            Debug.LogError("Erreur lors de la récupération des données de l'arme : " + request.error);
        }
    }

    // Coroutine pour envoyer la requête PATCH
    private IEnumerator UpdatePlayerWallet(string playerId, int amountToAdd)
    {
        // Crée un objet contenant les données à envoyer (nom et portefeuille)
        PlayerData playerData = new PlayerData
        {
            name = "PlayerName",  // Use the actual player name
            wallet = amountToAdd
        };

        // Sérialisation de l'objet en JSON
        string json = JsonUtility.ToJson(playerData);

        // Crée la requête PATCH
        UnityWebRequest request = new UnityWebRequest(apiUrl + playerId, "PATCH");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);

        // Définit les paramètres de la requête
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Envoi de la requête
        yield return request.SendWebRequest();

        // Vérification de la réponse
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Player wallet updated successfully!");
        }
        else
        {
            Debug.LogError("Failed to update wallet: " + request.error);
        }
    }
}
