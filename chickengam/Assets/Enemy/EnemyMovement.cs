using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f; // Snelheid van de enemy
    public float rotationSpeed = 5f; // Soepele rotatie

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        // Zoek de speler bij spawn
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        
        // Directe beweging starten
        if (player == null)
            Debug.LogError("Geen speler gevonden! Zorg voor een GameObject met tag 'Player'");
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            // Richting naar speler berekenen
            Vector2 direction = (player.position - transform.position).normalized;
            
            // Beweging toepassen
            rb.linearVelocity = direction * moveSpeed;
            
            // Rotatie naar bewegingsrichting (optioneel)
            if (rb.linearVelocity != Vector2.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.AngleAxis(angle, Vector3.forward),
                    rotationSpeed * Time.deltaTime
                );
            }
        }
    }
}