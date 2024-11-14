using UnityEngine;

public class OpenShopPanel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject shopPanel; // Reference to the ShopPanel in the UI

    void Start()
    {
        // Hide the shop panel at the start
        shopPanel.SetActive(false);
    }
    public void showShopPanel()
    {
        // Show the panel when the player clicks the shop button
        shopPanel.SetActive(true);
    }
    public void hideShopPanel()
    {
        // Hide the panel when the player clicks the close button
        shopPanel.SetActive(false);
    }
    }
