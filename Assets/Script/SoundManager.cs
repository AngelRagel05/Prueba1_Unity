using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;     // Para la música (loop)
    public AudioSource sfxSource;       // Para efectos (OneShots)

    [Header("Música del Juego (se reproducen en orden y luego repiten)")]
    public List<AudioClip> gameplayMusic = new List<AudioClip>();

    [Header("Música de Menú / Pausa")]
    public AudioClip menuMusic;

    [Header("SFX")]
    public AudioClip shootSound;
    public AudioClip dashSound;
    public AudioClip playerDeathSound;
    public AudioClip enemyDeathSound;

    private int currentTrackIndex = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayMenuMusic();
    }

    // ---------------------------------------------------------
    // ---------------------- MÚSICA ---------------------------
    // ---------------------------------------------------------

    public void PlayMenuMusic()
    {
        if (menuMusic == null) return;

        musicSource.clip = menuMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StartGameplayMusic()
    {
        if (gameplayMusic.Count == 0) return;

        currentTrackIndex = 0;
        StartCoroutine(PlayMusicLoop());
    }

    private IEnumerator PlayMusicLoop()
    {
        while (true)
        {
            musicSource.clip = gameplayMusic[currentTrackIndex];
            musicSource.Play();

            yield return new WaitForSeconds(musicSource.clip.length);

            currentTrackIndex++;
            if (currentTrackIndex >= gameplayMusic.Count)
                currentTrackIndex = 0;
        }
    }

    // ---------------------------------------------------------
    // ---------------------- EFECTOS ---------------------------
    // ---------------------------------------------------------

    public void PlayShoot()
    {
        if (shootSound != null)
            sfxSource.PlayOneShot(shootSound);
    }

    public void PlayDash()
    {
        if (dashSound != null)
            sfxSource.PlayOneShot(dashSound);
    }

    public void PlayPlayerDeath()
    {
        if (playerDeathSound != null)
            sfxSource.PlayOneShot(playerDeathSound);
    }

    public void PlayEnemyDeath()
    {
        if (enemyDeathSound != null)
            sfxSource.PlayOneShot(enemyDeathSound);
    }
}
