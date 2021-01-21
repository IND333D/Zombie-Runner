using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaserange = 5f;
    [SerializeField] float turnspeed = 2f;

    float distancetotarget = Mathf.Infinity;
    NavMeshAgent navenemy;
    bool isprovoked = false;

    // Start is called before the first frame update
    void Start()
    {
        navenemy = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distancetotarget = Vector3.Distance(target.position, transform.position);
        if (isprovoked)
        {
            EngageTarget();
        }
        else if (distancetotarget <= chaserange)
        {
            isprovoked = true;
        }
        if(distancetotarget > chaserange)
        {
            isprovoked = false;
        }
    }

    public void OnDamageTaken()
    {
        isprovoked = true;
    }

    private void EngageTarget()
    {
        FaceTarget();
        if (distancetotarget>= navenemy.stoppingDistance)
        {
            ChaseTarget();
        }
        if (distancetotarget <= navenemy.stoppingDistance)
        {
            AttackTarget();
        }

    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookrotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, turnspeed * Time.deltaTime);
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("Attack", true);
        print("dead");
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("Attack", false);
        GetComponent<Animator>().SetTrigger("Move");
        navenemy.SetDestination(target.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaserange);
    }
}
