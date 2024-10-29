using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHealth { get; private set; } = 0;

    private void Awake()
    {
        currentHealth = Constants.MAX_HEALTH;
    }

    public void ApplyDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) currentHealth = 0;
        Debug.Log($"{gameObject.name} :: Curent-Health :: {currentHealth}");
    }

    public void Increasehealth(int value)
    {
        currentHealth += value;
        if (currentHealth >= Constants.MAX_HEALTH) currentHealth = Constants.MAX_HEALTH;
    }
}