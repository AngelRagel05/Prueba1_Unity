using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject pausePanel;
    public GameObject gameplayUI;
    public GameObject mainMenuPanel;

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
            SoundManager.Instance.PauseMusic();           // ← PAUSAR música gameplay
            SoundManager.Instance.PlayPauseMusic();       // ← REPRODUCIR música menú (NUEVO MÉTODO)
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
            // Primero detener completamente la música del menú
            SoundManager.Instance.musicSource.Stop();
            // Luego reanudar la música de gameplay
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
        Debug.Log("Abrir ajustes (pendiente de implementar).");
        // Aquí puedes abrir tu panel de ajustes o submenú
    }
}
