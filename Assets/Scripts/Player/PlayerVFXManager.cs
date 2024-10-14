using UnityEngine;
using UnityEngine.VFX;

public class PlayerVFXManager : MonoBehaviour
{
    [SerializeField] private VisualEffect footvisualEffect;
    [SerializeField] private ParticleSystem blade01;

    public void UpdateFootStep(bool state)
    {
        if (state) footvisualEffect.Play();
        else footvisualEffect.Stop();
    }

    public void PlayBlade01Vfx()
    {
        blade01.Play();
    }
}