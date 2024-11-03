using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : BasicCharacter
{
    [SerializeField] private InputActionAsset _inputAsset;
    [SerializeField] private InputActionReference _movementReferenceX;
    [SerializeField] private InputActionReference _movementReferenceZ;
    [SerializeField] private Transform _playerTransform;

    private InputAction _jumpAction;
    private InputAction _attackAction;
    private InputAction _sprintAction;
    private InputAction _crouchAction;
    private bool _canSlide;
    protected SlidingScript _slidingBehaviour;
    protected override void Awake()
    {
        base.Awake();

        _slidingBehaviour = GetComponent<SlidingScript>();
      
        if (_inputAsset == null) return;

        //example of searching for the bindings in code, alternatively, they can be hooked in the editor using a InputAcctionReference as shown by _movementAction
        _jumpAction = _inputAsset.FindActionMap("Gameplay").FindAction("Jump");
        _attackAction = _inputAsset.FindActionMap("Gameplay").FindAction("Attack");
        _sprintAction = _inputAsset.FindActionMap("Gameplay").FindAction("Sprint");
        _crouchAction = _inputAsset.FindActionMap("Gameplay").FindAction("Crouch");

        //we bind a callback to it instead of continiously monitoring input
        _jumpAction.performed += HandleJumpInput;
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
            _movementBehaviour.Crouch(_crouchAction.IsPressed(), _playerTransform.forward);
        }

        if (!_canSlide && !_crouchAction.IsPressed() && _slidingBehaviour.CooldownOver) _canSlide = true;
    }
    private void HandleAttackInput()
    {
        if (_attackBehaviour == null
            || _attackAction == null)
            return;

        if (_attackAction.IsPressed())
            _attackBehaviour.Attack();
    }
}



