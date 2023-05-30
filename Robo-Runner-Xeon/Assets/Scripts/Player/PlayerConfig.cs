using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Config/Player")]
public class PlayerConfig : ScriptableObject {
    /// <summary>
    /// The horizontal speed that the player moves at.
    /// </summary>
    public float speed = 5.0f;
    
    /// <summary>
    /// The upward impulse force that will be applied when the player jumps.
    /// </summary>
    public float jumpImpulse = 5.0f;

    /// <summary>
    /// The sideways impulse force that will be applied when the player air-dashes.
    /// </summary>
    public float airDashImpulse = 10.0f;
}