using UnityEngine;
using UnityEngine.UI;

public class SimpleHealth : MonoBehaviour
{
    public Image healthFill; // Verwijs naar je rode Image
    public float maxHealth = 100;
    float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar(); // Zorg dat de healthbar meteen goed staat
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        UpdateHealthBar();
        
        if(currentHealth <= 0) Die();
    }

    void UpdateHealthBar()
    {
        healthFill.fillAmount = currentHealth / maxHealth;
        Debug.Log($"Healthbar update: {healthFill.fillAmount * 100}%");
    }

    void Die()
    {
        Debug.LogWarning("Speler is dood!");
        // Voeg hier dood-logica toe, bijvoorbeeld:
        // Time.timeScale = 0; // Pause het spel
        // SceneManager.LoadScene("GameOver");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
            Debug.Log($"Schade ontvangen. Huidige health: {currentHealth}");
        }
    }
}