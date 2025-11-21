using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject pausePanel;
    public GameObject gameplayUI;
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;  // ← DEBE SER ESTO, NO SettingsManager

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        gameplayUI.SetActive(false);
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false);

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PauseMusic();
            SoundManager.Instance.PlayPauseMusic();
        }

        Debug.Log("Juego pausado.");
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        gameplayUI.SetActive(true);
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false);

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.musicSource.Stop();
            SoundManager.Instance.ResumeMusic();
        }

        Debug.Log("Juego reanudado.");
    }

    public void ExitToMainMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        gameplayUI.SetActive(false);
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);

        Debug.Log("Volviendo al menú principal.");
    }

    public void OpenSettings()
    {
        Debug.Log("Abriendo ajustes desde pausa...");

        if (settingsPanel != null)
        {
            SettingsManager sm = GetComponent<SettingsManager>();
            if (sm != null)
                sm.AbrirDesdePausa();
        }
        else
            Debug.LogError("settingsPanel no asignado en PauseMenu!");
    }
}