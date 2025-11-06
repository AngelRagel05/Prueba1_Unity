using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int damage = 10;
    [SerializeField] private EnemyType enemyType;

    private Transform player;
    private int currentHealth;
    private WaveManager waveManager;

    public enum EnemyType
    {
        Soldier,
        Sergeant,
        Lieutenant,
        Colonel
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;

        // Configura atributos según el tipo de enemigo
        switch (enemyType)
        {
            case EnemyType.Soldier:
                speed = 3f;
                maxHealth = 50;
                damage = 5;
                break;

            case EnemyType.Sergeant:
                speed = 3.5f;
                maxHealth = 100;
                damage = 10;
                break;

            case EnemyType.Lieutenant:
                speed = 4f;
                maxHealth = 150;
                damage = 15;
                break;

            case EnemyType.Colonel:
                speed = 4.5f;
                maxHealth = 250;
                damage = 25;
                break;
        }

        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (player == null) return;

        // Movimiento hacia el jugador
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Si toca al jugador → hace daño
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }

        // Si choca con una bala → recibe daño y muere si llega a 0
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(50); // Puedes ajustar el daño de las balas aquí
            Destroy(collision.gameObject); // Destruye la bala al impactar
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (waveManager != null)
        {
            waveManager.EnemyDied();
        }

        Destroy(gameObject);
    }

    public void SetWaveManager(WaveManager manager)
    {
        waveManager = manager;
    }
}
