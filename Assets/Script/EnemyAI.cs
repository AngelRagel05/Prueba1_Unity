using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyType { Soldier, Sergeant, Lieutenant, Colonel }
    [Header("Tipo de enemigo")]
    public EnemyType enemyType = EnemyType.Soldier;

    [Header("Estadísticas")]
    public float moveSpeed;
    public float damage;
    public float maxHealth;
    private float currentHealth;

    private Transform player;
    private Rigidbody rb;
    private WaveManager waveManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        waveManager = FindFirstObjectByType<WaveManager>();

        // Ajusta los valores según el tipo de enemigo
        switch (enemyType)
        {
            case EnemyType.Soldier:
                moveSpeed = 3f;
                damage = 5f;
                maxHealth = 20f;
                break;
            case EnemyType.Sergeant:
                moveSpeed = 3.5f;
                damage = 10f;
                maxHealth = 35f;
                break;
            case EnemyType.Lieutenant:
                moveSpeed = 4f;
                damage = 15f;
                maxHealth = 50f;
                break;
            case EnemyType.Colonel:
                moveSpeed = 4.5f;
                damage = 25f;
                maxHealth = 70f;
                break;
        }

        currentHealth = maxHealth;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position);
        direction.y = 0f;
        direction.Normalize();

        rb.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);

        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(15); // daño fijo por bala
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            // El jugador recibe daño al tocarlo
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
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

    void Die()
    {
        if (waveManager != null)
            waveManager.EnemyDied();

        Destroy(gameObject);
    }
}
