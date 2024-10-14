using UnityEngine;

public class Health : MonoBehaviour
{
    private const int MaxHealth = 100;

    public int currentHealth { get; private set; } = 0;

    private void Awake()
    {
        currentHealth = MaxHealth;
    }

    public void ApplyDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) currentHealth = 0;
        Debug.Log($"{gameObject.name} :: Curent-Health :: {currentHealth}");
    }
}