using UnityEngine;

public class Jugador : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float moveSpeed = 10f;

    [Header("Salud del jugador")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        currentHealth = maxHealth;
    }

    void FixedUpdate()
    {
        // Movimiento directo, sin inercia
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movementDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (movementDirection != Vector3.zero)
        {
            Vector3 newPosition = rb.position + movementDirection * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
        }
    }

    // 🩸 Función para recibir daño
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Jugador recibió daño. Salud actual: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("💀 El jugador ha muerto.");
        // Aquí puedes reiniciar la escena, mostrar pantalla de muerte, etc.
        // Por ejemplo:
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
