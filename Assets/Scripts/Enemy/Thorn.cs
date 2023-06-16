using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Thorn : MonoBehaviour
{
    public float damage;
    public float damageDef;
    private void Start()
    {
        damage += Random.Range(damageDef, -damageDef);
        transform.DOShakePosition(1f, .3f, 5).SetEase(Ease.InCirc).SetLoops(-1);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerInteractions>().TakeDamage(damage);
        }
    }
}
