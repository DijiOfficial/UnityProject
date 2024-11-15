using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class SimpleEnemy : BasicCharacter
{
    protected GameObject _playerTarget = null;
    [SerializeField] protected float _attackRange = 5.0f;
    [SerializeField] protected float _attackDelay = 0.1f;
    protected float _attackWaitTime;
    protected bool _canAttack = false;
    protected BaseAnimationController _enemyAnimation;
    protected Vector3 _directionLookat;

    public float GetAttackRange { get { return _attackRange; } }
    public bool IsAttacking { get { return _attackBehaviour.IsAttacking; } }
    protected override void Awake()
    {
        base.Awake();
        StaticVariablesManager.Instance.IncreaseEnemyCount();
    }
    protected virtual void OnDestroy()
    {
        if (!StaticVariablesManager.Exists) return;
        StaticVariablesManager.Instance.DecreaseEnemyCount();
    }
    protected virtual void Start()
    {
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();

        if (player) _playerTarget = player.gameObject;

        _enemyAnimation = GetComponent<BaseAnimationController>();
    }

    protected virtual void Update()
    {
        HandleMovement();
        HandleAttacking();

        if (_isHit)
        {
            _isHit = false;
            _enemyAnimation.HandleHit();
        }
    }

    protected virtual void HandleMovement()
    {
        if (_movementBehaviour == null)
            return;

        _movementBehaviour.Target = _playerTarget;
    }

    protected virtual void HandleAttacking()
    {
        if (_attackBehaviour == null) return;
        if (_attackBehaviour.IsAttacking) return;
        if (_playerTarget == null) return;

        if ((transform.position - _playerTarget.transform.position).sqrMagnitude < _attackRange * _attackRange && !_canAttack)
        {
            _attackWaitTime = _attackDelay;
            _canAttack = true;
            _enemyAnimation.HandleAttack();
            //if (_attackVFXTemplate)
            //    Instantiate(_attackVFXTemplate, transform.position, transform.rotation);
        }

        if (_attackWaitTime < 0.0f && _canAttack)
        {
            _attackBehaviour.Attack();
            _canAttack = false;
        }
        else
            _attackWaitTime -= Time.deltaTime;
    }

    protected void LookAtPlayer()
    {
        // Rotate to look at the player
        Vector3 directionToPlayer = (_playerTarget.transform.position - transform.position).normalized;
        _directionLookat = directionToPlayer;
        transform.LookAt(transform.position + directionToPlayer);
    }
}

