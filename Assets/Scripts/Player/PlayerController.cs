using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController character;
    private float moveSpeed = 5f;
    private Vector3 movementVelocity;
    private float verticalVelocity;
    private const float gravity = -9.8f;
    private Animator animator;
    private int speedHash;
    private int airBorneHash;
    private InputManager input => DI.di.input;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        speedHash = Animator.StringToHash("Speed");
        airBorneHash = Animator.StringToHash("AirBorne");
    }

    private void CalculatePlayerMovement()
    {
        movementVelocity.Set(input.GetForward(), 0, input.GetRight());
        movementVelocity.Normalize();
        movementVelocity = Quaternion.Euler(0, -45, 0) * movementVelocity;

        animator.SetFloat(speedHash, movementVelocity.magnitude);

        movementVelocity *= moveSpeed * Time.fixedDeltaTime;

        if (movementVelocity != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(movementVelocity);

        animator.SetBool(airBorneHash, !character.isGrounded);
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
