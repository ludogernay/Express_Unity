using UnityEngine;

public class Coin : MonoBehaviour
{
    [HideInInspector]
    public CoinSpawner spawner;

    void Start()
    {
        // No need to find the GameManager when using the singleton
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance?.AddCoin(1);
            spawner?.CoinCollected();
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        spawner?.CoinCollected();
    }
}