using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void Restart()
    {
        Debug.Log("✅ Restart button clicked! Freezing game...");
        
        // Freeze the game before restarting
        Time.timeScale = 0f;

        // Wait 1 second, then restart the scene
        Invoke("ReloadScene", 1f);
    }

    void ReloadScene()
    {
        Debug.Log("🔄 Reloading Scene: ObstacleScene");
        Time.timeScale = 1f; // Reset time before loading
        SceneManager.LoadScene("ObstacleScene");
    }
}
