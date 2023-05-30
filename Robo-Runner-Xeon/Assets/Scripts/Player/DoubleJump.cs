using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script manages the "Double Jump" mechanic for the player character.
/// The character is able to perform a second jump while airborne (a double jump), but
/// it can only be performed once while the character is airborne. The double jump
/// can be performed again only after the character lands on the ground. The height of
/// the double jump is determined by the jumpVelocity property.
/// </summary>
public class DoubleJump : MonoBehaviour {
    public PlayerConfig config;
    public ContactFilter2D contactFilter;
    
    private Rigidbody2D rb;
    private bool canDoubleJump = true;
    private float height;

    public bool isGrounded => rb.IsTouching(contactFilter);

    /// <summary>
    /// Initial setup for the double jump mechanic, called at the start of the game.
    /// </summary>
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        height = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    /// <summary>
    /// Update is called once per frame and manages the logic for the double jump mechanic.
    /// </summary>
    void Update() {
        Vector2 raycastOrigin = (Vector2)transform.position - (height / 2) * Vector2.up;
        Debug.DrawRay(raycastOrigin, Vector2.down * config.groundCheckDistance, Color.red);

        if (isGrounded) {
            canDoubleJump = true;
        }

        if (Input.GetButtonDown("Jump") && !isGrounded && canDoubleJump) {
            rb.velocity = new Vector2(rb.velocity.x, config.jumpVelocity);
            canDoubleJump = false;
        }
    }
}