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
            if (other.TryGetComponent(out EnemyController enemy))
            {
                enemy.ApplyDamage(damageAmt);
            }

            if (other.TryGetComponent(out PlayerController player))
            {
                player.ApplyDamage(damageAmt);
            }

            RaycastHit hit;

            Vector3 originalPos = transform.position - damageCasterCollider.bounds.extents.z * transform.forward;

            bool isHit = Physics.BoxCast(originalPos, damageCasterCollider.bounds.extents / 2, transform.forward, out hit, transform.rotation, damageCasterCollider.bounds.extents.z, 1 << 6);

            if (isHit)
            {
                EventsModel.PLAY_SLASH_VFX?.Invoke(hit.point + new Vector3(0, 0.5f, 0));
            }

            EventsModel.PLAY_ENEMY_BEGIN_HIT_VFX?.Invoke(transform.parent.position);

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