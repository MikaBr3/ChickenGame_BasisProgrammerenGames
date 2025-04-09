using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;      // Sleep je Enemy-prefab hiernaartoe
    public GameObject enemyPrefab2;      // Sleep je Enemy-prefab hiernaartoe
    public float spawnInterval = 3f;   // Tijd tussen spawns
    public float minSpawnDistance = 3f; // Minimale afstand tot de speler
    public float maxSpawnDistance = 7f; // Maximale afstand tot de speler
    public int maxAttempts = 10;       // Maximaal aantal pogingen om een plek te vinden

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (player == null) return;

        Vector2 spawnPosition;
        bool positionFound = false;
        int attempts = 0;

        // Probeer een geldige spawnplek te vinden
        while (!positionFound && attempts < maxAttempts)
        {
            // Kies een willekeurige richting en afstand
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
            spawnPosition = (Vector2)player.position + randomDirection * randomDistance;

            // Controleer of de plek vrij is (optioneel: gebruik Physics2D.OverlapCircle)
            Collider2D hit = Physics2D.OverlapCircle(spawnPosition, 0.5f);
            if (hit == null) // Geen obstakels gevonden
            {
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                Instantiate(enemyPrefab2, spawnPosition, Quaternion.identity);
                positionFound = true;
            }

            attempts++;
        }

        if (!positionFound)
            Debug.LogWarning("Geen geldige spawnplek gevonden na " + maxAttempts + " pogingen.");
    }
}