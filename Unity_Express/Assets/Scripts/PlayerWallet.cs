using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerWallet : MonoBehaviour
{
    // L'URL de ton API (ajustez en fonction de ton environnement)
    private string apiUrl = "http://localhost:3000/api/players/Test";

    // ID du joueur (tu devras avoir ce ID de quelque mani�re dans ton jeu)
    public string playerId;

    // M�thode pour augmenter le portefeuille du joueur de 1
    public void IncreaseWallet()
    {
        // R�cup�rer le portefeuille actuel et l'augmenter de 1
        StartCoroutine(UpdatePlayerWallet("Test", 1));
    }

    // Coroutine pour envoyer la requ�te PATCH
    private IEnumerator UpdatePlayerWallet(string playerName, int amountToAdd)
    {
        // Cr�e un objet contenant les donn�es � envoyer (nom et portefeuille)
        var playerData = new
        {
            name = "PlayerName", // Remplacez par le nom r�el du joueur
            wallet = amountToAdd
        };

        // S�rialisation de l'objet en JSON
        string json = JsonUtility.ToJson(playerData);

        // Cr�e la requ�te PATCH
        UnityWebRequest request = new UnityWebRequest(apiUrl + playerName, "PATCH");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);

        // D�finit les param�tres de la requ�te
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Envoi de la requ�te
        yield return request.SendWebRequest();

        // V�rification de la r�ponse
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
