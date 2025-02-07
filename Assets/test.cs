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
    Debug.Log("Game Over triggered! Attempting to show Game Over screen.");

    // Stop or fade out camera shake
    CameraShakerHandler.FadeOutAll(0.5f); // Smoothly stops shake

    if (gameOverCanvas == null)
    {
        Debug.LogError("Game Over Canvas is NOT assigned in the Inspector!");
        return;
    }

    gameOverCanvas.SetActive(true); // Activate Game Over screen

    if (!gameOverCanvas.activeSelf)
    {
        Debug.LogError("Game Over Canvas is still inactive after SetActive(true)! Something is overriding it.");
    }
}
}