using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// This script controls the "Dash" mechanic of the player character.
/// The character is able to dash, either on the ground or in the air, in a specified direction (left or right) for a 
/// defined duration. If airborne, the mechanic can only be performed once,
/// and it can be performed again only after the character lands on the ground. The direction 
/// of the dash is determined based on the player's input (Q for left, E for right).
/// </summary>
public class AirDash : MonoBehaviour
{
    /// <summary>
    /// Duration of the air dash.
    /// </summary>
    public float dashDuration = 0.3f;

    public ContactFilter2D contactFilter;
    public Collider2D groundDetector;

    [SerializeField] public PlayerConfig config;

    private Animator anim;

    private float height;
    private Rigidbody2D rb;
    private bool canDash = true;
    private DynamicGravity dynamicGravity;

    public bool isGrounded => groundDetector.IsTouching(contactFilter);

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        height = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        dynamicGravity = gameObject.GetComponent<DynamicGravity>();
    }

    /// <summary>
    /// On each frame, checks if the character is on the ground and handles the air dash input.
    /// </summary>
    void Update() {
        if (isGrounded && !config.isDashing) {
            canDash = true;
        }

        if (!isGrounded && !config.isDashing && Input.GetAxisRaw("Horizontal") != 0) {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * config.speed, rb.velocity.y);
            // rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * (canDash ? config.speed : Mathf.Abs(rb.velocity.x)), rb.velocity.y);
        }

        if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) && canDash) {
            StartCoroutine(dash(Input.GetKeyDown(KeyCode.Q) ? -1 : 1));
        }

        anim.SetBool("isDashing", config.isDashing);
    }

    /// <summary>
    /// Coroutine that performs the air dash. It first disables the dynamic gravity script, then
    /// stops all currently applied velocity, to then apply the air dash in a direction before
    /// turn gravity back on.
    /// </summary>
    /// <param name="direction">Direction of the air dash (-1 for left, 1 for right).</param>
    IEnumerator dash(int direction) {
        canDash = false;
        config.isDashing = true;
        
        dynamicGravity.applyDynamicG = false;
        rb.velocity = Vector2.zero;
        if(!isGrounded){
            rb.gravityScale = 0;
        }

        rb.AddForce(new Vector2(direction * config.dashImpulse, 0), ForceMode2D.Impulse);
        if (Math.Abs(rb.velocity.x) > config.speed) {
            rb.velocity = new Vector2(config.dashImpulse * direction, 0);
        } else {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        yield return new WaitForSeconds(dashDuration);

        dynamicGravity.applyDynamicG = true;
        config.isDashing = false;
    }
}
