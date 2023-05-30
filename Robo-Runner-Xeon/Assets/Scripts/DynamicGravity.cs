using UnityEngine;

public class DynamicGravity : MonoBehaviour
{
    public float AgainstGravityScale = 1f;
    public float FallGravityScale = 2f;
    public bool applyDynamicG { get; set; } = true;

    private Rigidbody2D m_Rigidbody;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Certain mechanics require specific gravity be applied rather than dynamic gravity
        if (!applyDynamicG) return;

        // Calculate our direction relative to the global gravity.
        var direction = Vector2.Dot(m_Rigidbody.velocity, Physics2D.gravity);
        
        // Set the gravity scale accordingly.
        m_Rigidbody.gravityScale = direction > 0f ? FallGravityScale : AgainstGravityScale;
    }
}