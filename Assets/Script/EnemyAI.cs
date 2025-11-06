using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;
    private Transform player;
    private Rigidbody rb;
    private WaveManager waveManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    public void SetWaveManager(WaveManager wm)
    {
        waveManager = wm;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    public void Die()
    {
        if (waveManager != null)
            waveManager.EnemyDied();

        Destroy(gameObject);
    }

    // Detectar colisión con bala (por seguridad doble)
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Die();
            Destroy(collision.gameObject);
        }
    }
}
