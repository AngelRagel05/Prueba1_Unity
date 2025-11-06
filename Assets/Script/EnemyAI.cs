using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyType { Soldier, Sergeant, Lieutenant, Colonel }
    public EnemyType enemyType;

    private Transform player;
    private WaveManager waveManager;
    private PlayerHealth playerHealth;

    public float speed = 3f;
    public float damage = 10f;
    public float maxHealth = 50f;
    private float currentHealth;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        currentHealth = maxHealth;

        // Ajustar stats según el tipo de enemigo
        switch (enemyType)
        {
            case EnemyType.Soldier:
                speed = 6f;
                damage = 5f;
                maxHealth = 50f;
                break;
            case EnemyType.Sergeant:
                speed = 10f;
                damage = 15f;
                maxHealth = 75f;
                break;
            case EnemyType.Lieutenant:
                speed = 20f;
                damage = 25f;
                maxHealth = 100f;
                break;
            case EnemyType.Colonel:
                speed = 30f;
                damage = 50f;
                maxHealth = 150f;
                break;
        }
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (player == null) return;
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        transform.LookAt(player.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(25f);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(damage);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        waveManager?.EnemyKilled();
        Destroy(gameObject);
    }

    public void SetWaveManager(WaveManager manager)
    {
        waveManager = manager;
    }
}
