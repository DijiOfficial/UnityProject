using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]

public class NecromancerMovement : NavMeshMovementBehaviour
{
    [SerializeField] private GameObject _summonPrefab;
    [SerializeField] private float _meleeRange;
    [SerializeField] private float _minMeleeDistanceRange;
    private float _meleeRangeSquare;
    private float _minMeleeDistanceRangeSquare;
    private float _attackRangeSquare;
    private Animator _animator;
    public float MeleeRangeSqr { get { return _meleeRangeSquare; } }
    public float MinMeleeDistanceRangeSqr { get { return _minMeleeDistanceRangeSquare; } }
    public float AttackRangeSqr { get { return _attackRangeSquare; } }

    protected override void Start()
    {
        base.Start();

        _minMeleeDistanceRangeSquare = _minMeleeDistanceRange * _minMeleeDistanceRange;
        _attackRangeSquare = _attackRange * _attackRange;
        _meleeRangeSquare = _meleeRange * _meleeRange;

        _animator = GetComponent<Animator>();
    }

    private void StopMovement()
    {
        _navMeshAgent.isStopped = true;
        _navMeshAgent.velocity = Vector3.zero;
    }

    protected override void HandleMovement()
    {
        if (_target == null)
        {
            StopMovement();
            return;
        }
        if (_animator != null)
        {
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Attack"))
            {
                StopMovement();
                return;
            }
        }


        float sqrDistanceToTarget = (transform.position - _target.transform.position).sqrMagnitude - 1;
        if (sqrDistanceToTarget <= _meleeRangeSquare) // if player is within melee range stop (also stop if currently attacking)
        {
            StopMovement();
            return;
        }

        if (sqrDistanceToTarget <= _attackRangeSquare) // if player is within summoning range
        {
            if (sqrDistanceToTarget <= _minMeleeDistanceRangeSquare) // if player is within Melee distance
            {
                GoToPlayer();
                // Normal attack
            }
            else // otherwise stop and summon
                StopMovement();
        }

        GoToPlayer();

        
    }

    private void GoToPlayer()
    {
        // recalculate our path and go towards him
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
        Gizmos.DrawWireSphere(transform.position, _minMeleeDistanceRange);


        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _meleeRange);
    }
}
