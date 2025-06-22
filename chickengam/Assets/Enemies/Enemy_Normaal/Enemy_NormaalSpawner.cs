using UnityEngine;
using System.Collections;

public class Enemy_NormaalSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;      // Sleep je Enemy-prefab hiernaartoe
    public float spawnInterval = 3f;   // Tijd tussen spawns
    public float minSpawnDistance = 3f; // Minimale afstand tot de speler
    public float maxSpawnDistance = 7f; // Maximale afstand tot de speler
    public int maxAttempts = 10;       // Maximaal aantal pogingen om een plek te vinden

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // InvokeRepeating("SpawnEnemy", 0f, spawnInterval); // We remove this so the WaveManager can control spawning.
    }

    public void StartSpawning(int enemyCount)
    {
        StartCoroutine(SpawnWave(enemyCount));
    }

    private System.Collections.IEnumerator SpawnWave(int enemyCount)
    {
        int spawnedCount = 0;
        while (spawnedCount < enemyCount)
        {
            // Keep trying to spawn until successful
            if (TrySpawnEnemy())
            {
                spawnedCount++;
                yield return new WaitForSeconds(spawnInterval); // Wait before spawning the next one
            }
            else
            {
                // If it fails to find a spot, wait a short moment and try again
                // without counting it as a spawned enemy.
                yield return new WaitForSeconds(0.5f); 
            }
        }
    }

    // Changed to return true on success and false on failure
    bool TrySpawnEnemy()
    {
        if (player == null) return false;

        Vector2 spawnPosition;
        int attempts = 0;

        // Probeer een geldige spawnplek te vinden
        while (attempts < maxAttempts)
        {
            // Kies een willekeurige richting en afstand
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
            spawnPosition = (Vector2)player.position + randomDirection * randomDistance;

            // Controleer of de plek vrij is
            Collider2D hit = Physics2D.OverlapCircle(spawnPosition, 0.5f);
            if (hit == null) // Geen obstakels gevonden
            {
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                return true; // Success!
            }

            attempts++;
        }

        Debug.LogWarning("Geen geldige spawnplek gevonden na " + maxAttempts + " pogingen. Trying again...");
        return false; // Failure
    }
}