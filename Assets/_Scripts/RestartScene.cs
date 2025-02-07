using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    private bool canReload = false; // Prevent reloading during gameplay

    void Update()
    {
        if (canReload && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // Call this method from GameOver() in HealthSystem
    public void EnableReload()
    {
        canReload = true;
    }
}
