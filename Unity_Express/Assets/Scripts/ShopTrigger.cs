using UnityEngine;
using UnityEngine.UI;

public class ShopTrigger : MonoBehaviour
{
    public GameObject shopButton; // Reference to the ShopButton in the UI

    void Start()
    {
        // Hide the shop button at the start
        shopButton.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Show the button when the player enters the trigger
        if (other.CompareTag("Player"))
        {
            shopButton.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Hide the button when the player exits the trigger
        if (other.CompareTag("Player"))
        {
            shopButton.SetActive(false);
        }
    }
}
