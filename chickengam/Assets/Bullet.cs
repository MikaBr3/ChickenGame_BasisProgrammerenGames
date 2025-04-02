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
            EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
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