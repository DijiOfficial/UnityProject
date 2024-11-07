using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarManager : MonoBehaviour
{
    private Image _healthBar;
    private Health _healthScript;
    private Transform _camera;
    private void Start()
    {
        // Get the Health script from the parent object
        _healthScript = GetComponentInParent<Health>();

        // Get the child named "Health" and its Image component
        Transform healthTransform = transform.Find("Health");
        if (healthTransform != null)
            _healthBar = healthTransform.GetComponent<Image>();

        // Find the GameObject named "Camera" and assign its transform to _camera
        GameObject cameraObject = GameObject.Find("Camera");
        if (cameraObject != null)
            _camera = cameraObject.transform;
    }
    private void Update()
    {
        if (_healthBar == null || _healthScript == null) return;

        _healthBar.fillAmount = _healthScript.CurrentHealth / _healthScript.StartHealth;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + _camera.forward);
    }
}
