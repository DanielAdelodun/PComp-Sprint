using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMUController : MonoBehaviour
{
    private float initXPos;
    private float initYPos;
    private float initZPos;
    private float tiltToTranslateScaleFactor = -0.2f;
    [SerializeField] private Quaternion initialOrientation;
    [SerializeField] private Quaternion orientationRaw;
    [SerializeField] private Quaternion calibratedOrientation;   // Relative to Initial
    [SerializeField] private Quaternion headTilt;                // Only Z-Axis Rotation
    [SerializeField] Vector3 eulers;
    [SerializeField] float zRotation;
    void Start()
    {
        initXPos = transform.position.x;

        // Wait For Valid HID Joystick Input


        // Save Initial IMU Orientation.
        float Qw = Input.GetAxisRaw("Qw");
        float Qx = Input.GetAxisRaw("Qx");
        float Qy = Input.GetAxisRaw("Qy");
        float Qz = Input.GetAxisRaw("Qz");
        initialOrientation.Set(Qx, -Qy, -Qz, Qw);
    }

    void Update()
    {
        // Get Raw Values From The HID Gamepad For Orientation
        float Qw = Input.GetAxisRaw("Qw");
        float Qx = Input.GetAxisRaw("Qx");
        float Qy = Input.GetAxisRaw("Qy");
        float Qz = Input.GetAxisRaw("Qz");

        orientationRaw = new Quaternion(Qx, -Qy, -Qz, Qw);

        // Spherical Difference 
        calibratedOrientation = orientationRaw * Quaternion.Inverse(initialOrientation);

        // Only Allow Z-Axis Rotation
        headTilt = Quaternion.Euler(0, 0, eulers.z);
        transform.rotation = headTilt;

        // Euler Z Rotation (Left-Right Head Tilt) --> X (Side-to-Side) Movement
        eulers = calibratedOrientation.eulerAngles;
        zRotation = eulers.z;
        if (zRotation > 180) {
            zRotation -= 360;
        }
        float xTranslation = zRotation * tiltToTranslateScaleFactor;

        float nextXPos = initXPos + xTranslation;
        float currentYPos = transform.position.y;
        float currentZPos = transform.position.z;

        Vector3 newPosition = new Vector3(nextXPos, currentYPos, currentZPos);
        transform.position = newPosition;
    }
}

