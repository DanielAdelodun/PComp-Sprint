using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private Vector3 targetPosition;
    private bool gameOver = false;
    private bool startedRunning = false;

    [SerializeField] private Vector3 startCameraOffset;
    [SerializeField] private Vector3 runningCameraOffset;
    [SerializeField] private Vector3 startCameraRotation;
    [SerializeField] private Vector3 runningCameraRotation;
    [SerializeField] private float cameraTransitionTimeTotal = 2.0f;
    private float cameraTransitionTimeLeft;
    

    void Start()
    {
        startCameraOffset = new Vector3(-2.5f, 2.0f, 6.0f);
        runningCameraOffset = new Vector3(0.0f, 4.0f, -10.0f);
        startCameraRotation = new Vector3(2.0f, -200.0f, 0.0f);
        runningCameraRotation = new Vector3(15.0f, 0.0f, 0.0f);
        cameraTransitionTimeLeft = cameraTransitionTimeTotal;
    }

    void LateUpdate()
    {
        if (player != null && !gameOver && startedRunning && cameraTransitionTimeLeft <= 0)  // We Are Running
        {
            targetPosition = new Vector3(player.position.x, player.position.y, player.position.z);
            targetPosition += runningCameraOffset;

            transform.position = targetPosition;

            transform.rotation = Quaternion.Euler(runningCameraRotation);

        } else if (player != null && !gameOver && !startedRunning)             // Just Started Game
        {
            targetPosition = new Vector3(player.position.x, player.position.y, player.position.z);
            targetPosition += startCameraOffset;
            transform.position = targetPosition;

            transform.rotation = Quaternion.Euler(startCameraRotation);
        } else if (player != null && !gameOver && startedRunning && cameraTransitionTimeLeft > 0) {
            cameraTransitionTimeLeft -= Time.deltaTime;
            float t = 1 - (cameraTransitionTimeLeft / cameraTransitionTimeTotal);
            transform.position = player.position + Vector3.Lerp(startCameraOffset, runningCameraOffset, t);
            transform.rotation = Quaternion.Euler(Vector3.Lerp(startCameraRotation, runningCameraRotation, t));
        }
    }

    public void GameOver()
    {
        gameOver = true;
    }

    public void StartRunning()
    {
        startedRunning = true;
    }
}
