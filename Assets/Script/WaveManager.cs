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
        Debug.Log("[WaveManager] Iniciando el sistema de oleadas...");
        StartCoroutine(SpawnNextWave());
    }

    private IEnumerator SpawnNextWave()
    {
        waveInProgress = true;

        int enemiesToSpawn = GetEnemiesForWave(currentWave);
        enemiesRemaining = enemiesToSpawn;

        Debug.Log($"[WaveManager] === OLEADA {currentWave} ===");
        Debug.Log($"[WaveManager] Enemigos a spawnear: {enemiesToSpawn}");

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemyForWave(currentWave);
            yield return new WaitForSeconds(0.5f);
        }

        // Espera hasta que todos los enemigos mueran
        while (enemiesRemaining > 0)
        {
            yield return null;
        }

        Debug.Log($"[WaveManager] Oleada {currentWave} completada.");

        waveInProgress = false;
        currentWave++;

        if (currentWave <= 20)
        {
            Debug.Log($"[WaveManager] Preparando siguiente oleada: {currentWave}");
            yield return new WaitForSeconds(3f);
            StartCoroutine(SpawnNextWave());
        }
        else
        {
            Debug.Log("[WaveManager] Todas las oleadas completadas. ¡Victoria!");
        }
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
        {
            enemyAI.SetWaveManager(this);
            Debug.Log($"[WaveManager] Spawned {enemyAI.enemyType} en {spawnPoint.name} (Wave {wave})");
        }
        else
        {
            Debug.LogWarning("[WaveManager] Enemy instanciado sin EnemyAI.");
        }
    }

    public void EnemyDied()
    {
        enemiesRemaining--;
        Debug.Log($"[WaveManager] EnemyDied() recibido. Restan: {enemiesRemaining}");

        if (enemiesRemaining <= 0)
        {
            Debug.Log("[WaveManager] Todos los enemigos eliminados. Oleada completada.");
        }
    }

    private int GetEnemiesForWave(int wave)
    {
        int count = Mathf.Min(3 + (wave - 1) * 2, 50);
        Debug.Log($"[WaveManager] Oleada {wave}: se generarán {count} enemigos.");
        return count;
    }
}
