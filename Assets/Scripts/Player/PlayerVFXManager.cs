using UnityEngine;
using UnityEngine.VFX;

public class PlayerVFXManager : MonoBehaviour
{
    [SerializeField] private VisualEffect footvisualEffect;
    [SerializeField] private ParticleSystem blade01;
    [SerializeField] private ParticleSystem blade02;
    [SerializeField] private ParticleSystem blade03;
    [SerializeField] private VisualEffect slashVfx;
    [SerializeField] private VisualEffect healVfx;

    private void Awake()
    {
        EventsModel.PLAY_SLASH_VFX += PlaySlashVfx;
    }

    public void UpdateFootStep(bool state)
    {
        if (state) footvisualEffect.Play();
        else footvisualEffect.Stop();
    }

    public void PlayBlade01Vfx() => blade01.Play();
    public void PlayBlade02Vfx() => blade02.Play();
    public void PlayBlade03Vfx() => blade03.Play();

    public void StopBlade()
    {
        blade01.Simulate(0);
        blade01.Stop();

        blade02.Simulate(0);
        blade02.Stop();

        blade03.Simulate(0);
        blade03.Stop();
    }

    private void PlaySlashVfx(Vector3 pos)
    {
        slashVfx.transform.position = pos;
        slashVfx.Play();
    }

    public void PlayHealingVfx()
    {
        healVfx.Play();
    }

    private void OnDestroy()
    {
        EventsModel.PLAY_SLASH_VFX -= PlaySlashVfx;
    }
}