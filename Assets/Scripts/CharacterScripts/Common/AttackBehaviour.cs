using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _hitbox = null;
    [SerializeField] private GameObject _weaponTemplate = null;
    [SerializeField] private GameObject _weaponSocket = null;
    [SerializeField] private List<Transform> _AttackSockets = new List<Transform>();
    [SerializeField] private float _fireRate = 1.0f;

    [Header("Second Attack")]
    [SerializeField] private GameObject _secondHitbox = null;

    [Header("References")]
    [SerializeField] protected TempPlayerInfo _tempPlayerInfo;

    private GameObject _weapon = null;
    private bool _isAttacking = false;
    private float _attackTimer = 0.0f;
    public bool IsAttacking { get { return (_isAttacking || _attackTimer > 0.0f); } }
    private bool _isPlayer = false;

    private void Awake()
    {
        //spawn guns
        if (_weaponTemplate != null && _weaponSocket != null)
        {
            _weapon = Instantiate(_weaponTemplate,
                _weaponSocket.transform, true);
            _weapon.transform.localPosition = Vector3.zero;
            _weapon.transform.localRotation = Quaternion.identity;
        }
        _isPlayer = GetComponent<PlayerCharacter>() != null;
        if (_isPlayer)
            _fireRate += _fireRate * 0.15f * _tempPlayerInfo._hasteOfTheWarrior;
    }
    private void Update()
    {
        //handle the countdown of the fire timer
        if (_attackTimer > 0.0f)
            _attackTimer -= Time.deltaTime;

        if (_attackTimer <= 0.0f && _isAttacking)
            SpawnAttackHitBox();

        _isAttacking = false;
    }

    private void SpawnAttackHitBox()
    {
        //no bullet to fire
        if (_hitbox == null)
            return;

        for (int i = 0; i < _AttackSockets.Count; i++)
        {
            Instantiate(_hitbox, _AttackSockets[i].position, _AttackSockets[i].rotation);
        }

        //set the time so we respect the firerate
        _attackTimer += 1.0f / _fireRate;

        //_onFireEvent?.Invoke();
        if (_weapon != null)
            StartCoroutine(SwordSwing());

    }

    private void SpawnSecondAttackHitBox()
    {
        //no bullet to fire
        if (_secondHitbox == null)
            return;

        for (int i = 0; i < _AttackSockets.Count; i++)
        {
            Instantiate(_secondHitbox, _AttackSockets[i].position + new Vector3(0, 0.5f, 0), _AttackSockets[i].rotation);
        }
    }

    public void Attack()
    {
        _isAttacking = true;
    }
    public void SecondAttack()
    {
        SpawnSecondAttackHitBox();
    }
    IEnumerator SwordSwing()
    {
        Animator animator = _weapon.GetComponent<Animator>();
        float animationDuration = 1.0f; // The original duration of the animation in seconds
        float adjustedSpeed = animationDuration * _fireRate; // Adjust the speed based on the fire rate

        animator.speed = adjustedSpeed;
        animator.Play("sword slash");
        //there is no animator, only one swing so yield for the duration of the animation - Epsilon so the animation can finish before getting called again
        float waitTime = Mathf.Max(1 / _fireRate - 0.01f, 0.001f);
        yield return new WaitForSeconds(waitTime);
        animator.Play("New State");
    }


}



