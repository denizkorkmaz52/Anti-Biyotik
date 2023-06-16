using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] public GameObject ammoPrefab;
    
    public void Fire(Transform firePoint, Vector3 aimDir)
    {
        Instantiate(ammoPrefab, firePoint.position, Quaternion.LookRotation(aimDir, Vector3.up));      
    }
}
