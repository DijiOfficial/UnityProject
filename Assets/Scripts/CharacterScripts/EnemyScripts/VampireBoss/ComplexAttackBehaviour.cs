using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ComplexAttackBehaviour;
using static UnityEngine.UI.Image;

public class ComplexAttackBehaviour : MonoBehaviour
{
    [Header("Attack Variables")]
    [SerializeField] private List<GameObject> _attack = new List<GameObject>();
    //[SerializeField] private GameObject _grabSocket = null;
    [SerializeField] private Transform _AttackSocket = null;
    const string ATTACK_METHOD = "SpawnAttackHitBox";
    //may add multile attack oscket for ranged later
    //[SerializeField] private List<Transform> _AttackSockets = new List<Transform>();

    [SerializeField] private float _fireRate = 1.0f;
    private bool _isAttacking = false;
    private float _attackTimer = 0.0f;

    public bool IsAttacking { get { return (_isAttacking || _attackTimer > 0.0f); } }


    public enum AttackState { Grab, CloseAttack, FarAttack, None }
    private AttackState _attackState = AttackState.None;

    public void Attack(AttackState attackState)
    {
        _isAttacking = true;
        _attackState = attackState;

        if (_attack == null)
            return;
    }


    private void Update()
    {
        //handle the countdown of the fire timer
        if (_attackTimer > 0.0f)
            _attackTimer -= Time.deltaTime;

        if (_attackTimer <= 0.0f && _isAttacking)
        {
            switch (_attackState)
            {
                case AttackState.Grab:
                    SpawnAttackHitBox();
                    break;
                case AttackState.CloseAttack:
                    Invoke(ATTACK_METHOD, 0.65f);
                    break;
                case AttackState.FarAttack:
                    Invoke(ATTACK_METHOD, 0.65f);
                    break;
                case AttackState.None:
                    break;
            }

            //set the time so we respect the firerate
            _attackTimer += 1.0f / _fireRate;
        }

        _isAttacking = false;
    }

    private void SpawnAttackHitBox()
    {
        //no bullet to fire
        if (_attack == null)
            return;

        int idx = (int)_attackState;
        Instantiate(_attack[idx], _AttackSocket.position, _AttackSocket.rotation);

        //_onFireEvent?.Invoke();
    }
}




