using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private Transform player;
    private Rigidbody rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // Dirección horizontal hacia el jugador
        Vector3 direction = (player.position - transform.position);
        direction.y = 0f;
        direction.Normalize();

        // Movimiento directo
        rb.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);

        // Rotar hacia el jugador (opcional, pero queda mejor)
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Si la bala toca al enemigo, lo destruimos
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject); // destruye la bala
            Destroy(gameObject);           // destruye el enemigo
        }
    }
}
