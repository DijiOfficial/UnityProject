using UnityEngine;
using Cinemachine;

public class RotatePlayer : MonoBehaviour
{
    public float SensX;
    public float SensY;
    public Transform _orientation;
    public Transform _armOrientation;

    private float _rotationX = 0;
    private float _rotationY = 0;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * SensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * SensY * Time.deltaTime;

        _rotationX -= mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -90, 90);

        _rotationY += mouseX;

        transform.rotation = Quaternion.Euler(_rotationX, _rotationY, 0);
        _orientation.rotation = Quaternion.Euler(0, _rotationY, 0);
        _armOrientation.rotation = Quaternion.Euler(_rotationX, _rotationY, 0);
    }

    
}
