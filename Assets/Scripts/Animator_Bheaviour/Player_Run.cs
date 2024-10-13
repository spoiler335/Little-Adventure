using UnityEngine;

public class Player_Run : StateMachineBehaviour
{
    private PlayerVFXManager playerVFX;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // base.OnStateEnter(animator, stateInfo, layerIndex);
        if (!playerVFX) playerVFX = animator.GetComponent<PlayerVFXManager>();
        playerVFX.UpdateFootStep(true);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // base.OnStateExit(animator, stateInfo, layerIndex);
        if (!playerVFX) playerVFX = animator.GetComponent<PlayerVFXManager>();
        playerVFX.UpdateFootStep(false);
    }
}