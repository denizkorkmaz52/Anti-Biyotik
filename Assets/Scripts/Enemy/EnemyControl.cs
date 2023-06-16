using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] public float fireRate;
    [SerializeField] public Transform firePoint;

    private Animator animator;
    private Vector3 aimDir;
    private Attack attack;

    float timer = 0f;
    float attackDelay = 1.15f;
    // Start is called before the first frame update
    void Start()
    {
        attack = GetComponent<Attack>();
        animator = GetComponent<Animator>();
        InitializeAnimatorParameters();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
    }

    private void InitializeAnimatorParameters()
    {
        animator.SetBool("Attack", false);
    }
    public void Attack(Transform target)
    {
        if(timer <= 0)
        {
            StartCoroutine(AttackDelay(target));
            timer = fireRate;
        }
            
    }
    private IEnumerator AttackDelay(Transform target)
    {
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(attackDelay);
        aimDir = ((target.position - firePoint.position) + new Vector3(0f, (firePoint.position.y - target.position.y)/2, 0f)).normalized;
        attack.Fire(firePoint, aimDir);
        animator.SetBool("Attack", false);
    }
}
