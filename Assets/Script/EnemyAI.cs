using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyType { Soldier, Sergeant, Lieutenant, Colonel }

    [Header("Tipo (selecciona en el prefab)")]
    public EnemyType enemyType = EnemyType.Soldier;

    [Header("Stats (se asignan automáticamente según el tipo)")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private int maxHealth = 50;
    [SerializeField] private int damage = 5;

    private int currentHealth;
    private Transform player;
    private WaveManager waveManager;

    private void Awake()
    {
        // Aplicamos las stats desde el tipo lo antes posible
        ApplyTypeSettings();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        currentHealth = maxHealth;

        Debug.Log($"[EnemyAI] Spawned as {enemyType} | speed={speed} health={maxHealth} damage={damage}");
    }

    // Esto permite que cuando cambies el enum en el prefab en el editor, se actualicen los campos ahí mismo
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

        // si quieres ver los cambios inmediatos en el editor:
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
        if (dir != Vector3.zero) transform.rotation = Quaternion.LookRotation(dir);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // daño fijo por ejemplo — puedes cambiarlo o usar damage
            TakeDamage(50);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            var ph = collision.gameObject.GetComponent<PlayerHealth>();
            if (ph != null)
                ph.TakeDamage(damage);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        waveManager?.EnemyDied();
        Destroy(gameObject);
    }
}
