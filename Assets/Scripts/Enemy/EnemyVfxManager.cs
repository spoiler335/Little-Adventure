using UnityEngine;
using UnityEngine.VFX;

public class EnemyVfxManager : MonoBehaviour
{
    [SerializeField] private VisualEffect footStep;
    [SerializeField] private VisualEffect smashVfx;

    public void BurstFootStep()
    {
        footStep.SendEvent("OnPlay");
    }

    public void PlaySmashVfx()
    {
        smashVfx.SendEvent("OnPlay");
    }
}