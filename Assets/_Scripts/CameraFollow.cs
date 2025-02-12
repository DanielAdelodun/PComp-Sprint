using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private Vector3 targetPosition;
    private bool gameOver;
    private bool startedRunning;

    [SerializeField] private float startCameraOffset;
    [SerializeField] private float startOrbitRadius;
    [SerializeField] private float runningOrbitRadius;
                     private Vector3 runningCameraOffset;
    [SerializeField] private float startCameraTilt;
    [SerializeField] private float runningCameraTilt;
    [SerializeField] private float startHeight;
    [SerializeField] private float runningHeight;
    [SerializeField] private float cameraTransitionTimeTotal;
                     private float cameraTransitionTimeLeft;
                     private Quaternion startCameraRotation;
                     private bool cameraShake;
                     private float cameraShakeTimeLeft;
                     private float cameraShakeTimeTotal;
                     private float cameraShakeCount;
                     private float cameraShakeFreq;
                     private float cameraShakeIntensity;      
    
    void Start()
    {
        gameOver = false;
        startedRunning = false;
        startCameraOffset = 3.4f;
        startOrbitRadius = 9.0f;
        runningOrbitRadius = 9.0f;
        runningCameraOffset = new Vector3(0.0f, runningHeight, -runningOrbitRadius);
        startCameraTilt = -10.0f;
        runningCameraTilt = -2.5f;
        cameraTransitionTimeTotal = 2.0f;
        cameraTransitionTimeLeft = cameraTransitionTimeTotal;
        startHeight = 2.0f;
        runningHeight = 4.0f;
        // cameraShake = false;
        cameraShakeTimeTotal = 0.5f;
        cameraShakeCount = 0;
        cameraShakeFreq = 20f;
        cameraShakeIntensity = 0.1f;
    }

    void LateUpdate()
    {
        if (player != null && !gameOver && !startedRunning)
        {
            float startCameraXOffset = Mathf.Sin(startCameraOffset) * startOrbitRadius;
            float startCameraZOffset = - Mathf.Cos(startCameraOffset) * startOrbitRadius;
            float startCameraYOffset = startHeight;
            targetPosition = player.position + new Vector3(startCameraXOffset, startCameraYOffset, startCameraZOffset);
            transform.position = targetPosition;

            transform.LookAt(player);
            transform.Rotate(new Vector3(startCameraTilt, 0, 0));
            startCameraRotation = transform.rotation;
        } 
        else if (player != null && !gameOver && startedRunning && cameraTransitionTimeLeft > 0)
        {
            cameraTransitionTimeLeft -= Time.deltaTime;
            cameraTransitionTimeLeft = Mathf.Clamp(cameraTransitionTimeLeft, 0, cameraTransitionTimeTotal);
            float t = (cameraTransitionTimeLeft / cameraTransitionTimeTotal);

            float orbitRadius = Mathf.Lerp(startOrbitRadius, runningOrbitRadius, 1 - t);
            float cameraXOffset = Mathf.Sin(t * startCameraOffset) * orbitRadius;
            float cameraZOffset = - Mathf.Cos(t * startCameraOffset) * orbitRadius;
            float cameraYOffset = Mathf.Lerp(startHeight, runningHeight, 1 - t);
            transform.position = player.position + new Vector3(cameraXOffset, cameraYOffset, cameraZOffset);

            float tilt = Mathf.Lerp(startCameraTilt, runningCameraTilt, 1 - t);
            transform.LookAt(player);
            transform.Rotate(new Vector3(tilt, 0, 0));
        } 
        else if (player != null && !gameOver && startedRunning && cameraTransitionTimeLeft <= 0)
        {
            targetPosition = new Vector3(player.position.x, player.position.y, player.position.z);
            runningCameraOffset = new Vector3(0.0f, runningHeight, -runningOrbitRadius);
            targetPosition += runningCameraOffset;
            transform.position = targetPosition;

            transform.LookAt(player);
            transform.Rotate(new Vector3(runningCameraTilt, 0, 0));

            if (cameraShakeTimeLeft > 0)
            {
                cameraShakeTimeLeft -= Time.deltaTime;
                cameraShakeTimeLeft = Mathf.Clamp(cameraShakeTimeLeft, 0, cameraShakeTimeTotal);
                float k = (cameraShakeTimeLeft / cameraShakeTimeTotal);
                float shake = Mathf.Sin(k * cameraShakeFreq * 2 * Mathf.PI) * cameraShakeCount * cameraShakeIntensity;
                transform.position += new Vector3(shake, shake, shake);
            }
        } 
        else 
        {
            targetPosition = new Vector3(player.position.x, player.position.y, player.position.z);
            runningCameraOffset = new Vector3(0.0f, runningHeight, -runningOrbitRadius);
            targetPosition += runningCameraOffset;
            transform.position = targetPosition;
            transform.LookAt(player);
            transform.Rotate(new Vector3(runningCameraTilt, 0, 0));
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

    public void ShakeCamera() {
        cameraShakeTimeLeft = cameraShakeTimeTotal;
        cameraShakeCount += 1;
    }
}
