using UnityEngine;
using UnityEngine.UI;

public class SimpleHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public Image healthFill; // Sleep je healthbar fill image hiernaartoe
    
    [Header("Debug")]
    public bool enableDebug = true;
    
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        if(enableDebug) Debug.Log("[Health] Systeem gestart. Health: " + currentHealth);
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
        if(enableDebug) Debug.Log($"[Health] Update: {healthFill.fillAmount:P0}");
    }

    void Die()
    {
        Debug.LogWarning("[Health] Speler is dood!");
        // Voorbeeld dood-logica:
        // GetComponent<PlayerMovement>().enabled = false;
        // Time.timeScale = 0f;
    }

    void Update()
    {
        // Testfunctionaliteit - kun je later verwijderen
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
            if(enableDebug) Debug.Log($"[TEST] Schade ontvangen. Health: {currentHealth}");
        }
    }
}