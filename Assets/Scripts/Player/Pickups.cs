using UnityEngine;

public class Pickups : MonoBehaviour
{
    [field: SerializeField] public PickUpType pickUpType { get; private set; }
    [SerializeField] private ParticleSystem collectedVfx;
    public int value = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().PickupItem(this);
            if (collectedVfx) collectedVfx.Play();
            Destroy(gameObject);
        }
    }
}

public enum PickUpType
{
    Health,
    Coin
}