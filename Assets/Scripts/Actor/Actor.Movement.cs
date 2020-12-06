using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public partial class Actor : MonoBehaviour
{
    [Header("Movement: Parameters")]
    [SerializeField] private MovementType movementType = MovementType.WALK_NORMAL;
    [SerializeField] private float movementSpeed = 4F;

    [Header("Movement: Destination")]
    [SerializeField] private Vector3 destinationPosition;
    [SerializeField] private Actor destinationActor;

    public MovementType GetMovementType() { return movementType; }
    public float GetMovementSpeed() { return movementSpeed; }

    public bool CheckDestinationReached()
    {
        return transform.position == destinationPosition;
    }

    public void SetDestination(Vector3 targetPosition, Actor targetActor, float stoppingDistance = 0, float retreatDistance = 0)
    {
        destinationActor = targetActor;
        destinationPosition = destinationActor ? destinationActor.transform.position : targetPosition;

        Vector3 position = transform.position;
        if (position == destinationPosition) return;

        if (retreatDistance > 0)
        {
            float distance = Vector3.Distance(position, destinationPosition);
            if (distance < retreatDistance)
            {
                if (stoppingDistance == 0) stoppingDistance = retreatDistance;
                Vector3 retreatDir = (destinationPosition - position).normalized;
                Vector3 retreatPos = destinationPosition - (retreatDir * stoppingDistance);
                if (NavMesh.SamplePosition(retreatPos, out NavMeshHit hit, distance, NavMesh.AllAreas))
                {
                    destinationPosition = hit.position;
                }
            }

        }
        navMeshAgent.SetDestination(destinationPosition);
        navMeshAgent.stoppingDistance = stoppingDistance;
    }
}
