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

    private void Awake()
    {
        character = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        speedHash = Animator.StringToHash("Speed");
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
        }
    }

    private void FixedUpdate() => CalculateEnemyMovement();
}
