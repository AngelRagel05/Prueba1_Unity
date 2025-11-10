using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 movementDirection;

    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 10f;

    [Header("Dash")]
    [SerializeField] private float dashForce = 30f;     // fuerza del dash
    [SerializeField] private float dashDuration = 0.2f; // cuánto dura el dash
    [SerializeField] private float dashCooldown = 1.5f; // tiempo entre dashes

    private bool isDashing = false;
    private bool canDash = true;
    private float dashEndTime;
    private TrailRenderer trail;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>(); // <-- AQUÍ SE INICIALIZA

        if (trail != null)
            trail.emitting = false;
    }


    void Update()
    {
        // movimiento normal
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.z = Input.GetAxisRaw("Vertical");
        movementDirection.Normalize();

        // iniciar dash
        if (Input.GetKeyDown(KeyCode.Space) && canDash && movementDirection != Vector3.zero)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            // movimiento normal sin inercia
            rb.linearVelocity = movementDirection * moveSpeed;
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

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

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }


}
