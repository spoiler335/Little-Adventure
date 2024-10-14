using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private DamageCaster damageCaster;
    private CharacterController character;
    private float moveSpeed = 10f;
    private Vector3 movementVelocity;
    private float verticalVelocity;
    private const float gravity = -9.8f;
    private Animator animator;
    private int speedHash;
    private int airBorneHash;
    private CharacterState currentCharacterState;
    private float attackStartTime;
    private float attackSlideDuration = 0.1f;
    private float attackSlideSpeed = 1.5f;

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
        if (input.IsAttackClicked() && character.isGrounded)
        {
            SwitchStateTo(CharacterState.Attacking);
            return;
        }

        movementVelocity.Set(input.GetForward(), 0, input.GetRight());
        movementVelocity.Normalize();
        movementVelocity = Quaternion.Euler(0, -45, 0) * movementVelocity;

        animator.SetFloat(speedHash, movementVelocity.magnitude);

        movementVelocity *= moveSpeed * Time.fixedDeltaTime;

        if (movementVelocity != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(movementVelocity);

        animator.SetBool(airBorneHash, !character.isGrounded);
    }

    private void Update()
    {
        switch (currentCharacterState)
        {
            case CharacterState.Normal:
                CalculatePlayerMovement();
                break;
            case CharacterState.Attacking:
                movementVelocity = Vector3.zero;
                if (Time.time < attackStartTime + attackSlideDuration)
                {
                    float timePassed = Time.time - attackStartTime;
                    float lerpTime = timePassed / attackSlideDuration;
                    movementVelocity = Vector3.Lerp(transform.forward * attackSlideSpeed, Vector3.zero, lerpTime);
                }
                break;
        }

        if (!character.isGrounded)
            verticalVelocity = gravity;
        else
            verticalVelocity = gravity * 0.3f;

        movementVelocity += verticalVelocity * Vector3.up * Time.fixedDeltaTime;

        character.Move(movementVelocity);
    }

    private void SwitchStateTo(CharacterState newState)
    {
        switch (currentCharacterState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attacking:
                break;
        }

        switch (newState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attacking:
                animator.SetTrigger("Attack");
                attackStartTime = Time.time;
                break;
        }

        currentCharacterState = newState;

        Debug.Log($"Player Swiching State To :: {currentCharacterState}");
    }

    public void AttackAnimEnds()
    {
        Debug.Log($"Player Attack Animation Ends");
        SwitchStateTo(CharacterState.Normal);
    }

    public void EnableDamageCaster() => damageCaster.EnableDamageCaster();
    public void DisableDamageCaster() => damageCaster.DisableDamageCaster();
}

public enum CharacterState
{
    Normal,
    Attacking
}