using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    //rename to player eyes
    public Transform _cameraTransform;
    private void Update()
    {
        if (!_cameraTransform) return;

        transform.position = _cameraTransform.position;
    }
}
