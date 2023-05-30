using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    public float jumpVelocity = 5.0f;
    public float groundCheckDistance = 0.1f;
    private Rigidbody2D rb;
    private bool canDoubleJump = true;
    private float height;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        height = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        // Calculate the starting point of the raycast
        Vector2 raycastOrigin = (Vector2)transform.position - (height / 2) * Vector2.up;

        // Use a raycast straight downwards from the base of the player to check for ground
        bool isGrounded = Physics2D.Raycast(raycastOrigin, Vector2.down, groundCheckDistance);

        // Draw a debug ray. Note that this will be visible in Scene view, not in Game view.
        Debug.DrawRay(raycastOrigin, Vector2.down * groundCheckDistance, Color.red);

        // If the player is standing on the ground, reset the jump count
        if (isGrounded)
        {
            canDoubleJump = true;
        }

        // If the player presses the Space key and hasn't jumped the maximum amount of times, they should jump
        if (Input.GetButtonDown("Jump") && !isGrounded && canDoubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            canDoubleJump = false;
        }
    }
}

