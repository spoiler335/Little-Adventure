using System;
using System.Collections;
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
    private Health health;
    private MaterialPropertyBlock materialPropertyBlock;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private Vector3 impactOnPlayer;
    private bool isPlayerInvincible;
    private float invincibleDuratrion = 2f;

    private InputManager input => DI.di.input;
    private void Awake()
    {
        character = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        materialPropertyBlock = new MaterialPropertyBlock();
        skinnedMeshRenderer.GetPropertyBlock(materialPropertyBlock);

        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        EventsModel.ADD_IMPACT_ON_PLAYER += AddImpactOnPlayer;
    }

    private void UnsubscribeEvents()
    {
        EventsModel.ADD_IMPACT_ON_PLAYER -= AddImpactOnPlayer;

    }

    private void OnDestroy() => UnsubscribeEvents();

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

        movementVelocity *= moveSpeed * Time.deltaTime;

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
                if (Time.time < attackStartTime + attackSlideDuration)
                {
                    float timePassed = Time.time - attackStartTime;
                    float lerpTime = timePassed / attackSlideDuration;
                    movementVelocity = Vector3.Lerp(transform.forward * attackSlideSpeed * Time.deltaTime, Vector3.zero, lerpTime);
                }
                break;
            case CharacterState.Dead:
                return;
            case CharacterState.BeginHit:
                if (impactOnPlayer.magnitude > 0.2f)
                {
                    movementVelocity = impactOnPlayer * Time.deltaTime;
                }
                impactOnPlayer = Vector3.Lerp(impactOnPlayer, Vector3.zero, Time.deltaTime * 5);
                break;
        }

        if (!character.isGrounded)
            verticalVelocity = gravity;
        else
            verticalVelocity = gravity * 0.3f;

        movementVelocity += verticalVelocity * Vector3.up * Time.deltaTime;

        character.Move(movementVelocity);
        movementVelocity = Vector3.zero;
    }

    private void SwitchStateTo(CharacterState newState)
    {
        switch (currentCharacterState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attacking:
                DisableDamageCaster();
                break;
            case CharacterState.BeginHit:
                break;
            case CharacterState.Dead:
                return;
        }

        switch (newState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attacking:
                animator.SetTrigger("Attack");
                attackStartTime = Time.time;
                break;
            case CharacterState.BeginHit:
                animator.SetTrigger("BeginHit");
                isPlayerInvincible = true;
                StartCoroutine(DelayCancelInvincible());
                break;
            case CharacterState.Dead:
                character.enabled = false;
                animator.SetTrigger("Death");
                StartCoroutine(MaterialDissolve());
                return;
        }

        currentCharacterState = newState;

        Debug.Log($"Player Swiching State To :: {currentCharacterState}");
    }

    public void AttackAnimEnds() => SwitchStateTo(CharacterState.Normal);

    public void BeginHitAnimEnds() => SwitchStateTo(CharacterState.Normal);

    public void ApplyDamage(int damageAmt)
    {
        if (isPlayerInvincible) return;

        health.ApplyDamage(damageAmt);

        StartCoroutine(MaterialBlink());

        SwitchStateTo(CharacterState.BeginHit);

        if (health.currentHealth <= 0) SwitchStateTo(CharacterState.Dead);
    }

    private void ApplyDamageImpact(Vector3 attackerPos, float force)
    {
        var impackDir = transform.position - attackerPos;
        impackDir.Normalize();
        impackDir.y = 0;
        impactOnPlayer = impackDir * force;
    }

    private void AddImpactOnPlayer(Vector3 attackPos)
    {
        ApplyDamageImpact(attackPos, 10f);
    }

    private IEnumerator DelayCancelInvincible()
    {
        yield return new WaitForSeconds(invincibleDuratrion);
        isPlayerInvincible = false;
    }

    public void EnableDamageCaster() => damageCaster.EnableDamageCaster();
    public void DisableDamageCaster() => damageCaster.DisableDamageCaster();

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
    }
}

public enum CharacterState
{
    Normal,
    Attacking,
    Dead,
    BeginHit
}