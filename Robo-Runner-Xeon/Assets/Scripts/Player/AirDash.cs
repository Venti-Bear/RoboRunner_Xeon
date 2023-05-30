using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script controls the "Air Dash" mechanic of the player character.
/// The character is able to dash in the air in a specified direction (left or right) for a 
/// defined duration. The mechanic can only be performed once while the character is airborne,
/// and it can be performed again only after the character lands on the ground. The direction 
/// of the air dash is determined based on the player's input (Q for left, E for right).
/// </summary>
public class AirDash : MonoBehaviour
{
    /// <summary>
    /// Duration of the air dash.
    /// </summary>
    public float airDashDuration = 0.5f;

    /// <summary>
    /// Speed of the air dash.
    /// </summary>
    public float airDashSpeed = 20.0f;
    public PlayerConfig config;

    private float height;
    private Rigidbody2D rb;

    /// <summary>
    /// Flag to check if the character can air dash.
    /// </summary>
    private bool canAirDash = true;

    /// <summary>
    /// Flag to check if the character is on the ground.
    /// </summary>
    private bool isGrounded;

    /// <summary>
    /// On start, initializes components.
    /// </summary>
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        height = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    /// <summary>
    /// On each frame, checks if the character is on the ground and handles the air dash input.
    /// </summary>
    void Update() {
        Vector2 raycastOrigin = (Vector2)transform.position - (height / 2) * Vector2.up;
        isGrounded = Physics2D.Raycast(raycastOrigin, Vector2.down, config.groundCheckDistance);
        Debug.DrawRay(raycastOrigin, Vector2.down * config.groundCheckDistance, Color.red);
        
        if (isGrounded) {
            canAirDash = true;
        }

        if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) && canAirDash && !isGrounded) {
            StartCoroutine(airDash(Input.GetKeyDown(KeyCode.Q) ? -1 : 1));
        }
    }

    /// <summary>
    /// Coroutine that performs the air dash.
    /// </summary>
    /// <param name="direction">Direction of the air dash (-1 for left, 1 for right).</param>
    IEnumerator airDash(int direction) {
        canAirDash = false;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;

        float airDashEndTime = Time.time + airDashDuration;
        while (Time.time < airDashEndTime) {
            rb.velocity = new Vector2(airDashSpeed * direction, 0);
            yield return null;
        }

        rb.gravityScale = originalGravity;
    }
}
