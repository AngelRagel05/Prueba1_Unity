using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Referencias UI")]
    public Slider healthBar;
    public Slider dashBar;
    public Image dashFill; // Fill de la barra
    public TMP_Text waveText;

    [Header("Referencias Scripts")]
    public PlayerHealth playerHealth;
    public Jugador playerMovement;
    public WaveManager waveManager;

    [Header("Colores Dash")]
    public Color dashReadyColor = new Color(1f, 0.9f, 0.1f);  // amarillo brillante
    public Color dashChargingColor = new Color(1f, 0.5f, 0f); // naranja

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (healthBar != null && playerHealth != null)
            healthBar.maxValue = playerHealth.MaxHealth;

        if (dashBar != null)
            dashBar.maxValue = 1f;

        if (waveManager != null && waveText != null)
            waveText.text = $"Wave {waveManager.CurrentWave}";

        if (dashFill != null)
            dashFill.color = dashReadyColor;
    }

    void Update()
    {
        // VIDA
        if (playerHealth != null && healthBar != null)
            healthBar.value = playerHealth.CurrentHealth;

        // DASH
        if (playerMovement != null && dashBar != null)
        {
            // Barra = 0 cuando recién usado, 1 cuando listo
            float dashProgress = Mathf.Clamp01(1f - playerMovement.TimeUntilDash / playerMovement.DashCooldown);
            dashBar.value = dashProgress;

            // Cambio de color
            if (dashFill != null)
                dashFill.color = dashProgress >= 1f ? dashReadyColor : dashChargingColor;

            // Log del estado
            Debug.Log($"[UIManager] DashBar = {dashBar.value:F2} | Color = {(dashProgress >= 1f ? "Ready" : "Charging")}");
        }

        // OLEADAS
        if (waveManager != null && waveText != null)
            waveText.text = $"Wave {waveManager.CurrentWave}";
    }

    public void UpdateWaveText(int waveNumber)
    {
        if (waveText == null) return;

        if (waveNumber <= 0)
            waveText.text = "¡Victoria!";
        else
            waveText.text = $"Wave {waveNumber}";
    }
}
