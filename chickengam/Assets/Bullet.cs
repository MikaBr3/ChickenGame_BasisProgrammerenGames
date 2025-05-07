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
            Enemy_NormaalHealth enemy = collision.GetComponent<Enemy_NormaalHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else if (!collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
         if (collision.CompareTag("Enemy"))
        {
            Enemy_SpeedHealth enemy = collision.GetComponent<Enemy_SpeedHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else if (!collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
         if (collision.CompareTag("Enemy"))
        {
            Enemy_StrongHealth enemy = collision.GetComponent<Enemy_StrongHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else if (!collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
           if (collision.CompareTag("Enemy"))
        {
            Enemy_TankHealth enemy = collision.GetComponent<Enemy_TankHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else if (!collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}