using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyType { Soldier, Sergeant, Lieutenant, Colonel }

    [Header("Tipo (selecciona en el prefab)")]
    public EnemyType enemyType = EnemyType.Soldier;

    [Header("Stats automáticos según el tipo")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private int maxHealth = 50;
    [SerializeField] private int damage = 5;

    private int currentHealth;
    private Transform player;
    private WaveManager waveManager;

    [Header("Cooldown de daño al jugador")]
    [SerializeField] private float hitCooldown = 0.5f;
    private bool _hasBeenHitRecently = false;

    private void Awake()
    {
        ApplyTypeSettings();
        currentHealth = maxHealth;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (waveManager == null)
            waveManager = Object.FindFirstObjectByType<WaveManager>();

        Debug.Log($"[EnemyAI] Spawned {enemyType} | HP={maxHealth} | Speed={speed} | Damage={damage}");
    }

    private void OnValidate()
    {
        ApplyTypeSettings();
    }

    private void ApplyTypeSettings()
    {
        switch (enemyType)
        {
            case EnemyType.Soldier:
                speed = 10f;
                maxHealth = 50;
                damage = 5;
                break;

            case EnemyType.Sergeant:
                speed = 12.5f;
                maxHealth = 100;
                damage = 10;
                break;

            case EnemyType.Lieutenant:
                speed = 15f;
                maxHealth = 150;
                damage = 20;
                break;

            case EnemyType.Colonel:
                speed = 17.5f;
                maxHealth = 250;
                damage = 50;
                break;
        }
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    public void SetWaveManager(WaveManager manager)
    {
        waveManager = manager;
    }

    private void FixedUpdate()
    {
        // Chequeo de caída
        if (transform.position.y < -10f)
        {
            Die();
        }

        if (player == null) return;

        Vector3 dir = (player.position - transform.position);
        dir.y = 0f;
        dir.Normalize();

        transform.position += dir * speed * Time.fixedDeltaTime;
        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir);
    }

    // Detectar balas (si las balas son trigger)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(50);
            Destroy(other.gameObject);
        }
    }

    // Detectar colisión con el player
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_hasBeenHitRecently)
        {
            TryDamagePlayer(collision.gameObject);
        }
    }

    // Detectar si el player se queda dentro del enemigo (durante dash como trigger)
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !_hasBeenHitRecently)
        {
            TryDamagePlayer(other.gameObject);
        }
    }

    private void TryDamagePlayer(GameObject playerObject)
    {
        var ph = playerObject.GetComponent<PlayerHealth>();
        var jugador = playerObject.GetComponent<Jugador>();

        if (ph == null) return;

        // ✅ Verificar si el jugador es invulnerable
        if (ph.IsInvulnerable)
        {
            Debug.Log($"[EnemyAI] {enemyType} no puede dañar al jugador (invulnerable tras golpe).");
            return;
        }

        // ✅ Verificar si el jugador está en dash
        if (jugador != null && jugador.IsDashing)
        {
            Debug.Log($"[EnemyAI] {enemyType} no puede dañar al jugador (está en dash).");
            return;
        }

        // Si no es invulnerable, aplicar daño
        ph.TakeDamage(damage);
        Debug.Log($"[EnemyAI] {enemyType} hizo {damage} daño al jugador.");
        StartCoroutine(HandleSingleHit());
    }

    private IEnumerator HandleSingleHit()
    {
        _hasBeenHitRecently = true;
        yield return new WaitForSeconds(hitCooldown);
        _hasBeenHitRecently = false;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"[EnemyAI] {enemyType} recibió {amount} daño. Vida restante: {currentHealth}");

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log($"[EnemyAI] {enemyType} ha muerto (notificando al WaveManager).");

        if (waveManager != null)
        {
            waveManager.EnemyDied();
            Debug.Log($"[EnemyAI] {enemyType} notificó EnemyDied() correctamente.");
        }
        else
        {
            Debug.LogWarning($"[EnemyAI] {enemyType} murió sin WaveManager asignado.");
        }

        if (SoundManager.Instance != null) 
            SoundManager.Instance.PlayEnemyDeath();

        Destroy(gameObject);
    }
}