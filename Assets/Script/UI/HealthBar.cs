using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image rellenoBarraVida; // La parte "Fill" de la barra
    private PlayerHealth playerHealth;
    private float vidaMaxima;

    void Start()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        if (playerHealth != null)
            vidaMaxima = playerHealth.MaxHealth;
        else
            Debug.LogWarning("[BarraVida] No se encontró PlayerHealth en el Player.");
    }

    void Update()
    {
        if (playerHealth != null)
        {
            rellenoBarraVida.fillAmount = (float)playerHealth.CurrentHealth / vidaMaxima;
            Debug.Log($"[BarraVida] Vida actual: {playerHealth.CurrentHealth}/{vidaMaxima}");
        }
    }
}
