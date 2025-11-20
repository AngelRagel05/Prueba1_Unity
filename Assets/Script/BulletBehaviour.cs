using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float bulletSpeed = 50f;
    [SerializeField] float lifeTime = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Lanza la bala hacia adelante según su rotación
        rb.AddForce(transform.forward * bulletSpeed, ForceMode.VelocityChange);

        // Se destruye sola tras unos segundos por seguridad
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
        if (enemy != null)
        {
            enemy.TakeDamage(50); // O la cantidad que quieras
            Destroy(gameObject);  // Destruye la bala
        }
    }
}
