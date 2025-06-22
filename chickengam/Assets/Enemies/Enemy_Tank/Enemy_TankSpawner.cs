using UnityEngine;
using System.Collections;

public class Enemy_TankSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;      // Sleep je Enemy-prefab hiernaartoe
    public float spawnInterval = 8f;   // Tijd tussen spawns
    public float minSpawnDistance = 3f; // Minimale afstand tot de speler
    public float maxSpawnDistance = 7f; // Maximale afstand tot de speler
    public int maxAttempts = 10;       // Maximaal aantal pogingen om een plek te vinden

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
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
            if (TrySpawnEnemy())
            {
                spawnedCount++;
                yield return new WaitForSeconds(spawnInterval);
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    bool TrySpawnEnemy()
    {
        if (player == null) return false;

        Vector2 spawnPosition;
        int attempts = 0;

        while (attempts < maxAttempts)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
            spawnPosition = (Vector2)player.position + randomDirection * randomDistance;

            Collider2D hit = Physics2D.OverlapCircle(spawnPosition, 0.5f);
            if (hit == null)
            {
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                return true;
            }

            attempts++;
        }

        Debug.LogWarning("Geen geldige spawnplek gevonden na " + maxAttempts + " pogingen. Trying again...");
        return false;
    }
}