using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip deathSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void playDeath()
    {
        audioSource.PlayOneShot(deathSound);
    }
}
