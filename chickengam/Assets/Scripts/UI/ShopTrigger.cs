using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [Header("Shop Settings")]
    public GameObject shopUI;
    public string shopName = "Shop";
    
    [Header("Visual Feedback")]
    public GameObject shopIcon;
    public Color highlightColor = Color.yellow;
    
    private SpriteRenderer iconRenderer;
    private Color originalColor;
    
    void Start()
    {
        // Hide shop UI at start
        if (shopUI != null)
        {
            shopUI.SetActive(false);
        }
        
        // Setup shop icon
        if (shopIcon != null)
        {
            iconRenderer = shopIcon.GetComponent<SpriteRenderer>();
            if (iconRenderer != null)
            {
                originalColor = iconRenderer.color;
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Visual feedback
            if (iconRenderer != null)
            {
                iconRenderer.color = highlightColor;
            }
            
            // Open the shop automatically on enter
            OpenShop();
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Reset visual feedback
            if (iconRenderer != null)
            {
                iconRenderer.color = originalColor;
            }
            
            // Close shop if open
            if (shopUI != null && shopUI.activeSelf)
            {
                CloseShop();
            }
        }
    }
    
    void OpenShop()
    {
        if (shopUI != null)
        {
            shopUI.SetActive(true);
            
            // Update the shop UI to show current values
            ShopUI shopUIComponent = shopUI.GetComponent<ShopUI>();
            if (shopUIComponent != null)
            {
                shopUIComponent.UpdateShopUI();
            }
            
            // Pause game or disable player movement
            Time.timeScale = 0f;
            
            // Disable player movement
            PlayerMovement playerMovement = FindFirstObjectByType<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }
            
            Debug.Log($"{shopName} opened!");
        }
    }
    
    public void CloseShop()
    {
        if (shopUI != null)
        {
            shopUI.SetActive(false);
            
            // Resume game
            Time.timeScale = 1f;
            
            // Re-enable player movement
            PlayerMovement playerMovement = FindFirstObjectByType<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.enabled = true;
            }
            
            Debug.Log($"{shopName} closed!");
        }
    }
} 