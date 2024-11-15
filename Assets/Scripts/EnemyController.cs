using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enum Status {Idle, Patrolling, Staying, Returning}
    public float stayTime;

    private Transform group;
    private Status status = Status.Idle;
    private NavMeshAgent agent;
    private float targetRotation;
    private float returnTime;
    private Vector3 returnPos;
    private EnemyGroupController groupCtrl;

    private void Start()
    {
        group = transform.parent;
        transform.up = group.position - transform.position;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        returnPos = transform.position;
        groupCtrl = group.GetComponent<EnemyGroupController>();
    }

    private void Update()
    {
        if (agent.pathPending)
        {
            return;
        }

        switch (status)
        {
            case Status.Idle:
                break;
            case Status.Patrolling:
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    agent.ResetPath();
                    returnTime = Time.time + stayTime;
                    transform.rotation = Quaternion.Euler(0f, 0f, targetRotation + 180f);
                    status = Status.Staying;
                }
                break;
            case Status.Staying:
                if (Time.time > returnTime)
                {
                    agent.SetDestination(returnPos);
                    status = Status.Returning;
                }
                break;
            case Status.Returning:
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    agent.ResetPath();
                    transform.up = group.position - transform.position;
                    status = Status.Idle;
                    groupCtrl.NextPatrol();
                }    
                break;
        }

        if (agent.hasPath)
        {
            transform.up = (Vector2)(agent.steeringTarget - transform.position);
        }
    }

    public void StartPatrol(Transform target)
    {
        status = Status.Patrolling;
        agent.SetDestination(target.position);
        targetRotation = target.rotation.eulerAngles.z;
    }
}
