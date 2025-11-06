using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints; // puntos donde aparecerán
    [SerializeField] private int enemiesPerWave = 5;
    [SerializeField] private float timeBetweenWaves = 3f;

    private int currentWave = 0;
    private int enemiesAlive = 0;
    private bool spawning = false;

    void Update()
    {
        // Si no hay enemigos vivos y no está spawneando, empieza una nueva oleada
        if (enemiesAlive == 0 && !spawning)
        {
            StartCoroutine(SpawnWave());
        }
    }

    private System.Collections.IEnumerator SpawnWave()
    {
        spawning = true;
        currentWave++;
        Debug.Log($"Oleada {currentWave} iniciada.");

        for (int i = 0; i < enemiesPerWave; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            enemiesAlive++;

            // Espera un poco entre spawns
            yield return new WaitForSeconds(0.3f);
        }

        spawning = false;
    }

    public void EnemyDied()
    {
        enemiesAlive--;
        if (enemiesAlive <= 0)
        {
            Debug.Log($"Oleada {currentWave} completada.");
            Invoke(nameof(NextWave), timeBetweenWaves);
        }
    }

    private void NextWave()
    {
        enemiesPerWave += 2; // cada oleada tiene más enemigos
    }
}
