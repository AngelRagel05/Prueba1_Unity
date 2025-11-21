using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Atributos de vida del jugador")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    [Header("Detección de caída")]
    [SerializeField] private float deathHeight = -10f;

    [Header("Invulnerabilidad")]
    [SerializeField] private float invulnerabilityDuration = 1f;
    private bool isInvulnerable = false;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public bool IsInvulnerable => isInvulnerable;

    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (!isDead && transform.position.y < deathHeight)
        {
            Debug.Log("[PlayerHealth] Player cayó fuera del mapa!");
            Die();
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead || isInvulnerable) return;

        currentHealth -= amount;
        Debug.Log($"[PlayerHealth] Daño recibido: {amount}. Vida actual: {currentHealth}");

        StartCoroutine(InvulnerabilityCoroutine());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        Debug.Log("[PlayerHealth] Invulnerabilidad activada!");

        // Opcional: efecto visual de parpadeo
        StartCoroutine(BlinkEffect());

        yield return new WaitForSeconds(invulnerabilityDuration);

        isInvulnerable = false;
        Debug.Log("[PlayerHealth] Invulnerabilidad terminada.");
    }

    private IEnumerator BlinkEffect()
    {
        Renderer renderer = GetComponentInChildren<Renderer>();
        if (renderer == null) yield break;

        float blinkInterval = 0.1f;
        float elapsed = 0f;

        while (elapsed < invulnerabilityDuration)
        {
            renderer.enabled = !renderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        renderer.enabled = true; // Asegurar que quede visible
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        currentHealth = 0;

        Debug.Log("[PlayerHealth] Player ha muerto!");
        StartCoroutine(DieRoutine());
    }

    private IEnumerator DieRoutine()
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlayPlayerDeath();

        if (SoundManager.Instance != null && SoundManager.Instance.playerDeathSound != null)
            yield return new WaitForSeconds(SoundManager.Instance.playerDeathSound.length);
        else
            yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}