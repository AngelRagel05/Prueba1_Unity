using UnityEngine;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class SpawnPoint : MonoBehaviour
{
    // Este script solo sirve para mostrar un icono o esfera visual
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f); // esfera roja en la escena
    }
}
