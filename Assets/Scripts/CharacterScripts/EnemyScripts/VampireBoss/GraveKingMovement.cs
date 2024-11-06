using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class GraveKingMovement : NavMeshMovementBehaviour
{
    protected override void Start()
    {
        _attackRange = GetComponent<GraveKing>().GetAttackRange;
    }

    protected override void HandleMovement()
    {
        if (_target == null)
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.velocity = Vector3.zero; // Stop the agent's velocity
            return;
        }

        float sqrDistanceToTarget = (transform.position - _target.transform.position).sqrMagnitude;
        if (sqrDistanceToTarget <= _attackRange)
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.velocity = Vector3.zero; // Stop the agent's velocity
            return;
        }

        // Should the target move, we should recalculate our path
        if ((_target.transform.position - _previousTargetPosition).sqrMagnitude > MOVEMENT_EPSILON)
        {
            // Make the GraveKing look at the player
            transform.LookAt(_target.transform.position);
            _navMeshAgent.isStopped = true; // Ensure the agent is stopped
            _previousTargetPosition = _target.transform.position;
        }
    }
    protected override void FixedUpdate()
    {
        HandleMovement();
    }
}

