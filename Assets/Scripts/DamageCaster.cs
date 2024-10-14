using System.Collections.Generic;
using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    [SerializeField] private string targetTag;
    private Collider damageCasterCollider;
    private int damageAmt = 30;

    private List<Collider> damageTargetList = new List<Collider>();

    private void Awake()
    {
        damageCasterCollider = GetComponent<Collider>();
        damageCasterCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag) && !damageTargetList.Contains(other))
        {
            other.GetComponent<Health>().ApplyDamage(damageAmt);

            damageTargetList.Add(other);
        }
    }

    public void EnableDamageCaster()
    {
        damageTargetList.Clear();
        damageCasterCollider.enabled = true;
    }

    public void DisableDamageCaster()
    {
        damageTargetList.Clear();
        damageCasterCollider.enabled = false;
    }
}