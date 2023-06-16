using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteractions : MonoBehaviour
{
    public float Health = 100f;
    float currentHealth;
    private void Awake()
    {
        currentHealth = Health;
    }
    public float GetHealth()
    {
        return Health;
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    public void TakeDamage(float damage)
    {
        Debug.Log("enemy hit "  + damage);
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            //Won
            Destroy(gameObject);

        }
    }
}
