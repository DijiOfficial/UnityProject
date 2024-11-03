using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrbBehaviour : MonoBehaviour
{

    [Header("Jumping")]
    [SerializeField] protected float _upWardStrength;
    private Rigidbody _rigidbody;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
        _rigidbody.AddForce(Vector3.up * _upWardStrength, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
