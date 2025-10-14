using UnityEngine;

public class Jugador : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal"); // A/D o flechas
        float moveZ = Input.GetAxis("Vertical");   // W/S o flechas

        Vector3 movimiento = new Vector3(moveX, 0, moveZ) * speed;
        rb.AddForce(movimiento, ForceMode.VelocityChange);
    }
}
