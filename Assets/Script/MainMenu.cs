using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject menuPanel;
    public GameObject gameplayUI;
    public GameObject player;

    private void Start()
    {
        // Solo inicializa la UI, no pausamos el juego todavía
        if (menuPanel != null)
            menuPanel.SetActive(true);
        else
            Debug.LogError("menuPanel no asignado en MainMenu!");

        if (gameplayUI != null)
            gameplayUI.SetActive(false);
        else
            Debug.LogError("gameplayUI no asignado en MainMenu!");

        if (player != null)
            player.SetActive(false);

        // Reproducir música de menú si SoundManager existe
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlayMenuMusic();
        else
            Debug.LogWarning("SoundManager no encontrado - el juego continuará sin audio");

        // Pausar el juego solo cuando mostramos el menú
        PauseGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; // Pausa la física y animaciones
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // Reanuda todo
    }

    public void Jugar()
    {
        Debug.Log("[MainMenu] Jugar presionado, iniciando partida...");

        Time.timeScale = 1f; // Asegurarse de que el juego no esté pausado

        menuPanel.SetActive(false);
        gameplayUI.SetActive(true);
        if (player != null) player.SetActive(true);

        if (SoundManager.Instance != null)
            SoundManager.Instance.StartGameplayMusic();
    }

    public void Ajustes()
    {
        Debug.Log("[MainMenu] Ajustes presionado (aún no implementado)");
    }

    public void Salir()
    {
        Debug.Log("[MainMenu] Saliendo del juego...");
        Application.Quit();
    }
}
