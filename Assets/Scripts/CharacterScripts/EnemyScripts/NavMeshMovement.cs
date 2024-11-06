using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshMovementBehaviour : MovementBehaviour
{
    protected NavMeshAgent _navMeshAgent;

    protected Vector3 _previousTargetPosition = Vector3.zero;
    protected float _attackRange;

    protected override void Awake()
    {
        base.Awake();

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _walkSpeed;

        _previousTargetPosition = transform.position;
    }
    protected override void Start()
    {
        _attackRange = GetComponent<SimpleEnemy>().GetAttackRange;
    }

    protected const float MOVEMENT_EPSILON = .25f;
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
            _navMeshAgent.SetDestination(_target.transform.position);
            _navMeshAgent.isStopped = false;
            _previousTargetPosition = _target.transform.position;
        }
    }
    protected override void Update() { }
    
    protected override void FixedUpdate()
    {
        HandleMovement();
    }
}
