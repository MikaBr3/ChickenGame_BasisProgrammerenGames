using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 3;
    private int currentHealth;

    [Header("Rewards")]
    public int cashWorth = 10; // Stel dit in per enemy type in de Inspector

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"TakeDamage: {damage} schade");
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Geef geld aan de speler
        if (MoneyManager.Instance != null)
        {
            MoneyManager.Instance.AddMoney(cashWorth);
            Debug.Log($"Enemy gaf ${cashWorth}");
        }
        else
        {
            Debug.LogWarning("MoneyManager niet gevonden!");
        }

        Destroy(gameObject);
        
        // Optioneel death effect:
        Debug.Log("Enemy destroyed");
    }
}