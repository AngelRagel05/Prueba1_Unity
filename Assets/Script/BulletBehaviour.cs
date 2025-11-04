using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    Rigidbody rb;
    [SerializeField] float bulletSpeed;
    [SerializeField] Transform transformPointShoot; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transformPointShoot = GameObject.Find("ShootPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void FixedUpdate()
    {
        rb.AddForce(transformPointShoot.forward * bulletSpeed, ForceMode.VelocityChange);
    }
}
