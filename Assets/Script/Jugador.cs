using System.Collections;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 movementDirection;
    private PlayerHealth playerHealth;
    private Collider playerCollider;

    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 10f;

    [Header("Dash")]
    [SerializeField] private float dashForce = 30f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 5f;

    private bool dashReadyLogged = false;
    private bool isDashing = false;
    private TrailRenderer trail;

    public bool CanDash => TimeUntilDash <= 0f;
    public float TimeUntilDash { get; private set; }
    public float DashCooldown => dashCooldown;
    public bool IsDashing => isDashing;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
        playerHealth = GetComponent<PlayerHealth>();
        playerCollider = GetComponent<Collider>();
        
        if (trail != null) trail.emitting = false;

        TimeUntilDash = 0f;
    }

    void Update()
    {
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.z = Input.GetAxisRaw("Vertical");
        movementDirection.Normalize();

        if (Input.GetKeyDown(KeyCode.Space) && CanDash && movementDirection != Vector3.zero)
        {
            StartCoroutine(Dash());
            dashReadyLogged = false;
        }

        if (TimeUntilDash > 0f)
        {
            TimeUntilDash -= Time.deltaTime;
            if (TimeUntilDash < 0f)
                TimeUntilDash = 0f;
        }

        if (CanDash && !dashReadyLogged)
        {
            Debug.Log("[Jugador] Dash listo!");
            dashReadyLogged = true;
        }
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            // Mantener la velocidad Y (gravedad)
            Vector3 velocity = movementDirection * moveSpeed;
            velocity.y = rb.linearVelocity.y;
            rb.linearVelocity = velocity;
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        TimeUntilDash = dashCooldown;

        if (SoundManager.Instance != null) 
            SoundManager.Instance.PlayDash();

        Debug.Log("[Jugador] Dash usado! Atravesando enemigos.");

        // Desactivar colisiones con enemigos
        Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"), true);

        if (trail != null)
            trail.emitting = true;

        Vector3 dashDir = movementDirection;
        if (dashDir == Vector3.zero) 
            dashDir = transform.forward;

        rb.linearVelocity = dashDir * dashForce;

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        
        // Reactivar colisiones con enemigos
        Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"), false);

        if (trail != null)
            trail.emitting = false;

        Debug.Log("[Jugador] Dash terminado. Colisiones restauradas.");
    }
}