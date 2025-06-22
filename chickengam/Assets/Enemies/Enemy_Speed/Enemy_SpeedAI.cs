using UnityEngine;

public class Enemy_SpeedAI : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    
    [Header("Combat")]
    public float damage = 10f; // Pas dit aan per enemy type
    public float attackRange = 1.5f;
    public float attackCooldown = 0.5f;
    
    private Transform player;
    private float lastAttackTime;
    private PlayerHealth playerHealth;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if(player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        // Beweeg naar speler
        if(distanceToPlayer > attackRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        // Val aan wanneer dichtbij
        else if(Time.time > lastAttackTime + attackCooldown)
        {
            AttackPlayer();
            lastAttackTime = Time.time;
        }
    }

    void AttackPlayer()
    {
        if(playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            Debug.Log($"Enemy attacked! Damage: {damage}");
        }
    }
}