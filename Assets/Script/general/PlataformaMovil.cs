using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    public float velocidad = 2f;       // Velocidad de subida/bajada
    public float alturaMaxima = 5f;    // Altura máxima que sube
    private Vector3 posicionInicial;
    private bool personajeEncima = false;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        float paso = velocidad * Time.deltaTime;

        if (personajeEncima)
        {
            if (transform.position.y < posicionInicial.y + alturaMaxima)
                transform.position += Vector3.up * paso;
        }
        else
        {
            if (transform.position.y > posicionInicial.y)
                transform.position -= Vector3.up * paso;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            personajeEncima = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            personajeEncima = false;
    }
}
