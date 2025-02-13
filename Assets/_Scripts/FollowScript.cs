using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    public float speed = 10f;
    public float rotationSpeed = 5f;
    public float knockbackSpeed = 15f;
    public Button startEnemyButton;

    [Header("Camera Settings")]
    public Camera mainCamera;
    public Camera gameOverCam;

    private Transform playerTransform;
    private float groundY;
    private GameOverManager gameOverManager;
    private CharacterController characterController;
    public UIManager uiManager; // Reference to UIManager

    void Start()
    {
        if (player != null)
        {
            playerTransform = player.transform;
            characterController = player.GetComponent<CharacterController>();

            if (characterController != null)
            {
                characterController.enabled = false;
                Debug.Log("🚫 CharacterController DISABLED at start.");
            }
        }

        groundY = transform.position.y;

        gameOverManager = FindObjectOfType<GameOverManager>();
        if (gameOverManager == null)
        {
            Debug.LogError("⚠ GameOverManager not found! Assign it to the scene.");
        }

        if (gameOverCam != null)
        {
            gameOverCam.gameObject.SetActive(false);
        }

        if (startEnemyButton != null)
        {
            startEnemyButton.onClick.AddListener(StartEnemyMovement);
        }
        else
        {
            Debug.LogWarning("⚠ Start Enemy Button is not assigned in the Inspector.");
        }

        this.enabled = false;
    }

    void Update()
    {
        if (playerTransform != null)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        Vector3 targetPosition = new Vector3(playerTransform.position.x, groundY, playerTransform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("🔥 Enemy hit the player! Enabling CharacterController...");

            if (characterController != null && !characterController.enabled)
            {
                StartCoroutine(EnableCharacterControllerWithDelay());
            }

            StartCoroutine(KnockbackAndGameOver(other.transform));

            // **Trigger UIManager to show GameOver screen**
            if (uiManager != null)
            {
                uiManager.GameOver();
                Debug.Log("🕒 UIManager triggered GameOver Timer.");
            }
            else
            {
                Debug.LogError("⚠ UIManager not assigned in EnemyFollow!");
            }
        }
    }

    IEnumerator EnableCharacterControllerWithDelay()
    {
        yield return new WaitForEndOfFrame();
        characterController.enabled = true;
        Debug.Log("✅ CharacterController ENABLED after delay!");
    }

    IEnumerator KnockbackAndGameOver(Transform player)
    {
        Debug.Log("🚀 Switching to Game Over Camera...");

        if (gameOverCam != null && mainCamera != null)
        {
            mainCamera.gameObject.SetActive(false);
            gameOverCam.gameObject.SetActive(true);
        }

        Vector3 knockbackDirection = (player.position - transform.position).normalized;
        float horizontalForce = 30f;
        float verticalForce = 50f;

        float elapsedTime = 0f;
        float knockbackDuration = 0.5f;

        while (elapsedTime < knockbackDuration)
        {
            if (characterController != null && characterController.enabled)
            {
                characterController.Move((knockbackDirection * horizontalForce + Vector3.up * verticalForce) * Time.deltaTime);
            }
            else
            {
                player.position += (knockbackDirection * horizontalForce + Vector3.up * verticalForce) * Time.deltaTime;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("💀 Knockback finished! Triggering Game Over...");

        if (gameOverManager != null)
        {
            gameOverManager.TriggerGameOver();
        }
        else
        {
            Debug.LogError("⚠ gameOverManager is NULL! Assign it in the scene.");
        }
    }

    public void StartEnemyMovement()
    {
        Debug.Log("⚡ EnemyFollow script ENABLED!");
        this.enabled = true;
    }
}
