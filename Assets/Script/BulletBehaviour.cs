using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    Rigidbody rb;
    [SerializeField] float bulletSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void FixedUpdate()
    {
        rb.AddForce(Vector3.forward, ForceMode.VelocityChange);
    }
}
