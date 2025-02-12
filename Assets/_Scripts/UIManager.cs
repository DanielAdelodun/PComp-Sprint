using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] string totalTime;
    public TMP_Text timerDisplayCorner;
    public TMP_Text timerDisplayCenter;
    private bool startedRunning;
    private bool gameOver;
    private float runStartTime;
    private float runEndTime;
    private float timeDelta;

    void Start()
    {
        startedRunning = false;
        gameOver = false;

        // Hide Center Timer, Show Corner Timer
        timerDisplayCenter.gameObject.SetActive(false);
        timerDisplayCorner.gameObject.SetActive(true);
    }

    void Update()
    {
        // Update Corner Timer
        if (startedRunning && !gameOver) timerDisplayCorner.text = FormatTime(Time.time - runStartTime);
    }

    public void StartRunning() {
        startedRunning = true;

        // Remember Start Time
        runStartTime = Time.time;
    }

    public void GameOver() {
        gameOver = true;

        // Calculate Total Time Spent Running
        runEndTime = Time.time;
        timeDelta = runEndTime - runStartTime;


        // Show Total Time Spent Running On Center Timer
        string totalTime = FormatTime(timeDelta);
        timerDisplayCenter.text = totalTime;
        timerDisplayCenter.gameObject.SetActive(true);

        // Hide Corner Timer
        timerDisplayCorner.gameObject.SetActive(false);
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
