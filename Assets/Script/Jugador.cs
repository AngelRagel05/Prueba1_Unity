using System.Collections;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 movementDirection;

    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 10f;

    [Header("Dash")]
    [SerializeField] private float dashForce = 30f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 5f;

    private bool dashReadyLogged = false;

    private bool isDashing = false;
    private TrailRenderer trail;

    // Barra de dash
    public bool CanDash => TimeUntilDash <= 0f;
    public float TimeUntilDash { get; private set; } // tiempo restante
    public float DashCooldown => dashCooldown;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
        if (trail != null) trail.emitting = false;

        TimeUntilDash = 0f; // dash listo al inicio
    }

    void Update()
    {
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.z = Input.GetAxisRaw("Vertical");
        movementDirection.Normalize();

        if (Input.GetKeyDown(KeyCode.Space) && CanDash && movementDirection != Vector3.zero)
        {
            StartCoroutine(Dash());
            dashReadyLogged = false; // reseteamos el flag cuando usamos el dash
        }

        if (TimeUntilDash > 0f)
        {
            TimeUntilDash -= Time.deltaTime;
            if (TimeUntilDash < 0f)
                TimeUntilDash = 0f;
        }

        // Log solo la primera vez que el dash se vuelve listo
        if (CanDash && !dashReadyLogged)
        {
            Debug.Log("[Jugador] Dash listo!");
            dashReadyLogged = true;
        }

    }
    
    void FixedUpdate()
    {
        if (!isDashing)
            rb.linearVelocity = movementDirection * moveSpeed;
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        TimeUntilDash = dashCooldown; // inicia cooldown
        Debug.Log("[Jugador] Dash usado! Iniciando cooldown.");

        if (trail != null)
            trail.emitting = true;

        Vector3 dashDir = movementDirection;
        if (dashDir == Vector3.zero) dashDir = transform.forward;

        rb.linearVelocity = dashDir * dashForce;

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        if (trail != null)
            trail.emitting = false;
    }
}
