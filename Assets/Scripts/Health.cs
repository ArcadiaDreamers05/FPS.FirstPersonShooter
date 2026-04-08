using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isPlayer = false;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth = Mathf.Max(0f, currentHealth - amount);

        if (currentHealth <= 0f)
            Die();
    }

    void Die()
    {
        if (isPlayer)
        {
            GameManager.Instance.OnPlayerDied();
        }
        else
        {
            GameManager.Instance.OnEnemyKilled();
            Destroy(gameObject);
        }
    }
}