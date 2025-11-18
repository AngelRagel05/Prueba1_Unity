using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Música del Juego")]
    public List<AudioClip> gameplayMusic = new List<AudioClip>();

    [Header("Música de Menú / Pausa")]
    public AudioClip menuMusic;

    [Header("SFX")]
    public AudioClip shootSound;
    public AudioClip dashSound;
    public AudioClip playerDeathSound;
    public AudioClip enemyDeathSound;

    private int currentTrackIndex = 0;
    private Coroutine musicCoroutine;

    // Variables para guardar el estado de la música
    private bool wasPlayingGameplayMusic = false;
    private float savedMusicTime = 0f;
    private int savedTrackIndex = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (musicSource == null)
        {
            Debug.LogWarning("SoundManager: musicSource no asignado!");
            musicSource = gameObject.AddComponent<AudioSource>();
        }

        if (sfxSource == null)
        {
            Debug.LogWarning("SoundManager: sfxSource no asignado!");
            sfxSource = gameObject.AddComponent<AudioSource>();
        }

        PlayMenuMusic();
    }

    public void PlayMenuMusic()
    {
        if (menuMusic == null || musicSource == null) return;

        // Detener la corrutina de gameplay si está activa
        if (musicCoroutine != null)
        {
            StopCoroutine(musicCoroutine);
            musicCoroutine = null;
        }

        musicSource.Stop();
        musicSource.clip = menuMusic;
        musicSource.loop = true;
        musicSource.Play();

        wasPlayingGameplayMusic = false; // Solo aquí reseteamos
    }

    public void PlayPauseMusic()
    {
        if (menuMusic == null || musicSource == null) return;

        // NO detener la corrutina ni cambiar wasPlayingGameplayMusic
        // Solo cambiar temporalmente a música de menú
        musicSource.Stop();
        musicSource.clip = menuMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StartGameplayMusic()
    {
        if (gameplayMusic.Count == 0 || musicSource == null) return;

        currentTrackIndex = 0;
        savedTrackIndex = 0;
        savedMusicTime = 0f;

        if (musicCoroutine != null)
            StopCoroutine(musicCoroutine);

        wasPlayingGameplayMusic = true;
        musicCoroutine = StartCoroutine(PlayMusicLoop());
    }

    private IEnumerator PlayMusicLoop()
    {
        while (true)
        {
            if (musicSource == null || currentTrackIndex >= gameplayMusic.Count)
                yield break;

            musicSource.clip = gameplayMusic[currentTrackIndex];
            musicSource.loop = false;
            musicSource.time = savedMusicTime;
            musicSource.Play();

            savedMusicTime = 0f;

            yield return new WaitForSeconds(musicSource.clip.length - musicSource.time);

            currentTrackIndex++;
            if (currentTrackIndex >= gameplayMusic.Count)
                currentTrackIndex = 0;
        }
    }

    public void PauseMusic()
    {
        if (musicSource == null) return;

        if (musicSource.isPlaying)
        {
            if (wasPlayingGameplayMusic)
            {
                savedMusicTime = musicSource.time;
                savedTrackIndex = currentTrackIndex;
            }

            musicSource.Pause();
        }
    }

    public void ResumeMusic()
    {
        if (musicSource == null) return;

        if (wasPlayingGameplayMusic)
        {
            musicSource.Stop();
            currentTrackIndex = savedTrackIndex;

            if (musicCoroutine != null)
                StopCoroutine(musicCoroutine);

            musicCoroutine = StartCoroutine(PlayMusicLoop());
        }
        else if (!musicSource.isPlaying)
        {
            musicSource.UnPause();
        }
    }

    public void PlayShoot()
    {
        if (shootSound != null && sfxSource != null)
            sfxSource.PlayOneShot(shootSound);
    }

    public void PlayDash()
    {
        if (dashSound != null && sfxSource != null)
            sfxSource.PlayOneShot(dashSound);
    }

    public void PlayPlayerDeath()
    {
        if (playerDeathSound != null && sfxSource != null)
            sfxSource.PlayOneShot(playerDeathSound);
    }

    public void PlayEnemyDeath()
    {
        if (enemyDeathSound != null && sfxSource != null)
            sfxSource.PlayOneShot(enemyDeathSound);
    }
}