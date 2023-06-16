using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public float Health = 100f;
    float currentHealth;
    private void Awake()
    {
        currentHealth = Health;
    }
    public void TakeDamage(float damage)
    {
        Debug.Log("player hit " + damage);
        currentHealth -= damage;
    }
    public float GetHealth()
    {
        return Health;
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    /* private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Ammo"))
        {
            Debug.Log("Çarptý");
            //Take Damage
            //
            //

            hit .gameObject.GetComponent<Ammo>().DestroyAmmo();
        }
    }*/
}
