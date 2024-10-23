using System.Threading;
using UnityEngine;

public class DamageOrb : MonoBehaviour
{
    [SerializeField] private ParticleSystem hitVfx;
    private float speed = 2f;
    private int damage = 10;
    private Rigidbody rb;

    private static int count = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Debug.Log($"Orb-{count} Instantiated");
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Orb-{count} Hit {other.name}");
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().ApplyDamage(damage);
        }

        Instantiate(hitVfx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


}