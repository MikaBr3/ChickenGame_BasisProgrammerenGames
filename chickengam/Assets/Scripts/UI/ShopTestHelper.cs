using UnityEngine;

public class ShopTestHelper : MonoBehaviour
{
    [Header("Test Controls")]
    public KeyCode addMoneyKey = KeyCode.M;
    public KeyCode resetUpgradesKey = KeyCode.R;
    public int moneyToAdd = 100;

    void Update()
    {
        // Add money for testing
        if (Input.GetKeyDown(addMoneyKey))
        {
            if (MoneyManager.Instance != null)
            {
                MoneyManager.Instance.AddMoney(moneyToAdd);
                Debug.Log($"Added ${moneyToAdd} for testing. Total: ${MoneyManager.Instance.GetCurrentMoney()}");
            }
            else
            {
                Debug.LogWarning("MoneyManager not found in scene!");
            }
        }

        // Reset upgrades for testing
        if (Input.GetKeyDown(resetUpgradesKey))
        {
            if (Upgrades.Instance != null)
            {
                Upgrades.Instance.damageUpgrade.level = 0;
                Upgrades.Instance.speedUpgrade.level = 0;
                Upgrades.Instance.fireRateUpgrade.level = 0;
                Upgrades.Instance.healthUpgrade.level = 0;
                
                // Reset player stats
                PlayerMovement player = Object.FindFirstObjectByType<PlayerMovement>();
                if (player != null)
                {
                    player.moveSpeed = 5f;
                    player.fireRate = 0.5f;
                }
                
                PlayerHealth playerHealth = Object.FindFirstObjectByType<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.maxHealth = 100f;
                    playerHealth.IncreaseHealth(0); // This will reset current health to max
                }
                
                Debug.Log("All upgrades reset to level 0!");
            }
            else
            {
                Debug.LogWarning("Upgrades not found in scene!");
            }
        }
    }

    void OnGUI()
    {
        // Display test controls on screen
        GUILayout.BeginArea(new Rect(10, 10, 300, 100));
        GUILayout.Label("Shop Test Controls:");
        GUILayout.Label($"Press {addMoneyKey} to add ${moneyToAdd}");
        GUILayout.Label($"Press {resetUpgradesKey} to reset all upgrades");
        GUILayout.EndArea();
    }
} 