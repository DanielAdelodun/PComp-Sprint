using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public GameManager gameManager;
    public CameraFollow cameraFollow;
    [SerializeField] private bool startedRunning;
    [SerializeField] private bool gameOver;
    [SerializeField] public  uint totalLives;
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
        foreach (MeshRenderer heart in hearts) {
            heart.enabled = false;
        }
    }

    void Update()
    {
        // Game Over If No More Lives
        if (startedRunning && !gameOver && remainingLives == 0) GameOver();

        // Update Damage Cooldown
        if (damageCooldownRemaining > 0) damageCooldownRemaining -= Time.deltaTime;
    }

    public void LifeLost() {
        if (!gameOver && damageCooldownRemaining <= 0) {

            // Reset Damage Cooldown
            damageCooldownRemaining = damageCooldownTime;

            // Lose Life
            remainingLives -= 1;

            // Shake Camera
            cameraFollow.ShakeCamera();

            // Hide Heart
            hearts[remainingLives].enabled = false;
        }
    }

    public void GameOver() {
        gameOver = true;
        gameManager.GameOver();
    }

    public void StartRunning() {
        startedRunning = true;

        // Reset Lives
        remainingLives = totalLives;

        // Show All Hearts
        foreach (MeshRenderer heart in hearts) {
            heart.enabled = true;
        }
    }
}
