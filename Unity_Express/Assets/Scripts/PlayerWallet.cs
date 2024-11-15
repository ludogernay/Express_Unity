using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerWallet : MonoBehaviour
{
    // L'URL de ton API (ajustez en fonction de ton environnement)
    private string apiUrl = "http://localhost:3000/api/players/Test";

    // ID du joueur (tu devras avoir ce ID de quelque manière dans ton jeu)
    public string playerId;

    // Méthode pour augmenter le portefeuille du joueur de 1
    public void IncreaseWallet()
    {
        // Récupérer le portefeuille actuel et l'augmenter de 1
        StartCoroutine(UpdatePlayerWallet("Test", 1));
    }

    // Coroutine pour envoyer la requête PATCH
    private IEnumerator UpdatePlayerWallet(string playerName, int amountToAdd)
    {
        // Crée un objet contenant les données à envoyer (nom et portefeuille)
        var playerData = new
        {
            name = "PlayerName", // Remplacez par le nom réel du joueur
            wallet = amountToAdd
        };

        // Sérialisation de l'objet en JSON
        string json = JsonUtility.ToJson(playerData);

        // Crée la requête PATCH
        UnityWebRequest request = new UnityWebRequest(apiUrl + playerName, "PATCH");
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
