using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lives : MonoBehaviour
{
    [SerializeField] private bool startedRunning = false;
    [SerializeField] private bool gameOver = false;
    [SerializeField] private uint totalLives = 5;
    [SerializeField] private uint remainingLives; // >0 To Prevent Immediate GameOver
    [SerializeField] private float gameStartTime;
    [SerializeField] private float gameEndTime;
    [SerializeField] private float timeDelta;
    [SerializeField] string totalTime;
    public TMP_Text timerDisplayCorner;
    public TMP_Text timerDisplayCenter;
    public TMP_Text livesDisplay; // TODO Remove + Replace
    public PlayerRunning playerRunning;
    public HatController hatController;
    
    void Start()
    {
    }

    void Update()
    {
        // Game Over If No More Lives
        if (startedRunning && !gameOver && remainingLives == 0) GameOver();

        // Show Time Spent Running
        if (startedRunning && !gameOver) timerDisplayCorner.text = FormatTime(Time.time - gameStartTime);
    }

    public void LifeLost() {
        if (!gameOver) remainingLives -= 1;
        livesDisplay.text = remainingLives.ToString();
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
    }

    public void StartRunning() {
        startedRunning = true;

        remainingLives = totalLives;

        // Remember Current Time
        gameStartTime = Time.time;

        // Call Relevant Functions
        playerRunning.StartPlayerRun();
        hatController.StartRunning();
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
