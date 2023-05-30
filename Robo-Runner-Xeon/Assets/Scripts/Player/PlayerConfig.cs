using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Config/Player")]
public class PlayerConfig : ScriptableObject {
    /// <summary>
    /// The horizontal speed that the player moves at.
    /// </summary>
    public float speed = 10.0f;
    
    /// <summary>
    /// The upward velocity that will be applied when the player jumps.
    /// </summary>
    public float jumpVelocity = 5.0f;
    
    /// <summary>
    /// The distance below the player that will be checked to determine if the player is grounded.
    /// </summary>
    public float groundCheckDistance = 0.1f;
}