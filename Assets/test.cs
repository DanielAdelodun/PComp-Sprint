using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FirstGearGames.SmoothCameraShaker; // Make sure the camera shake library is correctly imported

public class HealthSystem : MonoBehaviour
{
    public Image[] healthImages; // Assign the three UI images in the Inspector
    public GameObject gameOverCanvas; // Assign the Game Over canvas

    private int lives;

    void Start()
    {
        lives = healthImages.Length; // Initialize lives count
        gameOverCanvas.SetActive(false); // Hide Game Over screen at start
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle")) // Ensure the obstacle has this tag
        {
            if (lives > 0)
            {
                lives--;
                healthImages[lives].gameObject.SetActive(false); // Hide one image

                if (lives == 0)
                {
                    GameOver();
                }
            }
        }
    }

    void GameOver()
    {
        Debug.Log("❌ Game Over triggered! Attempting to show Game Over screen.");

        CameraShakerHandler.FadeOutAll(0.5f); // Stop camera shake smoothly

        // **Stop the music by calling MusicManager's GameOver()**
        MusicManager musicManager = FindObjectOfType<MusicManager>();
        if (musicManager != null)
        {
            musicManager.GameOver(); // Stop music & disable rhythmsound
        }
        else
        {
            Debug.LogError("⚠ MusicManager not found in the scene!");
        }

        if (gameOverCanvas == null)
        {
            Debug.LogError("⚠ Game Over Canvas is NOT assigned in the Inspector!");
            return;
        }

        gameOverCanvas.SetActive(true); // Show Game Over screen

        if (!gameOverCanvas.activeSelf)
        {
            Debug.LogError("⚠ Game Over Canvas is still inactive after SetActive(true)! Something is overriding it.");
        }

        // Enable the SceneReloader script
        SceneReloader sceneReloader = FindObjectOfType<SceneReloader>();
        if (sceneReloader != null)
        {
            sceneReloader.EnableReload(); // Allow pressing 'R' to restart
        }
        else
        {
            Debug.LogError("⚠ SceneReloader script is missing in the scene!");
        }
    }
}
