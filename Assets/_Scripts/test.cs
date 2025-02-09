using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public Image[] healthImages; // Assign health UI images
    public GameOverManager gameOverManager; // Assign the GameOverManager script in Inspector

    private int lives;

    void Start()
    {
        lives = healthImages.Length; // Initialize lives count
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (lives > 0)
            {
                lives--;
                healthImages[lives].gameObject.SetActive(false);

                if (lives == 0 && gameOverManager != null)
                {
                    gameOverManager.TriggerGameOver();
                }
            }
        }
    }
}
