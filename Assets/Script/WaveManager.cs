using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject soldierPrefab;
    [SerializeField] private GameObject sergeantPrefab;
    [SerializeField] private GameObject lieutenantPrefab;
    [SerializeField] private GameObject colonelPrefab;

    private int currentWave = 1;
    private int enemiesRemaining;
    private bool waveInProgress = false;

    private void Start()
    {
        StartCoroutine(SpawnNextWave());
    }

    private IEnumerator SpawnNextWave()
    {
        waveInProgress = true;

        int enemiesToSpawn = GetEnemiesForWave(currentWave);
        enemiesRemaining = enemiesToSpawn;

        Debug.Log($"🔸 Oleada {currentWave} comenzando con {enemiesToSpawn} enemigos.");

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemyForWave(currentWave);
            yield return new WaitForSeconds(0.5f);
        }

        waveInProgress = false;
    }

    private void SpawnEnemyForWave(int wave)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject prefabToSpawn;

        // Escalado progresivo de enemigos según la oleada
        if (wave < 5)
            prefabToSpawn = soldierPrefab;
        else if (wave < 10)
            prefabToSpawn = sergeantPrefab;
        else if (wave < 15)
            prefabToSpawn = lieutenantPrefab;
        else
            prefabToSpawn = colonelPrefab;

        GameObject enemy = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);

        EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
        if (enemyAI != null)
        {
            enemyAI.SetWaveManager(this);
        }
    }

    // 🧩 Este método lo llama cada enemigo al morir
    public void EnemyDied()
    {
        enemiesRemaining--;

        if (enemiesRemaining <= 0 && !waveInProgress)
        {
            currentWave++;

            if (currentWave <= 20)
            {
                Debug.Log($"✅ Oleada {currentWave - 1} completada. Preparando la siguiente...");
                StartCoroutine(SpawnNextWave());
            }
            else
            {
                Debug.Log("🎉 Todas las oleadas completadas. ¡Victoria!");
            }
        }
    }

    private int GetEnemiesForWave(int wave)
    {
        // Ejemplo: crece exponencialmente (puedes ajustar)
        return Mathf.Min(3 + (wave - 1) * 2, 50);
    }
}
