using UnityEngine;

public class Enemy_SpeedHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 2;
    private int currentHealth;
    private bool isDead = false;

    [Header("Rewards")]
    public int cashWorth = 15; // Stel dit in per enemy type in de Inspector

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        Debug.Log($"TakeDamage: {damage} schade");
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("Speed Enemy Died");
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

        // Notify the WaveManager that an enemy has been killed
        if (WaveManager.Instance != null)
        {
            WaveManager.Instance.OnEnemyKilled();
        }

        Destroy(gameObject);
        
        // Optioneel death effect:
        Debug.Log("Enemy destroyed");
    }
}