using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [Header("Prefabs de enemigos")]
    [SerializeField] private GameObject soldierPrefab;
    [SerializeField] private GameObject sergeantPrefab;
    [SerializeField] private GameObject lieutenantPrefab;
    [SerializeField] private GameObject colonelPrefab;

    [Header("Spawns")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Configuración de oleadas")]
    [SerializeField] private int startingEnemies = 3;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private int maxWaves = 20;

    private int currentWave = 0;
    private int enemiesAlive = 0;
    private bool spawning = false;

    void Update()
    {
        if (enemiesAlive == 0 && !spawning && currentWave < maxWaves)
        {
            StartCoroutine(SpawnWave());
        }
    }

    private IEnumerator SpawnWave()
    {
        spawning = true;
        currentWave++;
        Debug.Log($"Oleada {currentWave} iniciada.");

        // Cada ronda tiene más enemigos (aumenta 2 por ronda)
        int enemiesThisWave = startingEnemies + (currentWave - 1) * 2;

        for (int i = 0; i < enemiesThisWave; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemyPrefab = GetEnemyTypeForWave(currentWave);
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            EnemyAI ai = enemy.GetComponent<EnemyAI>();
            if (ai != null)
            {
                ai.SetWaveManager(this);

                // Escalado de dificultad (vida + daño según la ronda)
                ai.maxHealth += (currentWave - 1) * 5;
                ai.damage += (currentWave - 1);
                ai.speed += (currentWave - 1) * 0.05f;
            }

            enemiesAlive++;
            yield return new WaitForSeconds(0.2f);
        }

        spawning = false;
    }

    // 🧮 Determina el tipo de enemigo según la oleada
    private GameObject GetEnemyTypeForWave(int wave)
    {
        if (wave <= 5) return soldierPrefab; // 1-5 solo Soldier
        if (wave <= 10)
            return Random.value < 0.7f ? soldierPrefab : sergeantPrefab;
        if (wave <= 15)
            return Random.value < 0.6f ? sergeantPrefab : lieutenantPrefab;
        if (wave <= 20)
        {
            float r = Random.value;
            if (r < 0.4f) return lieutenantPrefab;
            if (r < 0.8f) return colonelPrefab;
            return sergeantPrefab;
        }

        return soldierPrefab;
    }

    public void EnemyDied()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0)
        {
            Debug.Log($"Oleada {currentWave} completada.");

            if (currentWave < maxWaves)
                Invoke(nameof(NextWave), timeBetweenWaves);
            else
                Debug.Log("🎉 Has sobrevivido todas las oleadas!");
        }
    }

    private void NextWave()
    {
        // Este método solo inicia la siguiente oleada (el Update lo controla)
        Debug.Log($"Preparando oleada {currentWave + 1}...");
    }
}
