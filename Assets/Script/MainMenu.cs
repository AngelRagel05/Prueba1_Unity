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
        // Pausa el juego al iniciar
        Time.timeScale = 0f;

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

        // Reproducir música solo si SoundManager existe
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlayMenuMusic();
        else
            Debug.LogWarning("SoundManager no encontrado - el juego continuará sin audio");
    }

    public void Jugar()
    {
        Debug.Log("[MainMenu] Jugar presionado, iniciando partida...");
        Time.timeScale = 1f;
        menuPanel.SetActive(false);
        gameplayUI.SetActive(true);
        if (player != null) player.SetActive(true);

        if (SoundManager.Instance != null) SoundManager.Instance.StartGameplayMusic();
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
