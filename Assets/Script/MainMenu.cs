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
        menuPanel.SetActive(true);
        gameplayUI.SetActive(false);
        if (player != null) player.SetActive(false);
        SoundManager.Instance.PlayMenuMusic();
    }

    public void Jugar()
    {
        Debug.Log("[MainMenu] Jugar presionado, iniciando partida...");
        Time.timeScale = 1f;
        menuPanel.SetActive(false);
        gameplayUI.SetActive(true);
        if (player != null) player.SetActive(true);

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
