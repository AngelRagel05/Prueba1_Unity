using UnityEngine;

public class Shoot : MonoBehaviour
{

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform ShootPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnBullet();
        }
    }

    private void SpawnBullet()
    {
        // Instanciamos la bala desde el punto de disparo
        var bullet = Instantiate(bulletPrefab, ShootPoint.position, ShootPoint.rotation);

        // Ignoramos la colisión entre la bala y el propio jugador
        Collider playerCollider = GetComponent<Collider>();
        Collider bulletCollider = bullet.GetComponent<Collider>();

        if (playerCollider != null && bulletCollider != null)
        {
            Physics.IgnoreCollision(bulletCollider, playerCollider);
        }
    }
}
