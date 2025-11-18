using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform ShootPoint;
    [SerializeField] private float fireRate = 0.2f; // tiempo entre disparos (0.2 = 5 balas por segundo)

    private float nextFireTime = 0f;

    void Update()
    {
        // Si el botón izquierdo está mantenido
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            SpawnBullet();
            if (SoundManager.Instance != null) SoundManager.Instance.PlayShoot();
            nextFireTime = Time.time + fireRate; // Espera hasta el siguiente disparo
        }
    }

    private void SpawnBullet()
    {
        // Obtenemos la dirección del ratón en el plano
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float hitDist))
        {
            Vector3 hitPoint = ray.GetPoint(hitDist);

            // Dirección horizontal
            Vector3 direction = (hitPoint - ShootPoint.position);
            direction.y = 0f;
            direction.Normalize();

            // Instanciamos la bala
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            GameObject bullet = Instantiate(bulletPrefab, ShootPoint.position, rotation);

            // Aplicamos velocidad
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = direction * 50f; // velocidad ajustable
            }
        }
    }
}
