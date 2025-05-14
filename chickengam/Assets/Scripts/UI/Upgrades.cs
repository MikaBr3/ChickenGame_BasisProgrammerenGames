using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeShop : MonoBehaviour
{
    [Header("References")]
    public PlayerMovement playerMovement;
    public Bullet bulletTemplate; // Reference to a bullet prefab (for damage upgrades)
    public MoneyManager moneyManager;
    
    [Header("UI Elements")]
    public TMP_Text moneyText;
    public Button upgradeFireRateButton;
    public Button upgradeDamageButton;
    public Button upgradeMoveSpeedButton;

    [Header("Upgrade Costs")]
    public int fireRateUpgradeCost = 50;
    public int damageUpgradeCost = 75;
    public int moveSpeedUpgradeCost = 40;

    void Start()
    {
        // Initialize UI
        UpdateMoneyUI();
        
        // Setup button listeners
        upgradeFireRateButton.onClick.AddListener(UpgradeFireRate);
        upgradeDamageButton.onClick.AddListener(UpgradeDamage);
        upgradeMoveSpeedButton.onClick.AddListener(UpgradeMoveSpeed);
    }

    void UpdateMoneyUI()
    {
        moneyText.text = $"${moneyManager.currentMoney}";
    }

    // === UPGRADE METHODS ===
    public void UpgradeFireRate()
    {
        if (moneyManager.CanAfford(fireRateUpgradeCost))
        {
            moneyManager.AddMoney(-fireRateUpgradeCost);
            playerMovement.fireRate *= 0.9f; // 10% faster shooting
            fireRateUpgradeCost = (int)(fireRateUpgradeCost * 1.5f); // Increase next cost
            UpdateMoneyUI();
            Debug.Log("Fire Rate Upgraded!");
        }
    }

    public void UpgradeDamage()
    {
        if (moneyManager.CanAfford(damageUpgradeCost))
        {
            moneyManager.AddMoney(-damageUpgradeCost);
            bulletTemplate.damage += 1; // +1 damage per upgrade
            damageUpgradeCost = (int)(damageUpgradeCost * 1.5f);
            UpdateMoneyUI();
            Debug.Log("Damage Upgraded!");
        }
    }

    public void UpgradeMoveSpeed()
    {
        if (moneyManager.CanAfford(moveSpeedUpgradeCost))
        {
            moneyManager.AddMoney(-moveSpeedUpgradeCost);
            playerMovement.moveSpeed += 0.5f; // +0.5 speed
            moveSpeedUpgradeCost = (int)(moveSpeedUpgradeCost * 1.5f);
            UpdateMoneyUI();
            Debug.Log("Speed Upgraded!");
        }
    }
}