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


    private GameObject _weapon = null;
    private bool _isAttacking = false;
    private float _attackTimer = 0.0f;
    public bool IsAttacking { get { return (_isAttacking || _attackTimer > 0.0f); } }

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
        _weapon.GetComponent<Animator>().Play("sword slash");
        yield return new WaitForSeconds(0.367f);
        _weapon.GetComponent<Animator>().Play("New State");
    }
}



