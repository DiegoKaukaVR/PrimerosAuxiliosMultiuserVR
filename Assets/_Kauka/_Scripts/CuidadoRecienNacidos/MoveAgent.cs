using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class MoveAgent : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Transform target;
    public Transform[] targets;
    Vector3 destination;

    public UnityEvent onPPCompleted;

    public TypeMovement typeMovement;
    public enum TypeMovement
    {
        Teleport,
        Walking,
        pathPoints,
    }


    private void Start()
    {
        navMeshAgent =GetComponent<NavMeshAgent>(); 
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, targets[indexPP].position) <= 0.3f)
        {
            indexPP++;
            PathPoints(indexPP);
        }
    }

    int indexPP = 0;

    public void GoToTarget()
    {
        switch (typeMovement)
        {
            case TypeMovement.Teleport:

                transform.position = target.position;
                transform.rotation = target.rotation;
                break;
            case TypeMovement.Walking:
                navMeshAgent.SetDestination(target.position);
                break;

            case TypeMovement.pathPoints:
                PathPoints(indexPP);
                break;

            default:
                break;
        }
    }

    void PathPoints(int index)
    {
        if (index >= targets.Length)
        {
            onPPCompleted.Invoke();
            navMeshAgent.ResetPath();
            navMeshAgent.isStopped = true;
            this.enabled = false;
            return;
        }

        navMeshAgent.SetDestination(targets[indexPP].position);
    }


    public void QuitMoveAgent()
    {
        navMeshAgent.enabled = false;
        this.enabled = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (target != null)
        {
            Gizmos.DrawRay(target.position, target.forward);
        }

        for (int i = 0; i < targets.Length-1; i++)
        {
            Gizmos.DrawLine(targets[i].position, targets[i + 1].position);
        }
    
    }
}
