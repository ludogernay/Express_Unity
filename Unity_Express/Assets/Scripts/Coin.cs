using UnityEngine;

public class Coin : MonoBehaviour
{
    [HideInInspector]
    public CoinSpawner spawner;

    void Start()
    {
        // Optional: Destroy the coin after a certain time to prevent buildup
        Destroy(gameObject, 15f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Add code to update the player's coin count or score
            // Example: collision.gameObject.GetComponent<Player>().AddCoin();

            // Notify the spawner that the coin has been collected
            if (spawner != null)
            {
                spawner.CoinCollected();
            }

            // Destroy the coin
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        // Ensure the coin count is decremented if the coin is destroyed by other means
        if (spawner != null)
        {
            spawner.CoinCollected();
        }
    }
}