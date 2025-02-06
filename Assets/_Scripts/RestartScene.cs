using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    void Update()
    {
        // Check if the R key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Reload the active scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
