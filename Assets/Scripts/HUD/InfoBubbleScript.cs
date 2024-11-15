using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBubbleScript : MonoBehaviour
{
    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (_mainCamera != null)
        {
            transform.LookAt(_mainCamera.transform);
        }
    }
}