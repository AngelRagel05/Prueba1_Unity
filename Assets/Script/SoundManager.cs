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
        // Validar que los AudioSource existan
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

        // Detener música anterior
        if (musicCoroutine != null)
        {
            StopCoroutine(musicCoroutine);
            musicCoroutine = null;
        }

        musicSource.clip = menuMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StartGameplayMusic()
    {
        if (gameplayMusic.Count == 0 || musicSource == null) return;

        currentTrackIndex = 0;
        
        if (musicCoroutine != null)
            StopCoroutine(musicCoroutine);
            
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
            musicSource.Play();

            yield return new WaitForSeconds(musicSource.clip.length);

            currentTrackIndex++;
            if (currentTrackIndex >= gameplayMusic.Count)
                currentTrackIndex = 0;
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