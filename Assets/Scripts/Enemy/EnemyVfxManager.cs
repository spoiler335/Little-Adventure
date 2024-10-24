using UnityEngine;
using UnityEngine.VFX;

public class EnemyVfxManager : MonoBehaviour
{
    [SerializeField] private VisualEffect footStep;
    [SerializeField] private VisualEffect smashVfx;
    [SerializeField] private VisualEffect splashVfx;
    [SerializeField] private ParticleSystem beginHitVfx;

    private void Awake()
    {
        EventsModel.PLAY_ENEMY_BEGIN_HIT_VFX += PlayBeginHitVfx;
    }

    public void BurstFootStep() => footStep.SendEvent("OnPlay");
    public void PlaySmashVfx() => smashVfx.SendEvent("OnPlay");

    private void PlayBeginHitVfx(Vector3 attackerPosition, GameObject go)
    {
        if (go != gameObject) return;
        Vector3 forceFoward = transform.position - attackerPosition;
        forceFoward.Normalize();
        forceFoward.y = 0;
        beginHitVfx.transform.rotation = Quaternion.LookRotation(forceFoward);
        beginHitVfx.Play();

        Vector3 splashPos = transform.position;
        splashPos.y += 2;
        VisualEffect newSplashVfx = Instantiate(splashVfx, splashPos, Quaternion.identity);
        newSplashVfx.SendEvent("OnPlay");
        Destroy(newSplashVfx, 10f);
    }

    private void OnDestroy()
    {
        EventsModel.PLAY_ENEMY_BEGIN_HIT_VFX -= PlayBeginHitVfx;
    }
}