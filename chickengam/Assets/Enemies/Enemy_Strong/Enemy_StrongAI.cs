using UnityEngine;

public class Enemy_StrongAI : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    
    [Header("Combat")]
    public float damage = 20f; // Pas dit aan per enemy type
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;
    
    private Transform player;
    private float lastAttackTime;
    private PlayerHealth PlayerHealth;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerHealth = player.GetComponent<PlayerHealth>();
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
        if(PlayerHealth != null)
        {
            PlayerHealth.TakeDamage(damage);
            Debug.Log($"Enemy attacked! Damage: {damage}");
        }
    }
}