using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimation : MonoBehaviour
{

    private Animator _animator;
    //private List<string> _animations;
    //private Quaternion _initRotation;
    private Vector3 _previousPosition;
    const string IS_MOVING_PARAMETER = "IsWalking";
    private const float EPSILON = 0.0001f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        //_initRotation = transform.rotation;


        //_animations = new List<string>()
        //{
        //    "Hit1",
        //    "Fall1",
        //    "Attack1h1",
        //    "Walk 1",
        //};
        //_animator.SetTrigger("Walk 1");
    }

    void Awake()
    {
        _previousPosition = transform.root.position;

        _animator = transform.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        HandleMovementAnimation();
    }

    void HandleMovementAnimation()
    {
        if (_animator == null)
            return;

        _animator.SetBool(IS_MOVING_PARAMETER, (transform.root.position - _previousPosition).sqrMagnitude > EPSILON);

        _previousPosition = transform.root.position;
    }

    public void HandleAttack()
    {
        if (_animator == null) return;

        _animator.SetTrigger("Attack1h1");
    }

    public void HandleHit()
    {
        if (_animator == null) return;

        // Get the current animator state info
        AnimatorStateInfo currentState = _animator.GetCurrentAnimatorStateInfo(0);

        // Check if the current animation is "Attack1h1"
        if (currentState.IsName("Attack1h1")) return;

        // Set the trigger for the "Hit1" animation
        _animator.SetTrigger("Hit1");
    }
}
