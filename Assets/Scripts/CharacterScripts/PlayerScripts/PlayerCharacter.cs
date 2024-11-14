using System.ComponentModel;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;
using static MovementBehaviour;

public class PlayerCharacter : BasicCharacter
{
    [SerializeField] private InputActionAsset _inputAsset;
    [SerializeField] private InputActionReference _movementReferenceX;
    [SerializeField] private InputActionReference _movementReferenceZ;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private int _totalRangedAttack;
    [SerializeField] private TempPlayerInfo _tempPlayerInfo;

    public delegate void RangeAttackChange(int current);
    public event RangeAttackChange OnRangedAttackChange;
    public int TotalRangedAttack { get { return _totalRangedAttack; } }
    private int _availableAttacks = 2;
    private float _rechargeRate = 2.0f;
    private float _rechargeTime;
    private bool _isAttacking = false;
    private InputAction _jumpAction;
    private InputAction _attackAction;
    private InputAction _secondAttackAction;
    private InputAction _sprintAction;
    private InputAction _crouchAction;
    private InputAction _dashAction;
    private InputAction _specialPower;
    private bool _canSlide;
    private SlidingScript _slidingBehaviour;
    private DashScript _dash;
    private SpecialPowerScript _powerScript;
    protected override void Awake()
    {
        _totalRangedAttack += _tempPlayerInfo._secondarySkill;
        _availableAttacks = _totalRangedAttack;

        base.Awake();

        _slidingBehaviour = GetComponent<SlidingScript>();
        _dash = GetComponent<DashScript>();
        _powerScript = GetComponent<SpecialPowerScript>();

        if (_inputAsset == null) return;

        //example of searching for the bindings in code, alternatively, they can be hooked in the editor using a InputAcctionReference as shown by _movementAction
        _jumpAction = _inputAsset.FindActionMap("Gameplay").FindAction("Jump");
        _attackAction = _inputAsset.FindActionMap("Gameplay").FindAction("Attack");
        _secondAttackAction = _inputAsset.FindActionMap("Gameplay").FindAction("SecondAttack");
        _sprintAction = _inputAsset.FindActionMap("Gameplay").FindAction("Sprint");
        _crouchAction = _inputAsset.FindActionMap("Gameplay").FindAction("Crouch");
        _dashAction = _inputAsset.FindActionMap("Gameplay").FindAction("Dash");
        _specialPower = _inputAsset.FindActionMap("Gameplay").FindAction("SpecialPower");

        //we bind a callback to it instead of continiously monitoring input
        _jumpAction.performed += HandleJumpInput;
    }

    protected void Start()
    {
        //this was in awake, turns out the script are loaded in order of hierarchy on which they are attached to the object
        GetComponent<Health>().CurrentHealth = _tempPlayerInfo._health;
        GetComponent<Health>().CallHealthChange();

    }
    protected void OnDestroy()
    {
        _jumpAction.performed -= HandleJumpInput;
    }
    private void OnEnable()
    {
        if (_inputAsset == null) return;

        _inputAsset.Enable();
    }
    private void OnDisable()
    {
        if (_inputAsset == null) return;

        _inputAsset.Disable();
    }
    private void Update()
    {
        HandleMovementInput();
        HandleAttackInput();
        HandleSprint();
        HandleCrouch();
        HandledDash();
        HandleSprint();
        HandelSpecial();
    }
    void HandleMovementInput()
    {
        if (_movementBehaviour.IsSliding && !_movementBehaviour.IsOnSlope()) return;

        if (_movementBehaviour == null || _movementReferenceX == null || _movementReferenceZ == null)
            return;

        //movement
        float movementInputX = _movementReferenceX.action.ReadValue<float>();
        float movementInputZ = _movementReferenceZ.action.ReadValue<float>();

        Vector3 movement = movementInputX * _playerTransform.right + movementInputZ * _playerTransform.forward;
        
        _movementBehaviour.DesiredMovementDirection = movement;
    }
    private void HandleJumpInput(InputAction.CallbackContext context)
    {
        _movementBehaviour.Jump();
    }

    private void HandelSpecial()
    {
        if (_specialPower == null) return;

        if (_specialPower.triggered)
            _powerScript.Activate();
    }

    private void HandledDash()
    {
        if (_dashAction == null) return;

        if (_dashAction.triggered)
            _dash.Dash();
    }
    private void HandleSprint()
    {
        _movementBehaviour.Sprint(_sprintAction.IsPressed());
    }
    private void HandleCrouch()
    {
        float movementInputX = _movementReferenceX.action.ReadValue<float>();
        float movementInputZ = _movementReferenceZ.action.ReadValue<float>();

        bool isNotMoving = Mathf.Approximately(movementInputX, 0.0f) && Mathf.Approximately(movementInputZ, 0.0f);

        if (!isNotMoving && _crouchAction.IsPressed() && _canSlide && _movementBehaviour.IsGrounded)
        {
            _slidingBehaviour.StartSlide();
            _canSlide = false;
        }
        else if(!_movementBehaviour.IsSliding || !_movementBehaviour.IsGrounded)
        {
            _movementBehaviour.Crouch(_crouchAction.IsPressed());
        }

        if (!_canSlide && !_crouchAction.IsPressed() && _slidingBehaviour.CooldownOver) _canSlide = true;
    }
    private void HandleAttackInput()
    {
        if (_attackBehaviour == null
            || _attackAction == null
            || _secondAttackAction == null)
            return;

        if (_attackAction.IsPressed())
            _attackBehaviour.Attack();

        if (_secondAttackAction.IsPressed() && _availableAttacks > 0 && !_isAttacking)
        {
            _attackBehaviour.SecondAttack();
            _availableAttacks -= 1;
            if (_rechargeTime <= 0.0f)
                _rechargeTime = _rechargeRate;

            OnRangedAttackChange?.Invoke(_availableAttacks);
            _isAttacking = true;
        }
        else if (_isAttacking && !_secondAttackAction.IsPressed())
            _isAttacking = false;

        if (_availableAttacks < _totalRangedAttack)
        {
            if (_rechargeTime > 0.0f)
                _rechargeTime -= Time.deltaTime;

            if (_rechargeTime <= 0.0f)
            {
                _availableAttacks++;
                _rechargeTime = _rechargeRate;
                OnRangedAttackChange?.Invoke(_availableAttacks);
            }
        }
    }
}



