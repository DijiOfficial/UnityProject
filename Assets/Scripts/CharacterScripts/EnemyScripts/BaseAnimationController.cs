using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAnimationController : MonoBehaviour
{
    protected Animator _animator;
    protected Vector3 _previousPosition;
    protected const string IS_MOVING_PARAMETER = "IsWalking";
    protected const float EPSILON = 0.0001f;
    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
    }

    protected virtual void Awake()
    {
        _previousPosition = transform.root.position;

        _animator = transform.GetComponent<Animator>();
    }
    protected virtual void FixedUpdate()
    {
        HandleMovementAnimation();
    }

    protected virtual void HandleMovementAnimation()
    {
        if (_animator == null)
            return;

        _animator.SetBool(IS_MOVING_PARAMETER, (transform.root.position - _previousPosition).sqrMagnitude > EPSILON);

        _previousPosition = transform.root.position;
    }
}
