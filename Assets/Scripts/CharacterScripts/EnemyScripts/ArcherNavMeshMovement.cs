using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArcherNavMeshMovement : NavMeshMovementBehaviour
{
    [SerializeField] private float _minDistanceRange;
    private float _minDistanceRangeSquare;
    private SkeletonAnimation _skeletonAnimation;
    private ArcherEnemyScript _archerScript;
    private float _attackRangeSquare;

    protected override void Start()
    {
        base.Start();

        _skeletonAnimation = GetComponent<SkeletonAnimation>();
        _archerScript = GetComponent<ArcherEnemyScript>();
        _minDistanceRangeSquare = _minDistanceRange * _minDistanceRange;
        _attackRangeSquare = _attackRange * _attackRange;
    }

    protected override void HandleMovement()
    {
        if (_target == null)
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.velocity = Vector3.zero; // Stop the agent's velocity
            return;
        }

        float sqrDistanceToTarget = (transform.position - _target.transform.position).sqrMagnitude - 1;
        if (sqrDistanceToTarget <= _attackRangeSquare)
        {
            if (sqrDistanceToTarget < (_minDistanceRangeSquare) && (!_skeletonAnimation.IsAttacking() || !_archerScript.IsAttacking))
            {
                Vector3 directionAwayFromTarget = (transform.position - _target.transform.position).normalized;
                Vector3 newDestination = transform.position + directionAwayFromTarget * (_minDistanceRange - Mathf.Sqrt(sqrDistanceToTarget));
                _navMeshAgent.SetDestination(newDestination);
                _navMeshAgent.isStopped = false;
            }
            else
            {
                _navMeshAgent.isStopped = true;
                _navMeshAgent.velocity = Vector3.zero; // Stop the agent's velocity
            }
            return;
        }

        // Should the target move, we should recalculate our path
        if ((_target.transform.position - _previousTargetPosition).sqrMagnitude > MOVEMENT_EPSILON)
        {
            _navMeshAgent.SetDestination(_target.transform.position);
            _navMeshAgent.isStopped = false;
            _previousTargetPosition = _target.transform.position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _minDistanceRange);
    }
}
