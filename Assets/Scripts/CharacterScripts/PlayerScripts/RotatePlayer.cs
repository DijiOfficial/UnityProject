using UnityEngine;
using DG.Tweening;

public class RotatePlayer : MonoBehaviour
{
    public float SensX;
    public float SensY;
    public Transform _playerTransform;
    public Transform _armTranform;
    public Transform _minimapCameraTransform;

    private float _rotationX = 0;
    private float _rotationY = 0;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!_playerTransform || !_armTranform || !_minimapCameraTransform) return;

        float mouseX = Input.GetAxis("Mouse X") * SensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * SensY * Time.deltaTime;

        _rotationX -= mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -90, 90);

        _rotationY += mouseX;

        transform.rotation = Quaternion.Euler(_rotationX, _rotationY, 0);
        _playerTransform.rotation = Quaternion.Euler(0, _rotationY, 0);
        _armTranform.rotation = Quaternion.Euler(_rotationX, _rotationY, 0);
        _minimapCameraTransform.rotation = Quaternion.Euler(90, 0, -_rotationY);
    }

    public void DoFov(float value)
    {
        _camera.DOFieldOfView(value, 0.25f);
    }
}
