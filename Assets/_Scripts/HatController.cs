using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatController : MonoBehaviour
{
    private bool startedRunning = false;
    private bool gameOver = false;
    private float initXPos;
    private float initYPos;
    private float initZPos;
    [SerializeField] private float tiltMovementSpeed = -0.15f;
    [SerializeField] private Quaternion initialOrientation;
    [SerializeField] private Quaternion orientationRaw;
    [SerializeField] private Quaternion calibratedOrientation; // Relative to Initial
    [SerializeField] private Quaternion headTilt;              // Only Z-Axis Rotation
    [SerializeField] Vector3 eulers;
    [SerializeField] float zRotation;
    [SerializeField] float calibrate;
    void Start()
    {
        initXPos = transform.position.x;

        // Save Initial IMU Orientation.
        float Qw = Input.GetAxisRaw("Qw");
        float Qx = Input.GetAxisRaw("Qx");
        float Qy = Input.GetAxisRaw("Qy");
        float Qz = Input.GetAxisRaw("Qz");
        // initialOrientation.Set(Qx, -Qy, -Qz, Qw);
        initialOrientation.Set(0, 0, 0, 1);
    }

    void Update()
    {
        if (!startedRunning || gameOver) return;

        // Get Raw Values From The HID Gamepad For Orientation
        float Qw = Input.GetAxis("Qw");
        float Qx = Input.GetAxis("Qx");
        float Qy = Input.GetAxis("Qy");
        float Qz = Input.GetAxis("Qz");
        orientationRaw = new Quaternion(Qx, -Qy, -Qz, Qw);

        // Orientation Relative To Inital
        calibratedOrientation = orientationRaw * Quaternion.Inverse(initialOrientation);

        // Euler Z Rotation (Left-Right Head Tilt) --> X (Side-to-Side) Movement
        eulers = calibratedOrientation.eulerAngles;
        zRotation = eulers.z;
        if (zRotation > 180) {
            zRotation -= 360;
        }
        float xTranslation = zRotation * tiltMovementSpeed;

        // Only Allow Z-Axis Rotation
        headTilt = Quaternion.Euler(0, 0, eulers.z);
        transform.rotation = headTilt;

        float nextXPos = initXPos + xTranslation;
        float currentYPos = transform.position.y;
        float currentZPos = transform.position.z;

        // Recalibrate Orientation If Needed
        calibrate = Input.GetAxis("Calibrate");
        if (calibrate == 1) {
            initialOrientation = orientationRaw;
        }

        Vector3 newPosition = new Vector3(nextXPos, currentYPos, currentZPos);
        transform.position = newPosition;
    }

    public void StartRunning(){
        startedRunning = true;
    }

    public void GameOver() {
        gameOver = true; 
    }
}

