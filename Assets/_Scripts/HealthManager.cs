using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public GameManager gameManager;
    public CameraFollow cameraFollow;
    public EnemyFollow enemyFollow;  // Reference to EnemyFollow script
    public GameObject gameOverCanvas; // Assign GameOver Canvas in the Inspector

    [SerializeField] private bool startedRunning;
    [SerializeField] private bool gameOver;
    [SerializeField] public uint totalLives;
    [SerializeField] private uint remainingLives;
    [SerializeField] private float damageCooldownTime;
    private float damageCooldownRemaining = 0.0f;

    public MeshRenderer[] hearts;

    void Start()
    {
        startedRunning = false;
        gameOver = false;
        totalLives = 5;
        damageCooldownTime = 0.2f;
        remainingLives = totalLives;

        // Hide All Hearts
        foreach (MeshRenderer heart in hearts)
        {
            heart.enabled = false;
        }

        // Ensure GameOver Canvas is hidden at start
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
    }

    void Update()
    {
        // Stop the game if lives reach 0
        if (startedRunning && !gameOver && remainingLives == 0)
        {
            GameOver();
        }

        // Update Damage Cooldown
        if (damageCooldownRemaining > 0) damageCooldownRemaining -= Time.deltaTime;
    }

    public void LifeLost()
    {
        if (!gameOver && damageCooldownRemaining <= 0)
        {
            // Reset Damage Cooldown
            damageCooldownRemaining = damageCooldownTime;

            // Lose Life
            if (remainingLives > 0)
            {
                remainingLives -= 1;

                // Shake Camera
                cameraFollow.ShakeCamera();

                // Hide Heart
                if (remainingLives < hearts.Length)
                {
                    hearts[remainingLives].enabled = false;
                }
            }

            // Check if game should end
            if (remainingLives == 0)
            {
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        if (gameOver) return;
        gameOver = true;

        Debug.Log("💀 Game Over! Disabling EnemyFollow...");

        // **Disable Enemy Follow**
        if (enemyFollow != null)
        {
            enemyFollow.enabled = false;
        }
        else
        {
            Debug.LogWarning("⚠ EnemyFollow script is not assigned in the Inspector!");
        }

        // **Show Game Over Canvas**
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
            Debug.Log("📌 GameOver Canvas Activated!");
        }
        else
        {
            Debug.LogError("⚠ GameOver Canvas is NOT assigned in the Inspector!");
        }

        // Call Game Over in GameManager
        if (gameManager != null)
        {
            gameManager.GameOver();
        }
    }

    public void StartRunning()
    {
        startedRunning = true;
        gameOver = false;

        // Reset Lives
        remainingLives = totalLives;

        // Show All Hearts
        foreach (MeshRenderer heart in hearts)
        {
            heart.enabled = true;
        }

        // **Enable Enemy Follow Again**
        if (enemyFollow != null)
        {
            enemyFollow.enabled = true;
        }

        // **Hide Game Over Canvas**
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
    }
}
