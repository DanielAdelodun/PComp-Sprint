using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lives : MonoBehaviour
{
    [SerializeField] private bool startedRunning = false;
    [SerializeField] private bool gameOver = false;
    [SerializeField] private uint totalLives = 3;
    [SerializeField] private uint remainingLives; // >0 To Prevent Immediate GameOver
    [SerializeField] private float gameStartTime;
    [SerializeField] private float gameEndTime;
    [SerializeField] private float timeDelta;
    [SerializeField] private float damageCooldownTime = 0.2f;
    private float damageCooldownRemaining = 0.0f;
    [SerializeField] string totalTime;
    public TMP_Text timerDisplayCorner;
    public TMP_Text timerDisplayCenter;
    public PlayerRunning playerRunning;
    public HatController hatController;

    public MeshRenderer heartOne;
    public MeshRenderer heartTwo;
    public MeshRenderer heartThree;
    private MeshRenderer[] hearts;

    public CameraFollow cameraFollow;
    
    void Start()
    {
        remainingLives = totalLives;
        timerDisplayCenter.gameObject.SetActive(false);
        timerDisplayCorner.gameObject.SetActive(true);

        hearts = new MeshRenderer[] {heartOne, heartTwo, heartThree};
        
        // Hide All Hearts
        foreach (MeshRenderer heart in hearts) {
            heart.enabled = false;
        }
    }

    void Update()
    {
        // Game Over If No More Lives
        if (startedRunning && !gameOver && remainingLives == 0) GameOver();

        // Show Time Spent Running
        if (startedRunning && !gameOver) timerDisplayCorner.text = FormatTime(Time.time - gameStartTime);

        // Update Damage Cooldown
        if (damageCooldownRemaining > 0) damageCooldownRemaining -= Time.deltaTime;
    }

    public void LifeLost() {
        if (!gameOver && damageCooldownRemaining <= 0) {

            // Reset Damage Cooldown
            damageCooldownRemaining = damageCooldownTime;

            // Lose Life
            remainingLives -= 1;

            // Hide Heart
            Debug.Log(remainingLives);
            hearts[remainingLives].enabled = false;
        }
    }

    void GameOver() {
        gameOver = true;

        gameEndTime = Time.time;
        timeDelta = gameEndTime - gameStartTime;

        string totalTime = FormatTime(timeDelta);

        // Show Total Time Spent Running
        timerDisplayCenter.gameObject.SetActive(true);
        timerDisplayCenter.text = totalTime;
        timerDisplayCorner.gameObject.SetActive(false);

        // Call Relevant Functions
        playerRunning.GameOver();
        hatController.GameOver();
        cameraFollow.GameOver();
    }

    public void StartRunning() {
        startedRunning = true;

        remainingLives = totalLives;

        // Show All Hearts
        foreach (MeshRenderer heart in hearts) {
            heart.enabled = true;
        }

        // Remember Current Time
        gameStartTime = Time.time;

        // Call Relevant Functions
        playerRunning.StartPlayerRun();
        hatController.StartRunning();
        cameraFollow.StartRunning();
    }
    public static string FormatTime(float totalSeconds)
    {
        int seconds = Mathf.FloorToInt(totalSeconds);
        int hours = seconds / 3600;
        int minutes = (seconds % 3600) / 60;
        seconds = seconds % 60;

        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}
