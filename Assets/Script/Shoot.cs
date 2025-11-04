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
        Debug.Log("Disparo perra.");
        Instantiate(bulletPrefab, ShootPoint.position, ShootPoint.rotation);
    }
}
