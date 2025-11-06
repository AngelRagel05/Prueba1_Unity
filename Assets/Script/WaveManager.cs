using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int baseEnemiesPerWave = 3;
    [SerializeField] private int maxWaves = 20;
    [SerializeField] private float spawnDelay = 0.5f;
    [SerializeField] private float timeBetweenWaves = 4f;

    [Header("Spawn Points")]
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

    private int currentWave = 0;
    private int enemiesAlive = 0;
    private bool spawning = false;

    void Start()
    {
        StartCoroutine(StartNextWave());
    }

    private IEnumerator StartNextWave()
    {
        if (currentWave >= maxWaves)
        {
            Debug.Log("🏁 Has completado todas las oleadas. ¡Victoria!");
            yield break;
        }

        currentWave++;
        spawning = true;

        Debug.Log($"🔵 Iniciando oleada {currentWave}...");

        // Aumenta la cantidad de enemigos por oleada (3, 5, 7, ...)
        int enemiesToSpawn = baseEnemiesPerWave + (currentWave - 1) * 2;

        // Spawnea los enemigos uno a uno
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }

        spawning = false;
        Debug.Log($"🟡 Oleada {currentWave} iniciada con {enemiesAlive} enemigos.");
    }

    private void SpawnEnemy()
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogWarning("❌ No hay puntos de spawn asignados en el WaveManager.");
            return;
        }

        Transform randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Count)];
        GameObject enemy = Instantiate(enemyPrefab, randomSpawn.position, Quaternion.identity);

        // Escalar dificultad: más velocidad o vida por oleada
        EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
        if (enemyAI != null)
        {
            enemyAI.speed += currentWave * 0.2f; // Más velocidad cada ronda
            enemyAI.SetWaveManager(this);
        }

        enemiesAlive++;
    }

    public void EnemyDied()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0 && !spawning)
        {
            Debug.Log($"✅ Oleada {currentWave} completada.");
            Invoke(nameof(PrepareNextWave), timeBetweenWaves);
        }
    }

    private void PrepareNextWave()
    {
        StartCoroutine(StartNextWave());
    }
}
