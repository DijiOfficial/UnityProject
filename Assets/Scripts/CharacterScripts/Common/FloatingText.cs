using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float _lifetime = 1f;
    private Transform _cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        // Find the main camera
        _cameraTransform = Camera.main.transform;

        // Destroy the GameObject after the specified lifetime
        Destroy(gameObject, _lifetime);
    }

    // LateUpdate is called once per frame after all Update functions have been called
    void LateUpdate()
    {
        // Make the text face the camera
        transform.LookAt(transform.position + _cameraTransform.forward);
    }
}