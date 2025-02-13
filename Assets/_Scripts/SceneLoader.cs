using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneReloader : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        Debug.Log("🔄 Reloading Scene: " + SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
