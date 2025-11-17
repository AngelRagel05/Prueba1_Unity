using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public PlayerSoundController playerSoundController;

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
        playerSoundController.playDeath();

        // 🔹 Espera a que termine el sonido
        yield return new WaitForSeconds(playerSoundController.deathSound.length);

        // 🔹 Reinicia la escena después del sonido
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
