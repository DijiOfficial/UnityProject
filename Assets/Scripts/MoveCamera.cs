using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform _cameraPosition;
    private void Update()
    {
        if (!_cameraPosition) return;

        transform.position = _cameraPosition.position;
    }
}
