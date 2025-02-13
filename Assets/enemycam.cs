using UnityEngine;

public class FollowEnemyZOffset : MonoBehaviour
{
    public Transform enemy; // Assign the enemy in the Inspector
    public float zOffset = -5f; // Set distance from enemy (adjust in Inspector)

    private void LateUpdate()
    {
        if (enemy == null)
        {
            Debug.LogWarning("⚠ No enemy assigned to FollowEnemyZOffset script!");
            return;
        }

        // Keep X and Y fixed, but update only Z with an offset
        transform.position = new Vector3(transform.position.x, transform.position.y, enemy.position.z + zOffset);
    }
}
