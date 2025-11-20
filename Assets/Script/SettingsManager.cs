using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject settingsPanel;
    public GameObject menuPanel;
    public GameObject pausePanel;

    private GameObject previousPanel; // Guarda de dónde vienes

    private void Start()
    {
        // Asegurarse de que el panel esté oculto al iniciar
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void AbrirDesdeMenu()
    {
        previousPanel = menuPanel;
        CambiarPanel(menuPanel, settingsPanel);
    }

    public void AbrirDesdePausa()
    {
        previousPanel = pausePanel;
        CambiarPanel(pausePanel, settingsPanel);
    }

    public void Volver()
    {
        if (previousPanel != null)
        {
            CambiarPanel(settingsPanel, previousPanel);
            Debug.Log("Volviendo al panel anterior.");
        }
        else
        {
            Debug.LogError("No hay panel anterior guardado!");
        }
    }

    private void CambiarPanel(GameObject panelActual, GameObject panelNuevo)
    {
        if (panelActual != null)
            panelActual.SetActive(false);
        
        if (panelNuevo != null)
            panelNuevo.SetActive(true);
    }
}
