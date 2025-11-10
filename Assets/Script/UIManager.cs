using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Referencias UI")]
    public Slider healthBar;
    public Slider dashBar;
    public TMP_Text waveText;

    [Header("Referencias Scripts")]
    public PlayerHealth playerHealth;
    public Jugador playerMovement;
    public WaveManager waveManager;

    void Start()
    {
        if (healthBar != null && playerHealth != null)
            healthBar.maxValue = playerHealth.MaxHealth;

        if (dashBar != null)
            dashBar.maxValue = 1f;
    }

    void Update()
    {
        if (playerHealth != null && healthBar != null)
            healthBar.value = playerHealth.CurrentHealth;

        if (playerMovement != null && dashBar != null)
        {
            float dashProgress = playerMovement.CanDash ? 1f :
                Mathf.Clamp01(playerMovement.TimeUntilDash / playerMovement.DashCooldown);
            dashBar.value = dashProgress;
        }

        if (waveManager != null && waveText != null)
            waveText.text = $"Wave {waveManager.CurrentWave}";
    }
}
