using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    CharacterController controller;
    InputManager inputManager;

    public float mouseSensitivity = 100f;
    
    private float xRotation = 0f;

    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponentInParent<CharacterController>();
        inputManager = GetComponentInParent<InputManager>();     
    }

    void UpdateLookValues() {
        float mouseX = inputManager.lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = inputManager.lookInput.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        controller.transform.Rotate(Vector3.up, mouseX);
    }
    // Update is called once per frame
    void Update()
    {
        UpdateLookValues();
    }
}
