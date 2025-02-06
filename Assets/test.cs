using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    void GameOver()
    {
        gameOverCanvas.SetActive(true); // Show Game Over screen
    }
}
