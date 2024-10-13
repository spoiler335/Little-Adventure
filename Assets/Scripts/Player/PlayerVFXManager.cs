using UnityEngine;
using UnityEngine.VFX;

public class PlayerVFXManager : MonoBehaviour
{
    [SerializeField] private VisualEffect footvisualEffect;

    public void UpdateFootStep(bool state)
    {
        if (state) footvisualEffect.Play();
        else footvisualEffect.Stop();
    }
}