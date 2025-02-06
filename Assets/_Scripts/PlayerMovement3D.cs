using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Options")]
    public bool allowMovement = true;
    public bool forceMovement = true;
    [Range(0, 50)]
    public float movementSpeed = 5f;
    [Range(0, 100)]
    public float jumpHeight = 1f;
    public float gravity = -9.81f;
    private Vector3 moveDirection;
    private Vector3 velocity;
    private Transform cameraTransform;

    CharacterController controller;
    InputManager inputManager;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = GetComponent<InputManager>();
        cameraTransform = Camera.main.transform;
    }

    private void ApplyGravity()
    {
        if (IsGrounded() && velocity.y < 0) {
            velocity.y = -2.0f;
        } 
        if (!IsGrounded()) {
            velocity.y += gravity * Time.deltaTime;
        }
    }

    public void HandleMove()
    {
        if (!allowMovement){
            return;
        }
        
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        if (forceMovement){ 
            moveDirection = forward + right * inputManager.moveInput.x;
        } else {
            moveDirection = forward * inputManager.moveInput.y + right * inputManager.moveInput.x;
        }

        moveDirection.Normalize();
    }

    public void HandleJump()
    {
        if (inputManager.JumpPressed && IsGrounded()) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }
    }

    public bool IsGrounded()
    {
        return controller.isGrounded;
    }

    public void HandleAllMovement()
    {
        HandleMove();
        HandleJump();
        ApplyGravity();
        Vector3 finalVelocity = moveDirection * movementSpeed + velocity;
        controller.Move(finalVelocity * Time.deltaTime);
    }
}
