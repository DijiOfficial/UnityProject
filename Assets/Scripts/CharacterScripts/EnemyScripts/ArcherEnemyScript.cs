using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherEnemyScript : SimpleEnemy
{
    private SkeletonAnimation _skeletonAnimation;
    protected override void Awake()
    {
        base.Awake();
        _skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    protected override void HandleAttacking()
    {
        if (_attackBehaviour == null) return;
        if (_attackBehaviour.IsAttacking) return;
        if (_playerTarget == null) return;

        if ((transform.position - _playerTarget.transform.position).sqrMagnitude < _attackRange * _attackRange && !_canAttack)
        {
            LookAtPlayer();

            _attackWaitTime = _attackDelay;
            _canAttack = true;
            _skeletonAnimation.HandleArcherAttack();
        }

        if (_attackWaitTime < 0.0f && _canAttack)
        {
            LookAtPlayer();
            _attackBehaviour.Attack();
            _canAttack = false;
        }
        else
            _attackWaitTime -= Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + _directionLookat *50.0f);
    }
}