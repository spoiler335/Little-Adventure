using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController character;
    private float moveSpeed = 5f;
    private Vector3 movementVelocity;
    private float verticalVelocity;
    private const float gravity = -9.8f;
    private InputManager input => DI.di.input;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
    }

    private void CalculatePlayerMovement()
    {
        movementVelocity.Set(input.GetForward(), 0, input.GetRight());
        movementVelocity.Normalize();
        movementVelocity = Quaternion.Euler(0, -45, 0) * movementVelocity;
        movementVelocity *= moveSpeed * Time.fixedDeltaTime;

        if (movementVelocity != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(movementVelocity);
    }

    private void FixedUpdate()
    {
        CalculatePlayerMovement();

        if (!character.isGrounded)
            verticalVelocity = gravity;
        else
            verticalVelocity = gravity * 0.3f;

        movementVelocity += verticalVelocity * Vector3.up * Time.fixedDeltaTime;

        character.Move(movementVelocity);
    }
}
