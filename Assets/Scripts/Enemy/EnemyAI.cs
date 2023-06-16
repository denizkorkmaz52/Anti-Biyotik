using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private EnemyControl enemyControl;
    public LayerMask playerLayerMask;
    [SerializeField] private float fovRange = 25f;
    [SerializeField] private float fireRange = 15f;
    private Vector3 startingPos;
    private Vector3 roamingPos;
    float reachedRoamPos = 1f;
    float stoppingDistance;
    bool foundTarget = false;
    Transform target;
    [SerializeField] Collider[] colliders;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        roamingPos = GetRoamingPos();
        enemyControl = GetComponent<EnemyControl>();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(roamingPos);
        stoppingDistance = agent.stoppingDistance;
    }

    // Update is called once per frame
    void Update()
    {
        //Move enemy towards roaming pos
        //
        
        target = FindTarget();
        if (target != null)
        {
            foundTarget = true;
            agent.SetDestination(target.position);
            agent.stoppingDistance = fireRange;
            if (Vector3.Distance(target.position, transform.position) < fireRange)
            {
                enemyControl.Attack(target);
            }
        }
        else if ((Vector3.Distance(transform.position, roamingPos) < reachedRoamPos && !foundTarget) || !agent.hasPath)
        {
            roamingPos = GetRoamingPos();
            agent.SetDestination(roamingPos);
            agent.stoppingDistance = stoppingDistance;
        }    
    }
    private Vector3 GetRoamingPos()
    {
        Vector3 moveDir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;

        return startingPos + moveDir * Random.Range(10f, 15f);
    }

    private Transform FindTarget()
    {
        colliders = Physics.OverlapSphere(transform.position, fovRange, playerLayerMask);
        if (colliders.Length > 0)
        {
            return colliders[0].transform;
        }
        return null;
    }

}
