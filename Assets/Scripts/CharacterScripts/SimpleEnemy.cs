using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class SimpleEnemy : BasicCharacter
{
    private GameObject _playerTarget = null;
    [SerializeField] private float _attackRange = 5.0f;
    [SerializeField] private float _attackDelay = 0.1f;
    private float _attackWaitTime;
    private bool _canAttack = false;
    public float GetAttackRange { get { return _attackRange; } }
    private SkeletonAnimation _skeletonAnimation;
    protected override void Awake()
    {
        base.Awake();
        StaticVariablesManager.Instance.EnemyCount++;
    }
    void OnDestroy()
    {
        StaticVariablesManager.Instance.EnemyCount--;
    }
    private void Start()
    {
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();

        if (player) _playerTarget = player.gameObject;

        _skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    private void Update()
    {
        HandleMovement();
        HandleAttacking();

        if (_isHit)
        {
            _isHit = false;
            _skeletonAnimation.HandleHit();
        }
    }

    void HandleMovement()
    {
        if (_movementBehaviour == null)
            return;

        _movementBehaviour.Target = _playerTarget;
    }

    void HandleAttacking()
    {
        if (_attackBehaviour == null) return;
        if (_attackBehaviour.IsAttacking) return;
        if (_playerTarget == null) return;

        if ((transform.position - _playerTarget.transform.position).sqrMagnitude < _attackRange * _attackRange && !_canAttack)
        {
            _attackWaitTime = _attackDelay;
            _canAttack = true;
            _skeletonAnimation.HandleAttack();
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
}

