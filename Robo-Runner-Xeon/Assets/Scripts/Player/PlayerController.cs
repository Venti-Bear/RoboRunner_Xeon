using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpVelocity = 5.0f;
    public float groundCheckDistance = 0.1f;
    public float height;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Move the character
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);

        // Calculate the starting point of the raycast
        Vector2 raycastOrigin = (Vector2)transform.position - (height / 2) * Vector2.up;

        // Use a raycast straight downwards from the base of the player to check for ground
        bool isGrounded = Physics2D.Raycast(raycastOrigin, Vector2.down, groundCheckDistance);

        // Draw a debug ray. Note that this will be visible in Scene view, not in Game view.
        Debug.DrawRay(raycastOrigin, Vector2.down * groundCheckDistance, Color.red);

        // If the player is standing on the ground and presses the Space key, they should jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
        }
    }
}
