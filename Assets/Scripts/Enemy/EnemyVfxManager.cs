using UnityEngine;
using UnityEngine.VFX;

public class EnemyVfxManager : MonoBehaviour
{
    [SerializeField] private VisualEffect footStep;

    public void BurstFootStep()
    {
        footStep.SendEvent("OnPlay");
    }
}