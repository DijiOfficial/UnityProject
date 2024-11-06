using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveKing : SimpleEnemy
{
    private ComplexAttackBehaviour _complexAttackBehaviour;

    private VampireAnimation _vampireAnimation;
    private ComplexAttackBehaviour.AttackState _attackState = ComplexAttackBehaviour.AttackState.None;
    protected override void Start()
    {
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();
        _complexAttackBehaviour = GetComponent<ComplexAttackBehaviour>();

        if (player) _playerTarget = player.gameObject;

        _vampireAnimation = GetComponent<VampireAnimation>();
    }

    protected override void Update()
    {
        HandleMovement();
        HandleAttacking();
    }

    protected override void HandleMovement()
    {
        if (_movementBehaviour == null)
            return;

        _movementBehaviour.Target = _playerTarget;
    }

    protected override void HandleAttacking()
    {
        if (_complexAttackBehaviour == null) return;
        if (_complexAttackBehaviour.IsAttacking) return;
        if (_playerTarget == null) return;

        if (!_canAttack)
        {
            if ((transform.position - _playerTarget.transform.position).sqrMagnitude < _attackRange * _attackRange)
            {
                // Determine if a grab attack should be performed (10% chance)
                bool isGrabAttack = Random.Range(0, 100) < 10;

                _vampireAnimation.HandleAttackClose(isGrabAttack);

                _attackState = isGrabAttack ? ComplexAttackBehaviour.AttackState.Grab : ComplexAttackBehaviour.AttackState.CloseAttack;
            }
            else if (!_canAttack)
            {
                _vampireAnimation.HandleAttackRanged();
                _attackState = ComplexAttackBehaviour.AttackState.FarAttack;
            }
            
            _attackWaitTime = _attackDelay;
            _canAttack = true;
        }

        if (_attackWaitTime < 0.0f && _canAttack)
        {
            _complexAttackBehaviour.Attack(_attackState);
            _canAttack = false;
        }
        else
            _attackWaitTime -= Time.deltaTime;
    }
}
