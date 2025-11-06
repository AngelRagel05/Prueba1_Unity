using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;  // El jugador a seguir
    [SerializeField] private Vector3 offset = new Vector3(0, 10f, 0); // Altura de la cámara
    [SerializeField] private float smoothSpeed = 10f; // Qué tan suave sigue

    private void LateUpdate()
    {
        if (target == null) return;

        // La posición deseada es la del jugador + el offset
        Vector3 desiredPosition = target.position + offset;

        // Interpolación suave
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Si quieres que la cámara siempre mire hacia abajo, fuerza su rotación:
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }
}
