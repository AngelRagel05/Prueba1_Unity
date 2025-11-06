using UnityEngine;

public class Jugador : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float moveSpeed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // evita que se vuelque con colisiones
    }

    void FixedUpdate()
    {
        // Leer entrada del teclado
        float horizontal = Input.GetAxisRaw("Horizontal"); // sin suavizado
        float vertical = Input.GetAxisRaw("Vertical");     // sin suavizado

        Vector3 movementDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (movementDirection != Vector3.zero)
        {
            // Movimiento directo, sin aceleración ni inercia
            Vector3 newPosition = rb.position + movementDirection * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
        }
    }
}
