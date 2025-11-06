using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject soldierPrefab;
    public GameObject sergeantPrefab;
    public GameObject lieutenantPrefab;
    public GameObject colonelPrefab;

    private int currentWave = 0;
    private int enemiesAlive = 0;

    private void Start()
    {
        StartCoroutine(SpawnNextWave());
    }

    public void EnemyKilled()
    {
        enemiesAlive--;
        if (enemiesAlive <= 0)
        {
            StartCoroutine(SpawnNextWave());
        }
    }

    private IEnumerator SpawnNextWave()
    {
        currentWave++;
        if (currentWave > 20)
        {
            Debug.Log("Has sobrevivido a todas las oleadas!");
            yield break;
        }

        int enemyCount = Mathf.RoundToInt(3 + currentWave * 1.5f);
        enemiesAlive = enemyCount;
        Debug.Log("Wave " + currentWave + " spawning " + enemyCount + " enemies.");

        for (int i = 0; i < enemyCount; i++)
        {
            yield return new WaitForSeconds(0.3f);
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject prefab = ChooseEnemyType();
            GameObject enemy = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            EnemyAI ai = enemy.GetComponent<EnemyAI>();
            ai.SetWaveManager(this);
        }
    }

    private GameObject ChooseEnemyType()
    {
        float r = Random.value;
        if (currentWave < 5) return soldierPrefab;
        if (currentWave < 10) return r < 0.7f ? soldierPrefab : sergeantPrefab;
        if (currentWave < 15) return r < 0.5f ? sergeantPrefab : lieutenantPrefab;
        if (currentWave < 20) return r < 0.4f ? lieutenantPrefab : colonelPrefab;
        return colonelPrefab;
    }
}
