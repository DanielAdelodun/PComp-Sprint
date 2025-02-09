using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy Will Start enemyDistance From Player
// They Need To Catch Up 1/3 * enemyDistance Every Crash

// When You Crash, You Want To End Up 1/3 * enemyDistance Closer To Enemy
// => Velocity -= 1/collisionTimeTotal * 1/3 * enemyDistance

public class PlayerRunning : MonoBehaviour
{
    [SerializeField] private float startSpeed = 10.0f;
    [SerializeField] private float movementSpeed = 0f;
    [SerializeField] float currentSpeed;
    [SerializeField] float speedReduction;
    [SerializeField] float minimumSpeed = 0.1f;
    [SerializeField] private float enemyDistance = 10.0f;
    [SerializeField] private float collisionTimeLeft = 0.0f;
    [SerializeField] private float collisionTimeTotal = 0.5f;
    private Animation animationComponent;
    private string runAnimationName = "Running_A (1)";

    void Start()
    {
        animationComponent = GetComponent<Animation>();
        // StartPlayerRun();
    }

    void Update()
    {

        // Slow Down Player If Crashed
        if (collisionTimeLeft <= 0) {
            collisionTimeLeft = 0;
            speedReduction = 0;
        } else {
            collisionTimeLeft -= Time.deltaTime;
            speedReduction = (1.0f / collisionTimeTotal) * enemyDistance * (1.0f / 3.0f);
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
    }
    void OnTriggerEnter(Collider other)
    {
        collisionTimeLeft += collisionTimeTotal;
        Debug.Log("Collided");
    }
}
