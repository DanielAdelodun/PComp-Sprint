using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject gameOverCanvas;   // Assign the Game Over Canvas in Inspector
    public GameObject rhythmsound;      // Assign the rhythmsound GameObject in Inspector

    [Header("Player & Enemy Components")]
    public HatController hatController; // Assign the HatController directly
    public EnemyFollow enemyFollow;     // Assign the EnemyFollow script directly
    public AudioSource gameMusic;       // Assign the AudioSource that plays the music

    private bool isGameOver = false;

    void Start()
    {
        // Hide Game Over UI at the start
        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(false);
    }

    public void TriggerGameOver()
    {
        if (isGameOver) return; // Prevent multiple triggers
        isGameOver = true;

        Debug.Log("❌ Game Over Triggered! Stopping everything...");

        // **Stop Music**
        if (gameMusic != null)
        {
            gameMusic.Stop();
            Debug.Log("🎵 Music stopped!");
        }
        else
        {
            Debug.LogWarning("⚠ No AudioSource assigned for game music!");
        }

        // **Disable the entire rhythmsound GameObject**
        if (rhythmsound != null)
        {
            rhythmsound.SetActive(false);
            Debug.Log("🔇 rhythmsound GameObject Disabled!");
        }
        else
        {
            Debug.LogWarning("⚠ rhythmsound GameObject is not assigned!");
        }

        // **Stop Player and Enemy Movement**
        if (hatController != null)
        {
            hatController.enabled = false; // Disable movement
            Debug.Log("🚫 HatController disabled. Player movement stopped.");
        }
        if (enemyFollow != null)
        {
            enemyFollow.enabled = false; // Disable enemy movement
            Debug.Log("🚫 EnemyFollow disabled. Enemy movement stopped.");
        }

        // **Show Game Over screen**
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
            Debug.Log("💀 Game Over Canvas shown.");
        }
    }
}
