using UnityEngine;

[System.Serializable]
public class Upgrade
{
    public string name;
    public int cost;
    public int level;
    public int maxLevel;
    public float valuePerLevel;
    public string description;
}

public class Upgrades : MonoBehaviour
{
    [Header("Player Upgrades")]
    public Upgrade damageUpgrade = new Upgrade
    {
        name = "Damage",
        cost = 50,
        level = 0,
        maxLevel = 5,
        valuePerLevel = 10f,
        description = "Increase bullet damage"
    };
    
    public Upgrade speedUpgrade = new Upgrade
    {
        name = "Speed",
        cost = 30,
        level = 0,
        maxLevel = 5,
        valuePerLevel = 1f,
        description = "Increase movement speed"
    };
    
    public Upgrade fireRateUpgrade = new Upgrade
    {
        name = "Fire Rate",
        cost = 75,
        level = 0,
        maxLevel = 5,
        valuePerLevel = 0.1f,
        description = "Increase shooting speed"
    };
    
    public Upgrade healthUpgrade = new Upgrade
    {
        name = "Health",
        cost = 100,
        level = 0,
        maxLevel = 5,
        valuePerLevel = 20f,
        description = "Increase maximum health"
    };

    public static Upgrades Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool CanAffordUpgrade(Upgrade upgrade)
    {
        return MoneyManager.Instance.GetCurrentMoney() >= GetUpgradeCost(upgrade);
    }

    public int GetUpgradeCost(Upgrade upgrade)
    {
        return upgrade.cost + (upgrade.level * 25); // Cost increases with level
    }

    public void PurchaseUpgrade(Upgrade upgrade)
    {
        if (CanAffordUpgrade(upgrade) && upgrade.level < upgrade.maxLevel)
        {
            int cost = GetUpgradeCost(upgrade);
            MoneyManager.Instance.SpendMoney(cost);
            upgrade.level++;
            
            // Apply the upgrade effect
            ApplyUpgrade(upgrade);
            
            Debug.Log($"Purchased {upgrade.name} level {upgrade.level} for ${cost}");
        }
    }

    private void ApplyUpgrade(Upgrade upgrade)
    {
        PlayerMovement player = Object.FindFirstObjectByType<PlayerMovement>();
        PlayerHealth playerHealth = Object.FindFirstObjectByType<PlayerHealth>();
        
        if (player == null) return;

        switch (upgrade.name)
        {
            case "Damage":
                // Damage is handled in the Bullet script
                break;
            case "Speed":
                player.moveSpeed += upgrade.valuePerLevel;
                break;
            case "Fire Rate":
                // Decrease fire rate to shoot faster (lower value = faster shooting)
                player.fireRate = Mathf.Max(0.1f, player.fireRate - upgrade.valuePerLevel);
                break;
            case "Health":
                if (playerHealth != null)
                {
                    playerHealth.IncreaseHealth((int)upgrade.valuePerLevel);
                }
                break;
        }
    }

    public float GetUpgradeValue(Upgrade upgrade)
    {
        return upgrade.level * upgrade.valuePerLevel;
    }
}
