using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject settingsPanel;
    public GameObject previousPanel; // El panel desde el que vienes (menu o pausa)

    private void Start()
    {
        // Asegurarse de que el panel esté oculto al iniciar
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void OpenSettings(GameObject callerPanel)
    {
        // Guardar referencia del panel anterior
        previousPanel = callerPanel;

        // Ocultar el panel anterior
        if (previousPanel != null)
            previousPanel.SetActive(false);

        // Mostrar panel de ajustes
        if (settingsPanel != null)
            settingsPanel.SetActive(true);

        Debug.Log("Panel de ajustes abierto.");
    }

    public void CloseSettings()
    {
        // Ocultar panel de ajustes
        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        // Mostrar el panel anterior (menú o pausa)
        if (previousPanel != null)
            previousPanel.SetActive(true);

        Debug.Log("Panel de ajustes cerrado.");
    }
}
