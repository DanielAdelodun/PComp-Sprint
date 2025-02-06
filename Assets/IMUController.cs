using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMUController : MonoBehaviour
{
    // Start is called before the first frame update

    private float initXPos;
    private float initYPos;
    private float initZPos;
    private float tiltToTranslateScaleFactor = -0.05f;
    void Start()
    {
        initXPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {

        // Get Raw Values From The HID Gamepad
        // (TODO: Does Unity Label Axes Consistently Across OSes?)
        float Qw = Input.GetAxisRaw("Qw");
        float Qx = Input.GetAxisRaw("Qx");
        float Qy = Input.GetAxisRaw("Qy");
        float Qz = Input.GetAxisRaw("Qz");


        // Initilise Quaternion
        Quaternion Q = Quaternion.identity;
        Q.Set(Qx, -Qy, -Qz, Qw);

        // Get Euler Z Rotation. Turn that into side-to-side movement
        Vector3 eulers = transform.rotation.eulerAngles;
        float zRotation = eulers.z - 180;
        float xTranslation = zRotation * tiltToTranslateScaleFactor;

        // Translate and Rotate Game Object
        transform.rotation = Q;

        float nextXPos = initXPos + xTranslation;
        float currentYPos = transform.position.y;
        float currentZPos = transform.position.z;

        Vector3 newPosition = new Vector3(nextXPos, currentYPos, currentZPos);
        transform.position = newPosition;

        // Debug.Log(Q.ToString());
        Debug.Log(zRotation);
    
    }
}