using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform playerTrans;

    private CharacterController character;
    private float moveSpeed = 2f;
    private Vector3 movementVelocity;
    private NavMeshAgent agent;
    private Animator anim;
    private int speedHash;
    private CharacterState currentCharacterState;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        speedHash = Animator.StringToHash("Speed");
    }

    private void Start()
    {
        currentCharacterState = CharacterState.Normal;
    }

    private void CalculateEnemyMovement()
    {
        if (Vector3.Distance(transform.position, playerTrans.position) >= agent.stoppingDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(playerTrans.position);
            anim.SetFloat(speedHash, 0.2f);
        }
        else
        {
            agent.isStopped = true;
            anim.SetFloat(speedHash, 0);
            SwitchStateTo(CharacterState.Attacking);
        }
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
                transform.rotation = Quaternion.LookRotation(playerTrans.position - transform.position);
                anim.SetTrigger("Attack");
                break;
        }

        currentCharacterState = newState;

        Debug.Log($"Enemy Swiching State To :: {currentCharacterState}");
    }
    public void AttackAnimEnds()
    {
        Debug.Log($"Enemy Attack Animation Ends");
        SwitchStateTo(CharacterState.Normal);
    }

    private void Update()
    {
        switch (currentCharacterState)
        {
            case CharacterState.Normal:
                CalculateEnemyMovement();
                break;
            case CharacterState.Attacking:
                break;
        }
    }
}
