using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Atributos de vida del jugador")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            StartCoroutine(DieRoutine());
        }
    }

    private IEnumerator DieRoutine()
    {
        // 🔹 Reproduce el sonido de muerte usando SoundManager
        SoundManager.Instance.PlayPlayerDeath();

        // 🔹 Espera a que termine el sonido antes de reiniciar la escena
        if (SoundManager.Instance.playerDeathSound != null)
            yield return new WaitForSeconds(SoundManager.Instance.playerDeathSound.length);

        // 🔹 Reinicia la escena después del sonido
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
