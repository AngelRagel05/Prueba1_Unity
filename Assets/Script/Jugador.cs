using UnityEngine;

public class Jugador : MonoBehaviour
{
    private Rigidbody rb;
    Vector3 movementDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        movementDirection.x = Input.GetAxis("Horizontal");
        movementDirection.z = Input.GetAxis("Vertical");

    }

    void FixedUpdate()
    {
        rb.AddForce(movementDirection * 10f, ForceMode.Force);

    }
}