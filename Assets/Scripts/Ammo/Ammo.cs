using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private float lifetime = 2f;
    [SerializeField] private float damage = 12f;
    [SerializeField] private float speed = 40f;
    [SerializeField] private float damageDeflection = 3f;
    public bool isPlayerAmmo = false;
    private float timer = 0f;

    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        rb.velocity = transform.forward * speed;
        damage += Random.Range(damageDeflection, -damageDeflection);
        
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            DestroyAmmo();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerInteractions>().TakeDamage(damage);
            DestroyAmmo();
        }
        else if (other.CompareTag("Enemy") && isPlayerAmmo)
        {
            other.GetComponent<EnemyInteractions>().TakeDamage(damage);
            DestroyAmmo();
        }

        if (!isPlayerAmmo && other.CompareTag("Shield"))
        {
            DestroyAmmo();
        }
        
    }
    public void DestroyAmmo()
    {
        Destroy(gameObject);
    }
}
