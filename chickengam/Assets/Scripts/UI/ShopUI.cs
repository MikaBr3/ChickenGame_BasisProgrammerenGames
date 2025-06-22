using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    [Header("Shop UI References")]
    public Button damageButton;
    public Button speedButton;
    public Button fireRateButton;
    public Button healthButton;
    public Button closeButton;
    
    [Header("Upgrade Display")]
    public TMP_Text damageText;
    public TMP_Text speedText;
    public TMP_Text fireRateText;
    public TMP_Text healthText;
    
    [Header("Money Display")]
    public TMP_Text moneyText;
    
    private ShopTrigger shopTrigger;
    
    void Start()
    {
        // Setup button listeners
        if (damageButton != null)
            damageButton.onClick.AddListener(() => PurchaseUpgrade(Upgrades.Instance.damageUpgrade));
        
        if (speedButton != null)
            speedButton.onClick.AddListener(() => PurchaseUpgrade(Upgrades.Instance.speedUpgrade));
        
        if (fireRateButton != null)
            fireRateButton.onClick.AddListener(() => PurchaseUpgrade(Upgrades.Instance.fireRateUpgrade));
        
        if (healthButton != null)
            healthButton.onClick.AddListener(() => PurchaseUpgrade(Upgrades.Instance.healthUpgrade));
        
        if (closeButton != null)
            closeButton.onClick.AddListener(CloseShop);
        
        // Find the shop trigger
        shopTrigger = Object.FindFirstObjectByType<ShopTrigger>();
        
        // Initial update
        UpdateShopUI();
    }
    
    void Update()
    {
        // Update money display
        if (moneyText != null && MoneyManager.Instance != null)
        {
            moneyText.text = $"Money: ${MoneyManager.Instance.GetCurrentMoney()}";
        }
        
        // Check for escape key to close shop
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseShop();
        }
    }
    
    public void UpdateShopUI()
    {
        if (Upgrades.Instance == null) return;
        
        // Update damage upgrade display
        if (damageText != null)
        {
            var upgrade = Upgrades.Instance.damageUpgrade;
            int cost = Upgrades.Instance.GetUpgradeCost(upgrade);
            bool canAfford = Upgrades.Instance.CanAffordUpgrade(upgrade);
            
            damageText.text = $"Damage (Level {upgrade.level}/{upgrade.maxLevel})\n" +
                             $"Cost: ${cost}\n" +
                             $"Current: +{Upgrades.Instance.GetUpgradeValue(upgrade)} damage\n";
            
            if (damageButton != null)
            {
                damageButton.interactable = canAfford && upgrade.level < upgrade.maxLevel;
            }
        }
        
        // Update speed upgrade display
        if (speedText != null)
        {
            var upgrade = Upgrades.Instance.speedUpgrade;
            int cost = Upgrades.Instance.GetUpgradeCost(upgrade);
            bool canAfford = Upgrades.Instance.CanAffordUpgrade(upgrade);
            
            speedText.text = $"Speed (Level {upgrade.level}/{upgrade.maxLevel})\n" +
                            $"Cost: ${cost}\n" +
                            $"Current: +{Upgrades.Instance.GetUpgradeValue(upgrade)} speed\n";
            
            if (speedButton != null)
            {
                speedButton.interactable = canAfford && upgrade.level < upgrade.maxLevel;
            }
        }
        
        // Update fire rate upgrade display
        if (fireRateText != null)
        {
            var upgrade = Upgrades.Instance.fireRateUpgrade;
            int cost = Upgrades.Instance.GetUpgradeCost(upgrade);
            bool canAfford = Upgrades.Instance.CanAffordUpgrade(upgrade);
            
            // Get current fire rate from player
            PlayerMovement player = Object.FindFirstObjectByType<PlayerMovement>();
            float currentFireRate = player != null ? player.fireRate : 0.5f;
            
            fireRateText.text = $"Fire Rate (Level {upgrade.level}/{upgrade.maxLevel})\n" +
                               $"Cost: ${cost}\n" +
                               $"Current: {currentFireRate:F2}s between shots";
            
            if (fireRateButton != null)
            {
                fireRateButton.interactable = canAfford && upgrade.level < upgrade.maxLevel;
            }
        }
        
        // Update health upgrade display
        if (healthText != null)
        {
            var upgrade = Upgrades.Instance.healthUpgrade;
            int cost = Upgrades.Instance.GetUpgradeCost(upgrade);
            bool canAfford = Upgrades.Instance.CanAffordUpgrade(upgrade);
            
            healthText.text = $"Health (Level {upgrade.level}/{upgrade.maxLevel})\n" +
                             $"Cost: ${cost}\n" +
                             $"Current: +{Upgrades.Instance.GetUpgradeValue(upgrade)} health";
            
            if (healthButton != null)
            {
                healthButton.interactable = canAfford && upgrade.level < upgrade.maxLevel;
            }
        }
    }
    
    void PurchaseUpgrade(Upgrade upgrade)
    {
        if (Upgrades.Instance != null)
        {
            Upgrades.Instance.PurchaseUpgrade(upgrade);
            UpdateShopUI(); // Refresh the UI after purchase
        }
    }
    
    void CloseShop()
    {
        if (shopTrigger != null)
        {
            shopTrigger.CloseShop();
        }
    }
} 