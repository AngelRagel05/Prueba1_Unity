using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Spawns y Prefabs")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject soldierPrefab;
    [SerializeField] private GameObject sergeantPrefab;
    [SerializeField] private GameObject lieutenantPrefab;
    [SerializeField] private GameObject colonelPrefab;

    [Header("Configuración de Oleadas")]
    [SerializeField] private int enemiesInFirstWave;
    [SerializeField] private int maxEnemiesPerWave;
    [SerializeField] private float enemySpawnDelay;
    [SerializeField] private float waveDelay;
    [SerializeField] private int waveEnemyIncrement;

    private int currentWave = 1;
    private int enemiesRemaining;
    private bool waveInProgress = false;
    private bool gameStarted = false; // ← NUEVO

    public int CurrentWave => currentWave;

    // NO iniciar en Start(), esperar llamada manual
    // private void Start() <- BORRAR ESTO
    // {
    //     StartCoroutine(SpawnNextWave());
    // }

    // Método público para iniciar el juego
    public void StartGame()
    {
        if (gameStarted) return; // Evitar múltiples llamadas
        
        gameStarted = true;
        StartCoroutine(SpawnNextWave());
    }

    private IEnumerator SpawnNextWave()
    {
        if (waveInProgress)
        {
            Debug.LogWarning("[WaveManager] Ya hay una oleada en progreso, ignorando llamada duplicada.");
            yield break;
        }

        waveInProgress = true;

        if (UIManager.Instance != null)
            UIManager.Instance.UpdateWaveText(currentWave);

        int enemiesToSpawn = GetEnemiesForWave(currentWave);
        enemiesRemaining = enemiesToSpawn;

        Debug.Log($"[WaveManager] === OLEADA {currentWave} === ({enemiesToSpawn} enemigos)");

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemyForWave(currentWave);
            yield return new WaitForSeconds(enemySpawnDelay);
        }

        while (enemiesRemaining > 0)
        {
            yield return null;
        }

        Debug.Log($"[WaveManager] Oleada {currentWave} completada.");
        waveInProgress = false;
        currentWave++;

        if (currentWave <= 20)
        {
            Debug.Log($"[WaveManager] Esperando {waveDelay}s antes de la siguiente oleada.");
            yield return new WaitForSeconds(waveDelay);

            if (UIManager.Instance != null)
                UIManager.Instance.UpdateWaveText(currentWave);

            StartCoroutine(SpawnNextWave());
        }
        else
        {
            Debug.Log("[WaveManager] Todas las oleadas completadas. ¡Victoria!");
            if (UIManager.Instance != null)
                UIManager.Instance.UpdateWaveText(-1);
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
            enemyAI.SetWaveManager(this);
    }

    public void EnemyDied()
    {
        enemiesRemaining--;
    }

    private int GetEnemiesForWave(int wave)
    {
        return Mathf.Min(enemiesInFirstWave + (wave - 1) * waveEnemyIncrement, maxEnemiesPerWave);
    }
}