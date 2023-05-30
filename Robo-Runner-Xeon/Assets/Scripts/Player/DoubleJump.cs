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

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        height = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update() {
        if (isGrounded) {
            canDoubleJump = true;
        }

        if (Input.GetButtonDown("Jump") && !isGrounded && canDoubleJump) {
            rb.AddForce(Vector2.up * config.jumpImpulse * 1.5f, ForceMode2D.Impulse);
            canDoubleJump = false;
        }
    }
}