using UnityEngine;

/// <summary>
/// This script manages the player character's basic movements.
/// The character is able to move horizontally and jump while on the ground.
/// The speed of movement and jump height can be adjusted through public properties.
/// </summary>
public class PlayerController : MonoBehaviour
{
    public PlayerConfig config;

    private float height;
    private Rigidbody2D rb;

    /// <summary>
    /// Initial setup for the player controller, called at the start of the game.
    /// </summary>
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        height = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    /// <summary>
    /// Update is called once per frame and manages the logic for player movement and jumping.
    /// </summary>
    void Update() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * config.speed, rb.velocity.y);

        Vector2 raycastOrigin = (Vector2)transform.position - (height / 2) * Vector2.up;
        bool isGrounded = Physics2D.Raycast(raycastOrigin, Vector2.down, config.groundCheckDistance);
        Debug.DrawRay(raycastOrigin, Vector2.down * config.groundCheckDistance, Color.red);

        if (Input.GetButtonDown("Jump") && isGrounded) {
            rb.velocity = new Vector2(rb.velocity.x, config.jumpVelocity);
        }
    }
}
