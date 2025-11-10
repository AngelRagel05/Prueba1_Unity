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
    [SerializeField] private float dashCooldown = 1.5f; 

    private bool isDashing = false;
    private bool canDash = true;
    private TrailRenderer trail;

    // Para la barra de dash
    public bool CanDash => canDash;
    public float TimeUntilDash { get; private set; }
    public float DashCooldown => dashCooldown;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();

        if (trail != null)
            trail.emitting = false;

        TimeUntilDash = 0f;
    }

    void Update()
    {
        // Movimiento
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.z = Input.GetAxisRaw("Vertical");
        movementDirection.Normalize();

        // Dash
        if (Input.GetKeyDown(KeyCode.Space) && canDash && movementDirection != Vector3.zero)
        {
            StartCoroutine(Dash());
        }

        // Contador de cooldown
        if (!canDash && !isDashing)
        {
            TimeUntilDash -= Time.deltaTime;
            if (TimeUntilDash < 0f) TimeUntilDash = 0f;
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
        canDash = false;
        TimeUntilDash = dashCooldown;

        if (trail != null)
            trail.emitting = true;

        Vector3 dashDirection = movementDirection;
        if (dashDirection == Vector3.zero)
            dashDirection = transform.forward;

        rb.linearVelocity = dashDirection * dashForce;

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;

        if (trail != null)
            trail.emitting = false;

        while (TimeUntilDash > 0)
        {
            TimeUntilDash -= Time.deltaTime;
            yield return null;
        }

        canDash = true;
    }
}
