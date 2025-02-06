using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private string[] scenePaths;

    void Start()
    {
        scenePaths = new string[] { "ObstacleScene", "GaryOh", "TargetScene"};
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "InitialScene") {
            string hash = Application.absoluteURL.Split('#')[1];
            LoadScene(hash);
        }
    }
    public void LoadScene(string msg) 
    {
        int i = int.Parse(msg) - 1;;
        string sceneName = scenePaths[i];
        SceneManager.LoadScene(sceneName);
    }
}
