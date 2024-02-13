using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script manages the player character's basic movements.
/// The character is able to move horizontally and jump while on the ground.
/// Utilizes Unity's built-in physics to detect if the player is grounded.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] public PlayerConfig config;
    public ContactFilter2D contactFilter;
    public Collider2D groundDetector;

    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private Animator anim;
    
    private float height;
    private bool pressedJump, releasedJump;

    public bool isGrounded => groundDetector.IsTouching(contactFilter);

    /// <summary>
    /// Initial setup for the player controller, called at the start of the game.
    /// </summary>
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        height = sprite.bounds.size.y;
    }

    void Update() {
        if (Input.GetButtonDown("Jump")) {
            pressedJump = true;
        }

        if (Input.GetButtonUp("Jump")) {
            releasedJump = true;
        }

        if (rb.velocity.x != 0) {
            sprite.flipX = rb.velocity.x < 0;
        }
        
        anim.SetFloat("Vertical", rb.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }

    /// <summary>
    /// Manages the logic for player movement and jumping.
    /// Jumping height is variable; the longer the jump button is held, the higher the jump, to a limit.
    /// Upward speed is reset to zero rather than decelerated for a more responsive mini-jump.
    /// </summary>
    void FixedUpdate() {
        if (isGrounded && !config.isDashing){
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveHorizontal * config.speed, rb.velocity.y);
        }

        if (pressedJump && isGrounded) {
            rb.AddForce((Vector2.up * config.jumpImpulse), ForceMode2D.Impulse);
        }

        if (releasedJump && rb.velocity.y > 0) {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        pressedJump = false;
        releasedJump = false;
        anim.SetBool("isRunning", rb.velocity.x != 0);
    }
}
