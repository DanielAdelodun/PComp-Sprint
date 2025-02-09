using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    private bool canReload = false; // Prevent reloading before Game Over

    void Update()
    {
        if (canReload && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("🔄 Restarting Scene...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void EnableReload()
    {
        canReload = true;
        Debug.Log("✅ Scene reload enabled. Press 'R' to restart.");
    }
}
