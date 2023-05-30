using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDash : MonoBehaviour
{
    public float airDashDuration = 0.5f;
    public float airDashSpeed = 20.0f;
    public float groundCheckDistance = 0.1f;
    private float height;
    private Rigidbody2D rb;
    private bool canAirDash = true;
    private bool isGrounded;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        height = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 raycastOrigin = (Vector2)transform.position - (height / 2) * Vector2.up;
        isGrounded = Physics2D.Raycast(raycastOrigin, Vector2.down, groundCheckDistance);
        Debug.DrawRay(raycastOrigin, Vector2.down * groundCheckDistance, Color.red);

        // If the player is standing on the ground, reset the jump count and allow air dash
        if (isGrounded)
        {
            canAirDash = true;
        }

        if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) && canAirDash && !isGrounded)
        {
            StartCoroutine(airDash(Input.GetKeyDown(KeyCode.Q) ? -1 : 1));
        }
    }

    IEnumerator airDash(int direction) {
        // Disable gravity and air dash
        canAirDash = false;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;

        // Perform the air dash
        float airDashEndTime = Time.time + airDashDuration;
        while (Time.time < airDashEndTime)
        {
            rb.velocity = new Vector2(airDashSpeed * direction, 0);
            yield return null;
        }

        // Re-enable gravity
        rb.gravityScale = originalGravity;
    }
}

