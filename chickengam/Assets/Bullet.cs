using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    public float lifetime = 3f; // Nieuw: levensduur in seconden
    
    private Vector2 direction;
    private float spawnTime; // Nieuw: tijd van spawnen

    void Start()
    {
        spawnTime = Time.time; // Onthoud wanneer de bullet is gespawned
        
        // Apply damage upgrade if available
        if (Upgrades.Instance != null)
        {
            damage += (int)Upgrades.Instance.GetUpgradeValue(Upgrades.Instance.damageUpgrade);
        }
    }

    void Update()
    {
        // Beweging
        transform.Translate(direction * speed * Time.deltaTime);
        
        // Verwijder na 3 seconden
        if (Time.time - spawnTime >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Try to damage all enemy types
            Enemy_NormaalHealth enemyNormaal = collision.GetComponent<Enemy_NormaalHealth>();
            if (enemyNormaal != null)
            {
                enemyNormaal.TakeDamage(damage);
                Destroy(gameObject);
                return;
            }
            
            Enemy_SpeedHealth enemySpeed = collision.GetComponent<Enemy_SpeedHealth>();
            if (enemySpeed != null)
            {
                enemySpeed.TakeDamage(damage);
                Destroy(gameObject);
                return;
            }
            
            Enemy_StrongHealth enemyStrong = collision.GetComponent<Enemy_StrongHealth>();
            if (enemyStrong != null)
            {
                enemyStrong.TakeDamage(damage);
                Destroy(gameObject);
                return;
            }
            
            Enemy_TankHealth enemyTank = collision.GetComponent<Enemy_TankHealth>();
            if (enemyTank != null)
            {
                enemyTank.TakeDamage(damage);
                Destroy(gameObject);
                return;
            }
        }
        else if (!collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}