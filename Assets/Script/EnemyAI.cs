using UnityEngine;

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

    private void Awake()
    {
        ApplyTypeSettings();
        currentHealth = maxHealth;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (waveManager == null)
            waveManager = FindObjectOfType<WaveManager>();

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
                speed = 20f;
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
        if (player == null) return;

        Vector3 dir = (player.position - transform.position);
        dir.y = 0f;
        dir.Normalize();

        transform.position += dir * speed * Time.fixedDeltaTime;
        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(50);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Player"))
        {
            var ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(damage);
                Debug.Log($"[EnemyAI] {enemyType} hizo {damage} daño al jugador (Trigger).");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var ph = collision.gameObject.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(damage);
                Debug.Log($"[EnemyAI] {enemyType} hizo {damage} daño al jugador (Collision).");
            }
        }
    }

    // --- Sistema para evitar daño doble ---
    private bool _hasBeenHitRecently = false;

    private System.Collections.IEnumerator HandleSingleHit()
    {
        _hasBeenHitRecently = true;
        TakeDamage(50);
        yield return new WaitForSeconds(0.1f);
        _hasBeenHitRecently = false;
    }

    // --- Daño y muerte ---
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

        Destroy(gameObject);
    }
}
