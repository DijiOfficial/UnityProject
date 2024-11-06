using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCharacter : MonoBehaviour
{
    protected AttackBehaviour _attackBehaviour;
    protected MovementBehaviour _movementBehaviour;
    protected bool _isHit;
    public bool IsHit
    {
        get { return _isHit; }
        set { _isHit = value; }
    }
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        _attackBehaviour = GetComponent<AttackBehaviour>();
        _movementBehaviour = GetComponent<MovementBehaviour>();
    }
}

