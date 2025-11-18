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
        Time.timeScale = 0f; // Pausa la física y animaciones
        pausePanel.SetActive(true);
        gameplayUI.SetActive(false);
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false);

        SoundManager.Instance.PlayMenuMusic();

        Debug.Log("Juego pausado.");
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Reanuda todo
        pausePanel.SetActive(false);
        gameplayUI.SetActive(true);
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false);

        SoundManager.Instance.PlayMenuMusic();

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
