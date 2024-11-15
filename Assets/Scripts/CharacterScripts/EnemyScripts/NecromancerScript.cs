using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NecromancerScript : SimpleEnemy
{
    [SerializeField] private List<GameObject> _summonPrefabs = new List<GameObject>();

    [SerializeField] private float _summonCooldown;
    private float _summonCooldownTimer;
    private NecromancerMovement _movementRef;
    private bool _isSummoning = false;
    private bool _canSummon = false;
    private NecromancerAnimation _animation;
    protected override void Awake()
    {
        base.Awake();
        _movementRef = GetComponent<NecromancerMovement>();
        _animation = GetComponent<NecromancerAnimation>();
    }
    protected override void HandleAttacking()
    {
        if (_attackBehaviour == null) return;
        if (_attackBehaviour.IsAttacking) return;
        if (_playerTarget == null) return;
        LookAtPlayer();

        bool isSummoning = (_isSummoning || _summonCooldownTimer > 0.0f);

        float sqrDistanceToTarget = (transform.position - _playerTarget.transform.position).sqrMagnitude - 1;
        if (sqrDistanceToTarget <= (_movementRef.MeleeRangeSqr * 1.4f) && !_canAttack) // if player is within attack range (extending it a bit so can attack even if not in range ebacuse range is smaller than actual attack and mostly for movement)
        {
            _attackWaitTime = _attackDelay;
            _canAttack = true;
            _animation.HandleAttack();
        }
        else if (sqrDistanceToTarget <= _movementRef.AttackRangeSqr && !_canSummon && !isSummoning && sqrDistanceToTarget > _movementRef.MinMeleeDistanceRangeSqr)
        {//stop summon if player is within melee range or summoning
            _attackWaitTime = _attackDelay;
            _canSummon = true;
            _animation.HandleSummoning();
        }

        if (_attackWaitTime < 0.0f)
        {
            if (_canAttack)
            {
                _attackBehaviour.Attack();
                _canAttack = false;

            }
            else if(_canSummon && !_isSummoning)
            {
                _canSummon = false;
                _isSummoning = true;
            }
        }
        else
            _attackWaitTime -= Time.deltaTime;

        //handle the countdown of the summon timer
        if (_summonCooldownTimer > 0.0f)
            _summonCooldownTimer -= Time.deltaTime;

        if (_summonCooldownTimer <= 0.0f && _isSummoning)
            Summon();

        _isSummoning = false;
    }

    private void Summon()
    {
        Vector3 randomSpawnPosition;
        float distanceToSummonerSqr;
        float maxDistanceSqr = 100f; // 10 meters squared
        float spawnRadius = 10f;
        float groundCheckDistance = 1f;
        LayerMask groundLayer = LayerMask.GetMask("Ground");
        LayerMask staticLevelLayer = LayerMask.GetMask("StaticLevel");

        do
        {
            Vector2 randomPoint = Random.insideUnitCircle * spawnRadius; // Generate a random point within a 10-meter radius
            randomSpawnPosition = new Vector3(transform.position.x + randomPoint.x, transform.position.y, transform.position.z + randomPoint.y);
            distanceToSummonerSqr = (transform.position - randomSpawnPosition).sqrMagnitude;

            // Check if the position is not colliding with static level objects
            bool isColliding = Physics.CheckSphere(randomSpawnPosition, 0.5f, staticLevelLayer);

            // Check if there is ground under the position
            bool hasGround = Physics.Raycast(randomSpawnPosition, Vector3.down, groundCheckDistance, groundLayer);

            if (!isColliding && hasGround)
            {
                break;
            }
        }
        while (distanceToSummonerSqr > maxDistanceSqr);

        // Select a random prefab from the list
        int randomIndex = Random.Range(0, _summonPrefabs.Count);

        Instantiate(_summonPrefabs[randomIndex], randomSpawnPosition, Quaternion.identity);
        _summonCooldownTimer = _summonCooldown;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + _directionLookat * 50.0f);
    }
}
