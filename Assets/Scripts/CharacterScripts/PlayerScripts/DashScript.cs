using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DashScript : MonoBehaviour
{
    [SerializeField] private UnityEvent _onDash;


    [Header("References")]
    private Transform _orientation;
    //public Transform _playerCam;
    private Rigidbody _rigidbody;
    private MovementBehaviour _movementBehaviourScript;

    [Header("Dashing")]
    [SerializeField] private float _dashForce;
    [SerializeField] private float _dashUpwardForce;
    private float _dashDuration = 0.08f;

    [Header("CameraEffects")]
    private RotatePlayer _cam;
    [SerializeField] private float _dashFov;

    [Header("Cooldown")]
    [SerializeField] private float _dashCd;
    private float _dashCdTimer;

    [Header("References")]
    [SerializeField] protected TempPlayerInfo _tempPlayerInfo;

    public delegate void DashChanfe(float start, float current);
    public event DashChanfe OnDashChange;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _movementBehaviourScript = GetComponent<MovementBehaviour>();
        _orientation = GetComponent<Transform>();

        // Find the camera GameObject and get the RotatePlayer script
        GameObject cameraGameObject = Camera.main.gameObject;
        _cam = cameraGameObject.GetComponent<RotatePlayer>();
        _cam.DoFov(70f);

        if (_tempPlayerInfo._quickDash || _tempPlayerInfo._blinkStep)
            _dashCd *= (_tempPlayerInfo._quickDash && _tempPlayerInfo._blinkStep) ? 0.5f : 0.75f;
    }


    private void Update()
    {
        if (_dashCdTimer > 0)
            _dashCdTimer -= Time.deltaTime;

        OnDashChange?.Invoke(_dashCd, _dashCdTimer);
    }

    public void Dash()
    {
        if (_dashCdTimer > 0) 
            return;
        else 
            _dashCdTimer = _dashCd;

        _movementBehaviourScript.IsDashing = true;

        _cam.DoFov(_dashFov);

        Vector3 direction = GetDirection(_orientation);

        Vector3 forceToApply = direction * _dashForce + _orientation.up * _dashUpwardForce;

        _rigidbody.useGravity = false;

        _delayedForceToApply = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.025f);

        Invoke(nameof(ResetDash), _dashDuration);

        _onDash?.Invoke();
    }

    private Vector3 _delayedForceToApply;
    private void DelayedDashForce()
    {
        _rigidbody.AddForce(_delayedForceToApply, ForceMode.Impulse);
    }

    private void ResetDash()
    {
        _movementBehaviourScript.IsDashing = false;

        _cam.DoFov(70f);

        _rigidbody.useGravity = true;
    }

    private Vector3 GetDirection(Transform forwardT)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = forwardT.forward * verticalInput + forwardT.right * horizontalInput;

        if (verticalInput == 0 && horizontalInput == 0)
            direction = forwardT.forward;

        return direction.normalized;
    }
}