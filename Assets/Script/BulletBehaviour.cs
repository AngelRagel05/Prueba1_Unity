using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float bulletSpeed = 30f;
    [SerializeField] float impactForce = 10f;
    [SerializeField] float lifeTime = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Lanza la bala hacia adelante según su rotación
        rb.AddForce(transform.forward * bulletSpeed, ForceMode.VelocityChange);

        // Se destruye sola tras unos segundos para no dejar basura en la escena
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Si choca con un enemigo, aplicamos fuerza sin destruir la bala
        if (other.CompareTag("Enemy"))
        {
            Rigidbody enemyRb = other.attachedRigidbody;

            if (enemyRb != null)
            {
                // Empuja al enemigo en la dirección de la bala
                Vector3 direction = transform.forward;
                enemyRb.AddForce(direction * impactForce, ForceMode.Impulse);
            }
        }
    }
}
