using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    
    [Header("UI")]
    public TMP_Text moneyText;
    
    private int currentMoney = 0;

    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        UpdateUI();
        Debug.Log($"${amount} toegevoegd. Totaal: ${currentMoney}");
    }

    void UpdateUI()
    {
        moneyText.text = $"${currentMoney}";
    }
}