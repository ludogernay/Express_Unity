using UnityEngine;
using System.Collections;
public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;    // Assign the coin prefab in the Inspector
    public float spawnInterval = 2f; // Time between spawns
    public int maxCoins = 10;        // Max coins allowed in the scene
    public float spawnAreaWidth = 10f; // Width of the spawn area

    private int currentCoinCount = 0;
    public PlayerWallet playerWallet;

    void Start()
    {
        StartCoroutine(SpawnCoins());
    }

    IEnumerator SpawnCoins()
    {
        while (true)
        {
            if (currentCoinCount < maxCoins)
            {
                SpawnCoin();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnCoin()
    {
        float xPosition = Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);
        Vector2 spawnPosition = new Vector2(xPosition, transform.position.y);

        GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        currentCoinCount++;

        // Assign the spawner reference to the coin
        Coin coinScript = coin.GetComponent<Coin>();
        if (coinScript != null)
        {
            coinScript.spawner = this;
        }
    }

    public void CoinCollected()
    {
        currentCoinCount--;
        // Appeler la méthode IncreaseWallet lorsque la pièce est collectée
        if (playerWallet != null)
        {
            playerWallet.IncreaseWallet();  // Ajoute 1 au portefeuille du joueur
        }

    }
}