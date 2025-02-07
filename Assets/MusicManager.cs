using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public GameObject rhythmsound;      // Assign the rhythmsound GameObject in Inspector
    public GameObject successCanvas;    // Assign the Success Canvas
    public GameObject gameOverCanvas;   // Assign the Game Over Canvas
    public GameObject player;           // Assign the Player GameObject in Inspector

    private bool isGameOver = false;

    void Start()
    {
        // Ensure both canvases are hidden initially
        if (successCanvas != null)
            successCanvas.SetActive(false);

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(false);
    }

    public void GameOver()
    {
        if (isGameOver) return; // Prevent multiple triggers
        isGameOver = true;

        Debug.Log("❌ Game Over Triggered! Stopping music & disabling rhythmsound.");

        // Disable the entire rhythmsound GameObject
        if (rhythmsound != null)
        {
            rhythmsound.SetActive(false);
            Debug.Log("🔇 rhythmsound GameObject Disabled!");
        }
        else
        {
            Debug.LogWarning("⚠ rhythmsound GameObject is not assigned!");
        }

        // Stop player movement (Disable Player Movement Script)
        if (player != null)
        {
            PlayerMovement movementScript = player.GetComponent<PlayerMovement>(); // Replace with actual script name

            if (movementScript != null)
            {
                movementScript.enabled = false;
                Debug.Log("🚫 Player movement disabled.");
            }
            else
            {
                Debug.LogWarning("⚠ No movement script found on player.");
            }
        }

        // Show Game Over screen
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
            Debug.Log("💀 Game Over Canvas shown.");
        }
    }
}
