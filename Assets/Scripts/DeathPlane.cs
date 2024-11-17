using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Destroy the object that collides with the DeathPlane
        Destroy(other.gameObject);
    }
}
