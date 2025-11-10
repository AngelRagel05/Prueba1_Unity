using System.Collections;
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

    public int CurrentWave => currentWave;

    private void Start()
    {
        StartCoroutine(SpawnNextWave());
    }

    private IEnumerator SpawnNextWave()
    {
        waveInProgress = true;

        int enemiesToSpawn = GetEnemiesForWave(currentWave);
        enemiesRemaining = enemiesToSpawn;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemyForWave(currentWave);
            yield return new WaitForSeconds(0.5f);
        }

        // Espera hasta que todos los enemigos mueran antes de marcar que la oleada terminó
        while (enemiesRemaining > 0)
            yield return null;

        waveInProgress = false;
        currentWave++;

        if (currentWave <= 20)
            StartCoroutine(SpawnNextWave());
    }

    private void SpawnEnemyForWave(int wave)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject prefabToSpawn;

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
            enemyAI.SetWaveManager(this);
    }

    public void EnemyDied()
    {
        enemiesRemaining--;
    }

    private int GetEnemiesForWave(int wave)
    {
        return Mathf.Min(3 + (wave - 1) * 2, 50);
    }
}
