using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialities : MonoBehaviour
{
    [SerializeField] private GameObject shield;
    [SerializeField] private float shieldCooldown = 4f;
    [SerializeField] private float shieldDuration = 2f;
    
    float shieldTimer = 0f;
    
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && shieldTimer <= 0f)
        {
            shieldTimer = shieldCooldown;
            StartCoroutine(ActivateShield());
        }
        shieldTimer -= Time.deltaTime;
        
    }

    public float GetShieldTimer()
    {
        return (shieldCooldown - shieldTimer)/shieldCooldown;
    }
    private IEnumerator ActivateShield()
    {
        shield.SetActive(true);
        yield return new WaitForSeconds(shieldDuration);
        shield.SetActive(false);
    }
}
