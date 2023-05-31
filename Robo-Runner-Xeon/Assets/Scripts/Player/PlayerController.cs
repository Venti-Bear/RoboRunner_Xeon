using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script manages the player character's basic movements.
/// The character is able to move horizontally and jump while on the ground.
/// Utilizes Unity's built-in physics to detect if the player is grounded.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] public PlayerConfig config;
    public ContactFilter2D contactFilter;

    private float height;
    private Rigidbody2D rb;
    private bool pressedJump, releasedJump;

    [SerializeField] public bool isGrounded => rb.IsTouching(contactFilter);
    /// <summary>
    /// Initial setup for the player controller, called at the start of the game.
    /// </summary>
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        height = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update() {
        if (Input.GetButtonDown("Jump")) {
            pressedJump = true;
        }

        if (Input.GetButtonUp("Jump")) {
            releasedJump = true;
        }
    }

    /// <summary>
    /// Manages the logic for player movement and jumping.
    /// Jumping height is variable; the longer the jump button is held, the higher the jump, to a limit.
    /// Upward speed is reset to zero rather than decelerated for a more responsive mini-jump.
    /// </summary>
    void FixedUpdate() {
        if (isGrounded){
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveHorizontal * config.speed, rb.velocity.y);
        }

        if (pressedJump && isGrounded) {
            rb.AddForce(Vector2.up * config.jumpImpulse, ForceMode2D.Impulse);
        }

        if (releasedJump && rb.velocity.y > 0) {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        
        pressedJump = false;
        releasedJump = false;
    }
}
