using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    [SerializeField]
    protected float _movementSpeed = 1.0f;

    [SerializeField]
    protected float _jumpStrength = 10.0f;

    protected Rigidbody _rigidbody;

    protected Vector3 _desiredMovementDirection = Vector3.zero;
    protected GameObject _target;

    protected bool _isGrounded = false;
    protected const float GROUND_CHECK_DISTANCE = 0.2f;
    protected const string GROUND_STRING = "Ground";
    public Vector3 DesiredMovementDirection
    {
        get { return _desiredMovementDirection; }
        set { _desiredMovementDirection = value; }
    }
    public GameObject Target
    {
        get { return _target; }
        set { _target = value; }
    }
    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
    }
    protected virtual void FixedUpdate()
    {
        HandleMovement();

        _isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, GROUND_CHECK_DISTANCE, LayerMask.GetMask(GROUND_STRING));
    }
    protected virtual void HandleMovement()
    {
        if (_rigidbody == null) return;

        Vector3 movement = _movementSpeed * _desiredMovementDirection.normalized;

        movement.y = _rigidbody.velocity.y;
        _rigidbody.velocity = movement;
    }

    public void Jump()
    {
        if (_isGrounded)
            _rigidbody.AddForce(Vector3.up * _jumpStrength, ForceMode.Impulse);
    }
}
