using UnityEngine;

public class DamageOrb : MonoBehaviour
{
    [SerializeField] private ParticleSystem hitVfx;
    private float speed = 2f;
    private int damage = 10;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().ApplyDamage(damage);
        }

        Instantiate(hitVfx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}