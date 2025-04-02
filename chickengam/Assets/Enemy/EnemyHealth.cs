using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log(name + " start HP: " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("TakeDamage wordt aangeroepen! Schade: " + damage);
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Debug.Log("Enemy sterft nu!");
            Destroy(gameObject);
        }
    }
}