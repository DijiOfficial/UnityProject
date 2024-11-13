using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherEnemyScript : SimpleEnemy
{
    private Vector3 _directionLookat;
    protected override void HandleAttacking()
    {
        if (_attackBehaviour == null) return;
        if (_attackBehaviour.IsAttacking) return;
        if (_playerTarget == null) return;

        if ((transform.position - _playerTarget.transform.position).sqrMagnitude < _attackRange * _attackRange && !_canAttack)
        {
            // Rotate to look at the player
            Vector3 directionToPlayer = (_playerTarget.transform.position - transform.position).normalized;
            _directionLookat = directionToPlayer;
            transform.LookAt(transform.position + directionToPlayer);

            _attackWaitTime = _attackDelay;
            _canAttack = true;
            _skeletonAnimation.HandleArcherAttack();
        }

        if (_attackWaitTime < 0.0f && _canAttack)
        {
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