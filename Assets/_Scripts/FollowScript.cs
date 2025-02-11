using System.Collections;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player; // Reference to the Player
    public float speed = 10f; // Enemy movement speed
    public float rotationSpeed = 5f; // Enemy rotation speed
    public float knockbackSpeed = 15f; // Speed of the knockback

    [Header("Camera Settings")]
    public Camera mainCamera; // Assign Main Camera
    public Camera gameOverCam; // Assign Game Over Camera

    private Transform playerTransform;
    private float groundY; // Stores the Y-position to keep enemy on the ground
    private GameOverManager gameOverManager; // Reference to GameOverManager
    private CharacterController characterController; // Reference to CharacterController

    void Start()
    {
        if (player != null)
        {
            playerTransform = player.transform;
            characterController = player.GetComponent<CharacterController>();

            // Ensure CharacterController is disabled at the beginning
            if (characterController != null)
            {
                characterController.enabled = false;
                Debug.Log("🚫 CharacterController DISABLED at start.");
            }
        }

        // Store the initial Y position to keep enemy grounded
        groundY = transform.position.y;

        // Find the GameOverManager in the scene
        gameOverManager = FindObjectOfType<GameOverManager>();
        if (gameOverManager == null)
        {
            Debug.LogError("⚠ GameOverManager not found! Assign it to the scene.");
        }

        // Ensure Game Over Camera is disabled at start
        if (gameOverCam != null)
        {
            gameOverCam.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            FollowPlayer(); // Enemy always follows player but stays on the ground
        }
    }

    void FollowPlayer()
    {
        // Keep the enemy on the ground by ignoring the player's Y position
        Vector3 targetPosition = new Vector3(playerTransform.position.x, groundY, playerTransform.position.z);
        
        // Move towards the target position but keep Y constant
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Rotate smoothly to face the player
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("🔥 Enemy hit the player! Enabling CharacterController...");

            // **Enable Character Controller if it was disabled**
            if (characterController != null && !characterController.enabled)
            {
                StartCoroutine(EnableCharacterControllerWithDelay());
            }

            StartCoroutine(KnockbackAndGameOver(other.transform));
        }
    }

    IEnumerator EnableCharacterControllerWithDelay()
    {
        yield return new WaitForEndOfFrame(); // **Wait for physics update**
        characterController.enabled = true;
        Debug.Log("✅ CharacterController ENABLED after delay!");
    }

    IEnumerator KnockbackAndGameOver(Transform player)
    {
        Debug.Log("🚀 Switching to Game Over Camera...");
        
        // **Switch to Game Over Camera**
        if (gameOverCam != null && mainCamera != null)
        {
            mainCamera.gameObject.SetActive(false);
            gameOverCam.gameObject.SetActive(true);
        }

        // Knockback direction should only affect the player
        Vector3 knockbackDirection = (player.position - transform.position).normalized;
        float horizontalForce = 30f; // Strong push
        float verticalForce = 50f; // Strong upward push

        float elapsedTime = 0f;
        float knockbackDuration = 0.5f; // Duration of knockback effect

        while (elapsedTime < knockbackDuration) 
        {
            if (characterController != null && characterController.enabled)
            {
                // **Use CharacterController.Move() instead of transform.position**
                characterController.Move((knockbackDirection * horizontalForce + Vector3.up * verticalForce) * Time.deltaTime);
            }
            else
            {
                // **If CharacterController is disabled, use transform movement**
                player.position += (knockbackDirection * horizontalForce + Vector3.up * verticalForce) * Time.deltaTime;
            }
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("💀 Knockback finished! Triggering Game Over...");

        // **Trigger Game Over after knockback completes**
        if (gameOverManager != null)
        {
            gameOverManager.TriggerGameOver();
        }
        else
        {
            Debug.LogError("⚠ gameOverManager is NULL! Assign it in the scene.");
        }
    }
}