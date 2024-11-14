using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
//using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class MovementBehaviour : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] protected float _walkSpeed;
    [SerializeField] protected float _sprintSpeed;
    [SerializeField] protected float _groundDrag;
    protected float _movementSpeed;
    private bool _isSprinting = false;
    
    protected Vector3 _desiredMovementDirection = Vector3.zero;
    protected GameObject _target;

    [Header("Jumping")]
    [SerializeField] protected float _jumpStrength;
    [SerializeField] protected float _airMultiplier;
    [SerializeField] private float _jumpButtonMaxBuffer;
    private float _jumpButtonBuffer;
    private bool _isJumping = false;
    private float _jumpTimer;

    [Header("Crouching")]
    [SerializeField] protected float _crouchHeight;
    private float _startYScale;
    private bool _isCrouching = false;
    private Vector3 _crouchScale = new(0, 0.05f, 0);
    private bool _isSliding;
    private SlidingScript _slidingScript;

    [Header("Collision Detection")]
    private bool _isGrounded = false;
    protected const float GROUND_CHECK_DISTANCE = 0.4f;
    protected const string GROUND_STRING = "Ground";
    protected Rigidbody _rigidbody;

    [Header("Slope Handling")]
    [SerializeField] protected float _maxSlopeAngle;
    private RaycastHit _slopeHit;
    private bool _isJumpingOffSlope;

    [Header("Movement State")]
    private MovementState _movementState;
    private enum MovementState { Walking, Running, Air, Crouching, Sliding }


    [Header("References")]
    [SerializeField] protected TempPlayerInfo _tempPlayerInfo;

    #region Setters and Getters
    public bool IsGrounded { get { return _isGrounded; } }
    public float CrouchHeight { get { return _crouchHeight; } }
    public Vector3 CrouchScale { get { return _crouchScale; } }
    public bool IsSliding
    {
        get { return _isSliding; }
        set { _isSliding = value; }
    }
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
    #endregion

    #region Debug
    public float Speed
    {
        get { return speed; }
        //set { speed = value; }
    }
    public float speed;
    protected virtual void Start()
    {
        StartCoroutine(CalcSpeed());
    }
    public delegate void SpeedChange(float speed);
    public event SpeedChange OnSpeedChange;
    IEnumerator CalcSpeed()
    {
        while (true)
        {
            Vector3 lastPosition = transform.position;
            yield return new WaitForFixedUpdate();
            Vector3 flatVelocity = new(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
            speed = flatVelocity.magnitude;
            OnSpeedChange?.Invoke(speed);
        }
    }
    #endregion



    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _slidingScript = GetComponent<SlidingScript>();

        if (_rigidbody != null) 
            _rigidbody.freezeRotation = true;

        _startYScale = transform.localScale.y;

        // Check if this is the Player
        if (gameObject.name == "Player")
        {
            // Add to sprint speed based on swiftStride
            float additionalSprintSpeed = _tempPlayerInfo._swiftStride * _sprintSpeed * 0.05f;
            _sprintSpeed += additionalSprintSpeed;
        }
    }

    protected virtual void FixedUpdate()
    {
        HandleMovement();
        HandleCrouch();
        bool isInAir = !_isGrounded;
        _isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.2f, Vector3.down, GROUND_CHECK_DISTANCE, LayerMask.GetMask(GROUND_STRING));

        //just landed
        if (isInAir && _isGrounded)
            _rigidbody.velocity = new Vector3(0, 0, 0);

        if (_isJumping)
        {
            _jumpTimer += Time.deltaTime;
            if (_jumpTimer > 0.1f)
                _isJumping = false;
        }

        if (_jumpButtonBuffer > 0)
        {
            _jumpButtonBuffer -= Time.deltaTime;
            if (_isGrounded)
            {
                _jumpButtonBuffer = 0;
                _rigidbody.drag = _groundDrag;
                Jump();
            }
        }

    }
    protected virtual void Update()
    {
        SpeedControl();
        StateHandle();

        if (_isGrounded)
        {
            _rigidbody.drag = _groundDrag;
            if (_isJumpingOffSlope && !_isJumping)
            {
                _isJumpingOffSlope = false;
                _rigidbody.velocity = new Vector3(0, 0, 0);
            }
        }
        else
            _rigidbody.drag = 0.0f;
    }
    protected virtual void StateHandle()
    {
        //replace with StateMachine
        if (_isSliding)
        {
            _movementSpeed = _walkSpeed;
            _movementState = MovementState.Sliding;
        }
        else if (_isCrouching && _isGrounded)
        {
            _movementState = MovementState.Crouching;
            _movementSpeed = _walkSpeed;
        }
        else if (_isGrounded && _isSprinting && _slidingScript.CooldownOver)
        {
            _movementState = MovementState.Running;
            _movementSpeed = _sprintSpeed;
        }
        else if (_isGrounded)
        {
            _movementState = MovementState.Walking;
            _movementSpeed = _walkSpeed;
        }
        else
        {
            _movementState = MovementState.Air;
            if (_isSprinting || !_slidingScript.CooldownOver)
                _movementSpeed = _sprintSpeed;
            else
                _movementSpeed = _walkSpeed;
        }

        _movementSpeed += _tempPlayerInfo._blazingAgility * _movementSpeed * 0.14f;
    }
    protected virtual void HandleMovement()
    {
        if (_rigidbody == null) return;

        int speedMultiplier = 80;

        if (IsOnSlope() && !_isJumpingOffSlope)
        {
            _rigidbody.AddForce(_movementSpeed * speedMultiplier * GetSlopeMoveDirection(), ForceMode.Force);
            if (_rigidbody.velocity.y > 0)
                _rigidbody.AddForce(Vector3.down * 80.0f, ForceMode.Force);
        }
        else
        {
            Vector3 movement = _movementSpeed * speedMultiplier * _desiredMovementDirection.normalized;

            if(_isGrounded)
                _rigidbody.AddForce(movement, ForceMode.Force);
            else if (!_isGrounded)
                _rigidbody.AddForce(_airMultiplier * movement, ForceMode.Force);
        }

        _rigidbody.useGravity = !IsOnSlope();
    }
    
    private void SpeedControl()
    {
        if (IsOnSlope() && !_isJumpingOffSlope)
        {
            if (_rigidbody.velocity.magnitude > _movementSpeed)
                _rigidbody.velocity = _rigidbody.velocity.normalized * _movementSpeed;
        }
        else
        {
            Vector3 flatVelocity = new(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);

            if (flatVelocity.magnitude > _movementSpeed)
            {
                Vector3 maxVelocity = flatVelocity.normalized * _movementSpeed;
                _rigidbody.velocity = new(maxVelocity.x, _rigidbody.velocity.y, maxVelocity.z);
            }
        }
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _isSliding = false;
            _isJumping = true;
            _jumpTimer = 0.0f;
            _isJumpingOffSlope = true;

            _rigidbody.velocity = new(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
            if (_isCrouching)
                _rigidbody.AddForce(_jumpStrength * 0.5f * Vector3.up, ForceMode.Impulse);
            else
                _rigidbody.AddForce(Vector3.up * _jumpStrength, ForceMode.Impulse);

            _jumpButtonBuffer = 0;
        }
        else
            _jumpButtonBuffer = _jumpButtonMaxBuffer;
    }
    public void Sprint(bool sprint)
    {
        _isSprinting = sprint;
    }

    private void HandleCrouch()
    {
        if (_isCrouching && !_isSliding)
        {
            if (transform.localScale.y > _crouchHeight)
                transform.localScale -= _crouchScale;
            else
                transform.localScale = new Vector3(transform.localScale.x, _crouchHeight, transform.localScale.z);
        }
        else if (!_isCrouching)
        {
            if (transform.localScale.y < _startYScale)
                transform.localScale += _crouchScale;
            else
                transform.localScale = new Vector3(transform.localScale.x, _startYScale, transform.localScale.z);
        }
    }
    public void Crouch(bool crouch)
    {
        _isCrouching = crouch;
    }

    public bool IsOnSlope()
    {
        if (Physics.Raycast(transform.position + Vector3.up * 0.2f, Vector3.down, out _slopeHit, GROUND_CHECK_DISTANCE))
        {
            float slopeAngle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            return slopeAngle < _maxSlopeAngle && slopeAngle != 0;
        }
        return false;
    }

    public Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(_desiredMovementDirection, _slopeHit.normal).normalized;
    }
}
