using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    public float airDashDuration = 0.3f;

    public ContactFilter2D contactFilter;
    public PlayerConfig config;

    private float height;
    private Rigidbody2D rb;
    private bool canAirDash = true;
    private bool isAirDashing = false;
    private DynamicGravity dynamicGravity;

    public bool isGrounded => rb.IsTouching(contactFilter);

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        height = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        dynamicGravity = gameObject.GetComponent<DynamicGravity>();
    }

    /// <summary>
    /// On each frame, checks if the character is on the ground and handles the air dash input.
    /// </summary>
    void Update() {
        if (isGrounded) {
            canAirDash = true;
        }

        if (!isGrounded && !isAirDashing && Input.GetAxisRaw("Horizontal") != 0) {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * config.speed, rb.velocity.y);
        }

        if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) && canAirDash && !isGrounded) {
            StartCoroutine(airDash(Input.GetKeyDown(KeyCode.Q) ? -1 : 1));
        }
    }

    /// <summary>
    /// Coroutine that performs the air dash. It first disables the dynamic gravity script, then
    /// stops all currently applied velocity, to then apply the air dash in a direction before
    /// turn gravity back on.
    /// </summary>
    /// <param name="direction">Direction of the air dash (-1 for left, 1 for right).</param>
    IEnumerator airDash(int direction) {
        canAirDash = false;
        isAirDashing = true;
        
        dynamicGravity.applyDynamicG = false;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;

        rb.AddForce(new Vector2(direction * config.airDashImpulse, 0), ForceMode2D.Impulse);
        if (Math.Abs(rb.velocity.x) > config.speed) {
            rb.velocity = new Vector2(config.airDashImpulse * direction, 0);
        } else {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        yield return new WaitForSeconds(airDashDuration);

        dynamicGravity.applyDynamicG = true;
        isAirDashing = false;
    }
}
