using UnityEngine;
using UnityEngine.SceneManagement; // Solo si quieres reiniciar la escena al morir

public class PlayerHealth : MonoBehaviour
{
    [Header("Atributos de vida del jugador")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        Debug.Log($"[PlayerHealth] Vida inicial: {currentHealth}");
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"[PlayerHealth] Daño recibido: {amount}, vida restante: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("[PlayerHealth] 💀 El jugador ha muerto!");

        // Ejemplo: reiniciar la escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
