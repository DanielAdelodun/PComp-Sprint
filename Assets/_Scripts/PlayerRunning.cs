using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy Will Start enemyDistance From Player
// They Need To Catch Up 1/3 * enemyDistance Every Crash
// Crash Lasts 3 Seconds

// Decrease Velocity By 1/3 * 1/3 * enemyDistance

public class StartRunning : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 0f;
    [SerializeField] private Animation animationComponent;
    [SerializeField] private string runAnimationName = "Running_A (1)";
    [SerializeField] private Transform playerTransform;
    private bool cameraTranstionRunning = false;
    private float cameraTransitionElapsed = 0.0f;
    private Camera currentCamera;    
    private Camera targetCamera;
    private float enemyDistance = 10.0f;
    [SerializeField] private float collisions = 0;

    void Start()
    {
        animationComponent = GetComponent<Animation>();
        // StartPlayerRun();
    }

    void Update()
    {

        // Slow Down Player If Crashed
        float speedReduction;
        if (collisions <= 0) {
            collisions = 0;
            speedReduction = 0;
        } else {
            collisions -= Time.deltaTime;
            speedReduction = enemyDistance / 3;
        }

        // Run Forward At Current Set Speed
        float currentXPos = transform.position.x;
        float currentYPos = transform.position.y;
        float currentZPos = transform.position.z;
        
        float nextZPos = currentZPos + (movementSpeed - speedReduction) * Time.deltaTime;

        Vector3 newPosition = new Vector3(currentXPos, currentYPos, nextZPos);
        transform.position = newPosition;

        if (cameraTranstionRunning) {
            if (cameraTransitionElapsed >= 1) {
                cameraTranstionRunning = false;
            }
            // Rotate From startCameraAngle To 0
            // Look At Character 
        }
    }

    public void StartPlayerRun() {
        // Start Running Animation
        animationComponent.Play(runAnimationName);

        // Start Camera Transition
        cameraTranstionRunning = true;

        // Give Initial Speed
        movementSpeed = 5.0f;
    }
    void OnTriggerEnter(Collider other)
    {
        collisions += 1.5f;
        Debug.Log("Collided");
        Debug.Log(collisions);
    }
}
