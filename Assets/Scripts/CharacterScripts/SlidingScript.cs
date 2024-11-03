using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SlidingScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _playerTransform;
    private Rigidbody _rigidbody;
    private MovementBehaviour _movementBehaviourScript;

    [Header("Sliding")]
    [SerializeField] private float _maxSlideTime;
    [SerializeField] private float _SlideCooldownTotal;
    [SerializeField] private float _slideForce;
    private float _slideTimer;

    private bool _canSlide;
    private float _slideCooldown;
    //private Vector3 _slideDirection;

    public bool CooldownOver { get { return _canSlide; } }
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _movementBehaviourScript = GetComponent<MovementBehaviour>();
    }
    private void FixedUpdate()
    {
        if (_movementBehaviourScript.IsSliding)
            SlidingMovement();

        if (!_canSlide)
        {
            _slideCooldown -= Time.deltaTime;
            if (_slideCooldown <= 0)
                _canSlide = true;
        }
    }

    public void StartSlide()
    {
        _movementBehaviourScript.IsSliding = true;
        //_slideDirection = _movementBehaviourScript.DesiredMovementDirection;
        _slideTimer = _maxSlideTime;
        _canSlide = false;
        _slideCooldown = _SlideCooldownTotal + _maxSlideTime;
    }

    private void SlidingMovement()
    {
        if (transform.localScale.y > _movementBehaviourScript.CrouchHeight)
        {
            float newScaleY = Mathf.Max(transform.localScale.y - _movementBehaviourScript.CrouchScale.y * 2, _movementBehaviourScript.CrouchHeight);
            transform.localScale = new(transform.localScale.x, newScaleY, transform.localScale.z);
        }
        else
        {
            transform.localScale = new(transform.localScale.x, _movementBehaviourScript.CrouchHeight, transform.localScale.z);
        }

        // _movementBehaviourScript.IsSliding normal
        if (!_movementBehaviourScript.IsOnSlope() || _rigidbody.velocity.y > -0.1f)
        {
            _rigidbody.AddForce(_movementBehaviourScript.DesiredMovementDirection.normalized * _slideForce, ForceMode.Force);

            _slideTimer -= Time.deltaTime;
        }

        // _movementBehaviourScript.IsSliding down a slope
        else
        {
            _rigidbody.AddForce(_movementBehaviourScript.GetSlopeMoveDirection() * _slideForce, ForceMode.Force);
        }

        if (_slideTimer <= 0)
            StopSlide();
    }

    private void StopSlide()
    {
        _movementBehaviourScript.IsSliding = false;
    }
}
