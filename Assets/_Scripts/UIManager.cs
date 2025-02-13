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

        timerDisplayCenter.gameObject.SetActive(false);
        timerDisplayCorner.gameObject.SetActive(true);
    }

    void Update()
    {
        if (startedRunning && !gameOver)
            timerDisplayCorner.text = FormatTime(Time.time - runStartTime);
    }

    public void StartRunning()
    {
        startedRunning = true;
        runStartTime = Time.time;
    }

    public void GameOver()
    {
        if (gameOver) return;
        gameOver = true;

        runEndTime = Time.time;
        timeDelta = runEndTime - runStartTime;

        string totalTime = FormatTime(timeDelta);
        timerDisplayCenter.text = "Time: " + totalTime;
        timerDisplayCenter.gameObject.SetActive(true);
        timerDisplayCorner.gameObject.SetActive(false);

        Debug.Log("🕒 Game Over! Displaying final time: " + totalTime);
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
