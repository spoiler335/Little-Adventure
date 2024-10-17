using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform playerTrans;
    [SerializeField] private GameObject healthOrb;

    private CharacterController character;
    private float moveSpeed = 2f;
    private Vector3 movementVelocity;
    private NavMeshAgent agent;
    private Animator anim;
    private int speedHash;
    private CharacterState currentCharacterState;
    private MaterialPropertyBlock materialPropertyBlock;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private Health health;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        speedHash = Animator.StringToHash("Speed");
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        materialPropertyBlock = new MaterialPropertyBlock();
        skinnedMeshRenderer.GetPropertyBlock(materialPropertyBlock);
        health = GetComponent<Health>();
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
            case CharacterState.Dead:
                return;
        }

        switch (newState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attacking:
                transform.rotation = Quaternion.LookRotation(playerTrans.position - transform.position);
                anim.SetTrigger("Attack");
                break;
            case CharacterState.Dead:
                character.enabled = false;
                anim.SetTrigger("Death");
                StartCoroutine(MaterialDissolve());
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

    public void ApplyDamage(int damageAmt)
    {
        health.ApplyDamage(damageAmt);

        if (health.currentHealth <= 0) SwitchStateTo(CharacterState.Dead);

        StartCoroutine(MaterialBlink());
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
            case CharacterState.Dead:
                return;
        }
    }

    private IEnumerator MaterialBlink()
    {
        materialPropertyBlock.SetFloat("_blink", 0.4f);
        skinnedMeshRenderer.SetPropertyBlock(materialPropertyBlock);

        yield return new WaitForSeconds(0.2f);

        materialPropertyBlock.SetFloat("_blink", 0);
        skinnedMeshRenderer.SetPropertyBlock(materialPropertyBlock);
    }

    private IEnumerator MaterialDissolve()
    {
        yield return new WaitForSeconds(2f);

        float dissolveTimeDuratrion = 2f;
        float currentDisolveTime = 0;
        float dissolveHeight_start = 20f;
        float dissolveHeight_target = -10f;
        float dissolveHeight;

        materialPropertyBlock.SetFloat("_enableDissolve", 1f);
        skinnedMeshRenderer.SetPropertyBlock(materialPropertyBlock);

        while (currentDisolveTime < dissolveTimeDuratrion)
        {
            currentDisolveTime += Time.deltaTime;
            dissolveHeight = Mathf.Lerp(dissolveHeight_start, dissolveHeight_target, currentDisolveTime / dissolveTimeDuratrion);
            materialPropertyBlock.SetFloat("_dissolve_height", dissolveHeight);
            skinnedMeshRenderer.SetPropertyBlock(materialPropertyBlock);
            yield return null;
        }

        DropHeathOrb();
    }

    private void DropHeathOrb()
    {
        var _item = Instantiate(healthOrb, transform.position, Quaternion.identity);
    }
}
