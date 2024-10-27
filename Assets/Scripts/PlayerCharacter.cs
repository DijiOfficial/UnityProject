using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : BasicCharacter
{
    [SerializeField]
    private InputActionAsset _inputAsset;

    [SerializeField]
    private InputActionReference _movementReferenceX;
    
    [SerializeField]
    private InputActionReference _movementReferenceZ;

    [SerializeField]
    private Transform _orientation;

    private InputAction _jumpAction;
    private InputAction _attackAction;

    protected override void Awake()
    {
        base.Awake();

        if (_inputAsset == null) return;

        //example of searching for the bindings in code, alternatively, they can be hooked in the editor using a InputAcctionReference as shown by _movementAction
        _jumpAction = _inputAsset.FindActionMap("Gameplay").FindAction("Jump");
        _attackAction = _inputAsset.FindActionMap("Gameplay").FindAction("Attack");

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
    }
    void HandleMovementInput()
    {
        if (_movementBehaviour == null || _movementReferenceX == null || _movementReferenceZ == null)
            return;

        //movement
        float movementInputX = _movementReferenceX.action.ReadValue<float>();
        float movementInputZ = _movementReferenceZ.action.ReadValue<float>();

        Vector3 movement = movementInputX * _orientation.right + movementInputZ * _orientation.forward;
        
        _movementBehaviour.DesiredMovementDirection = movement;

        //if (movement != Vector3.zero)
        //    transform.forward = movement;
    }
    private void HandleJumpInput(InputAction.CallbackContext context)
    {
        _movementBehaviour.Jump();
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



