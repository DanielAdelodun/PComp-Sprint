using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerRunning playerRunning;
    public HatController hatController;
    public CameraFollow cameraFollow;
    public UIManager uiManager;
    public HealthManager healthManager;
    private bool startedRunning;
    private bool gameOver;

    void Start()
    {
        // startedRunning = false;
        // gameOver = false;
    }

    void Update()
    {
        
    }

    public void StartRunning() 
    {
        // startedRunning = true;

        healthManager.StartRunning();
        uiManager.StartRunning();
        playerRunning.StartPlayerRun();
        hatController.StartRunning();
        cameraFollow.StartRunning();
    }

    public void GameOver() 
    {
        // gameOver = true;

        uiManager.GameOver();
        playerRunning.GameOver();
        hatController.GameOver();
        cameraFollow.GameOver();
    }
}
