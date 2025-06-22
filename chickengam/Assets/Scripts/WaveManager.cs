using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    public List<GameObject> enemySpawners; // Drag all your Enemy Spawner GameObjects here in the Inspector
    public TMP_Text waveText; // Drag a UI Text element here to display the wave number
    public TMP_Text enemiesLeftText; // Drag a UI Text element here to display the remaining enemies

    private int currentWave = 0;
    private int enemiesLeftInWave;
    private bool isSpawning;

    public float timeBetweenWaves = 5f; // Time in seconds before the next wave starts

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Disable all spawners at the start, the WaveManager will control them
        foreach (var spawner in enemySpawners)
        {
            spawner.SetActive(false);
        }
        
        StartCoroutine(StartNextWaveAfterDelay());
    }

    private IEnumerator StartNextWaveAfterDelay()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        currentWave++;
        UpdateWaveUI();
        StartWave(currentWave);
    }

    void StartWave(int waveNumber)
    {
        enemiesLeftInWave = 0;
        isSpawning = true;

        Debug.Log("Starting Wave " + waveNumber);

        // --- This is where you define the logic for each wave ---
        // You can add more cases or change these numbers to customize your game.
        switch (waveNumber)
        {
            case 1:
                // Wave 1: 10 Normal enemies
                ActivateSpawner("Enemy_NormaalSpawner", 10);
                enemiesLeftInWave += 10;
                break;

            case 2:
                // Wave 2: 15 Normal enemies
                ActivateSpawner("Enemy_NormaalSpawner", 15);
                enemiesLeftInWave += 15;
                break;

            case 3:
                // Wave 3: Introduce Speed enemies
                ActivateSpawner("Enemy_NormaalSpawner", 10);
                ActivateSpawner("Enemy_SpeedSpawner", 5);
                enemiesLeftInWave += 15;
                break;

            case 4:
                // Wave 4: More Normal and Speed
                ActivateSpawner("Enemy_NormaalSpawner", 15);
                ActivateSpawner("Enemy_SpeedSpawner", 8);
                enemiesLeftInWave += 23;
                break;

            case 5:
                // Wave 5: Introduce Strong enemies
                ActivateSpawner("Enemy_NormaalSpawner", 15);
                ActivateSpawner("Enemy_SpeedSpawner", 10);
                ActivateSpawner("Enemy_StrongSpawner", 3);
                enemiesLeftInWave += 28;
                break;

            default:
                // For waves 6 and beyond, it gets progressively harder.
                // You can create any formula you want here.
                int normalCount = 10 + waveNumber;
                int speedCount = 10 + waveNumber;
                int strongCount = 5 + (waveNumber - 5);
                // Tanks appear from wave 9 onwards
                int tankCount = (waveNumber > 8) ? (waveNumber - 8) * 2 : 0;

                ActivateSpawner("Enemy_NormaalSpawner", normalCount);
                ActivateSpawner("Enemy_SpeedSpawner", speedCount);
                ActivateSpawner("Enemy_StrongSpawner", strongCount);
                if (tankCount > 0)
                {
                    ActivateSpawner("Enemy_TankSpawner", tankCount);
                }

                enemiesLeftInWave += normalCount + speedCount + strongCount + tankCount;
                break;
        }

        isSpawning = false;
        UpdateWaveUI();
    }

    // Helper function to find and activate a spawner
    private void ActivateSpawner(string spawnerName, int count)
    {
        GameObject spawnerToActivate = null;
        foreach (var spawnerGO in enemySpawners)
        {
            // Find the spawner whose GameObject name matches the requested name
            if (spawnerGO.name == spawnerName)
            {
                spawnerToActivate = spawnerGO;
                break; // Found it, stop searching.
            }
        }

        if (spawnerToActivate != null)
        {
            spawnerToActivate.SetActive(true);
            // We assume the script name matches the object name, which is the pattern used.
            var spawnerComponent = spawnerToActivate.GetComponent(spawnerName) as MonoBehaviour;

            if (spawnerComponent != null)
            {
                spawnerComponent.SendMessage("StartSpawning", count, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                Debug.LogError($"Could not find the script '{spawnerName}' on the GameObject '{spawnerName}'. Make sure the script is attached and has the correct name.");
            }
        }
        else
        {
            Debug.LogWarning($"Could not find a spawner GameObject named '{spawnerName}' in the WaveManager's list.");
        }
    }

    public void OnEnemyKilled()
    {
        if (isSpawning) return; // Don't count kills while the wave is still setting up

        enemiesLeftInWave--;
        UpdateWaveUI(); // Update the UI every time an enemy dies

        if (enemiesLeftInWave <= 0)
        {
            Debug.Log("Wave " + currentWave + " Complete!");
            // Deactivate all spawners to be safe
            foreach (var spawner in enemySpawners)
            {
                spawner.SetActive(false);
            }
            StartCoroutine(StartNextWaveAfterDelay());
        }
    }

    void UpdateWaveUI()
    {
        if (waveText != null)
        {
            waveText.text = "Wave: " + currentWave;
        }
        if (enemiesLeftText != null)
        {
            enemiesLeftText.text = "Enemies Left: " + enemiesLeftInWave;
        }
    }
} 