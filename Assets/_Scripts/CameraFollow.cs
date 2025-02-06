using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform player; // Reference to the player transform

    [Header("Camera Settings")]
    public float smoothSpeed = 0.125f; // Smoothness of camera movement
    public Vector2 minBounds; // Minimum bounds (bottom-left corner)
    public Vector2 maxBounds; // Maximum bounds (top-right corner)

    private Vector3 targetPosition; // Desired camera position

    void LateUpdate()
    {
        if (player != null)
        {
            // Calculate target position based on the player position
            targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);

            // Clamp the target position to the level bounds
            targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);

            // Smoothly move the camera to the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Visualise the camera bounds in the Scene view
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(minBounds.x, minBounds.y, 0), new Vector3(maxBounds.x, minBounds.y, 0));
        Gizmos.DrawLine(new Vector3(minBounds.x, maxBounds.y, 0), new Vector3(maxBounds.x, maxBounds.y, 0));
        Gizmos.DrawLine(new Vector3(minBounds.x, minBounds.y, 0), new Vector3(minBounds.x, maxBounds.y, 0));
        Gizmos.DrawLine(new Vector3(maxBounds.x, minBounds.y, 0), new Vector3(maxBounds.x, maxBounds.y, 0));
    }
}
