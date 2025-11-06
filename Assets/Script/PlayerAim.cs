using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;   // Cámara principal
    [SerializeField] private Transform bodyToRotate; // Parte del jugador que rota (puede ser el mismo jugador)

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (bodyToRotate == null)
            bodyToRotate = transform;
    }

    void Update()
    {
        AimAtMouse();
    }

    void AimAtMouse()
    {
        // Crea un rayo desde la cámara al punto donde está el ratón
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // Plano donde el jugador se mueve (por ejemplo el suelo, Y = 0)
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float hitDist))
        {
            Vector3 hitPoint = ray.GetPoint(hitDist);
            Vector3 direction = (hitPoint - bodyToRotate.position);
            direction.y = 0; // Evitar rotaciones verticales

            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion rotation = Quaternion.LookRotation(direction);
                bodyToRotate.rotation = rotation;
            }
        }
    }
}
