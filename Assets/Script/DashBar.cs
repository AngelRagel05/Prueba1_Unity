using UnityEngine;
using UnityEngine.UI;

public class DashBar : MonoBehaviour
{
    [Header("UI")]
    public Image rellenoBarraDash;
    public Jugador jugador;

    [Header("Colores")]
    public Color dashReadyColor = Color.yellow; // cuando el dash está listo
    public Color dashChargingColor = Color.blue; // cuando está recargando

    private float dashCooldown;

    void Start()
    {
        if (jugador == null)
        {
            GameObject jugadorObj = GameObject.Find("Player");
            if (jugadorObj != null)
                jugador = jugadorObj.GetComponent<Jugador>();
        }

        if (jugador == null)
        {
            Debug.LogError("[DashBar] No se encontró el jugador!");
        }

        if (rellenoBarraDash == null)
        {
            Debug.LogError("[DashBar] No se ha asignado el Image Fill en el Inspector!");
        }

        dashCooldown = jugador != null ? jugador.DashCooldown : 5f;
    }

    void Update()
    {
        if (jugador == null || rellenoBarraDash == null) return;

        float progreso = Mathf.Clamp01(1f - jugador.TimeUntilDash / dashCooldown);

        rellenoBarraDash.fillAmount = progreso;

        // Cambia color según estado
        rellenoBarraDash.color = progreso >= 1f ? dashReadyColor : dashChargingColor;

        if (progreso >= 1f)
        {
            Debug.Log("[DashBar] Dash listo!");
        }
    }
}
