using UnityEngine;

public class Jugador : MonoBehaviour
{

    Rigidbody rb;
    [SerializeField] private float movementSpeed = 0.1f;
    float moveDirectionVertical;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        moveDirectionVertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (moveDirectionVertical > 0)
        {
            rb.AddForce(Vector3.forward * movementSpeed, ForceMode.VelocityChange);
        }
    }
}
