using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy Will Start enemyDistance From Player
// They Need To Catch Up 1/totalLives * enemyDistance Every Crash

// When You Crash, You Want To End Up 1/totalLives * enemyDistance Closer To Enemy
// => Velocity -= 1/collisionTimeTotal * 1/totalLives * enemyDistance

public class PlayerRunning : MonoBehaviour
{
    // TODO Get These From Game Manager?...
    private bool startedRunning;
    private bool gameOver;
    [SerializeField] private float startSpeed;
    [SerializeField] private float movementSpeed;
    [SerializeField] float speedReduction;
    [SerializeField] float currentSpeed;
    [SerializeField] float minimumSpeed;
    [SerializeField] private float enemyDistance;
    [SerializeField] private float collisionTimeLeft;
    [SerializeField] private float collisionTimeTotal;
    private Animation animationComponent;
    private string runAnimationName;
    [SerializeField] private HealthManager livesManager;

    void Start()
    {
        startedRunning = false;
        gameOver = false;
        startSpeed = 10.0f;
        movementSpeed = 0f;
        minimumSpeed = 0.1f;
        enemyDistance = 10.0f;
        collisionTimeLeft = 0.0f;
        collisionTimeTotal = 0.5f;
        animationComponent = GetComponent<Animation>();
        runAnimationName = "Running_A (1)";
    }

    void Update()
    {
        if (!startedRunning || gameOver) return;

        // Slow Down Player If Crashed
        if (collisionTimeLeft <= 0) {
            collisionTimeLeft = 0;
            speedReduction = 0;
        } else {
            collisionTimeLeft -= Time.deltaTime;
            speedReduction = (1.0f / collisionTimeTotal) * enemyDistance * (1.0f / livesManager.totalLives);
        }

        // Make Sure We Don't Start Moving Backwards
        currentSpeed = (movementSpeed - speedReduction);
        currentSpeed = Mathf.Clamp(currentSpeed, minimumSpeed, movementSpeed);

        // Save Current Position
        float currentXPos = transform.position.x;
        float currentYPos = transform.position.y;
        float currentZPos = transform.position.z;
        
        // Move Forward. Our Velocity is `movementSpeed - speedReduction` (Clamped).
        float nextZPos = currentZPos + currentSpeed * Time.deltaTime;
        Vector3 newPosition = new Vector3(currentXPos, currentYPos, nextZPos);
        transform.position = newPosition;
    }

    public void StartPlayerRun() {
        // Start Running Animation
        animationComponent.Play(runAnimationName);

        // Give Initial Speed
        movementSpeed = startSpeed;

        // Start
        startedRunning = true;
    }
    void OnTriggerEnter(Collider other)
    {
        // Lose Life
        livesManager.LifeLost();

        // Slow Down For Set Time
        collisionTimeLeft += collisionTimeTotal;
    }

    public void GameOver() {
        gameOver = true;
    }
}
