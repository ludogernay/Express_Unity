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

    // ID du joueur (tu devras avoir ce ID de quelque mani�re dans ton jeu)
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


    // M�thode pour augmenter le portefeuille du joueur de 1
    public void IncreaseWallet()
    {
        StartCoroutine(GetPlayerData(apiUrl,"6735d8e64afb5a08e4d5ddc5"));
        // R�cup�rer le portefeuille actuel et l'augmenter de 1
        StartCoroutine(UpdatePlayerWallet("6735d8e64afb5a08e4d5ddc5",getWallet+1));
    }

    private IEnumerator GetPlayerData(string apiUrl,string endpoint )
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl + endpoint);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // D�s�rialiser la r�ponse JSON en un objet ApiResponse
            ApiResponse response = JsonUtility.FromJson<ApiResponse>(request.downloadHandler.text);

            // Acc�der aux donn�es de weapon
            PlayerData player = response.player;
            getWallet = player.wallet;
        }
        else
        {
            Debug.LogError("Erreur lors de la r�cup�ration des donn�es de l'arme : " + request.error);
        }
    }

    // Coroutine pour envoyer la requ�te PATCH
    private IEnumerator UpdatePlayerWallet(string playerId, int amountToAdd)
    {
        // Cr�e un objet contenant les donn�es � envoyer (nom et portefeuille)
        PlayerData playerData = new PlayerData
        {
            name = "PlayerName",  // Use the actual player name
            wallet = amountToAdd
        };

        // S�rialisation de l'objet en JSON
        string json = JsonUtility.ToJson(playerData);

        // Cr�e la requ�te PATCH
        UnityWebRequest request = new UnityWebRequest(apiUrl + playerId, "PATCH");
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
