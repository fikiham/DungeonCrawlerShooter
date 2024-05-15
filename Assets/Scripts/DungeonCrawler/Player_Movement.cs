using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_Movement : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 movement;
    TrailRenderer tr;

    KeyCode dashInput = KeyCode.Space;
    KeyCode runInput = KeyCode.LeftShift;

    [SerializeField] Transform face;

    [Header("SPEEDS")]
    float moveSpd;
    [SerializeField] float walkSpd = 5f;
    [SerializeField] float runSpd = 9f;

    [Header("DASH")]
    [SerializeField] float dashStamina = 40f;
    [SerializeField] float dashDistance = 5;
    [SerializeField] float dashForce = 100;
    bool dashing = false;

    bool noMovement = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponentInChildren<TrailRenderer>();
    }

    void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    // Handle all player input regarding movement (axis, run, dash)
    void PlayerInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        if (movement != Vector2.zero)
            face.localPosition = movement;

        if (Input.GetKeyDown(dashInput) && !dashing)
        {
            if (Player_Health.Instance.SpendStamina(dashStamina))
                dashing = true;
        }

        // Change moveSpd depending on if running or not
        moveSpd = Input.GetKey(runInput) ? runSpd : walkSpd;
    }

    void HandleMovement()
    {
        if (!noMovement)
        {
            if (dashing)
            {
                StartCoroutine(StartDashing(transform.position));
            }
            else
            {
                rb.AddForce(100f * moveSpd * Time.deltaTime * movement, ForceMode2D.Impulse);

                // Speed Control
                if (rb.velocity.magnitude > moveSpd)
                {
                    rb.velocity = rb.velocity.normalized * moveSpd;
                }

                // Stops player if no input given
                if (movement == Vector2.zero)
                    rb.velocity = Vector2.zero;
            }
        }
    }
    IEnumerator StartDashing(Vector2 startPos)
    {
        noMovement = true;
        Vector2 targetDir = (face.position - transform.position).normalized;
        float startTime = Time.time;
        tr.enabled = true;

        while (Vector2.Distance(startPos, transform.position) < dashDistance && Time.time < startTime + 1)
        {
            rb.AddForce(dashForce * Time.deltaTime * targetDir, ForceMode2D.Impulse);
            yield return null;
        }
        tr.enabled = false;
        rb.velocity = Vector2.zero;
        noMovement = false;
        dashing = false;
    }
}
