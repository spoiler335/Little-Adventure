using UnityEngine;

public class Pickups : MonoBehaviour
{
    public PickUpType pickUpType;
    public int value = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().PickupItem(this);
            Destroy(gameObject);
        }
    }
}

public enum PickUpType
{
    Health,
    Coin
}