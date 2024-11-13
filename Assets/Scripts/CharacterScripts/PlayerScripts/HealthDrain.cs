using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthDrain : MonoBehaviour
{
    [SerializeField] private float _drainRate = 20.0f;
    [SerializeField] private int _drainingValue = 1;
    private float _drainRateTracker;
    private Health _health;
    private bool _isDraining = true;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 2)
            _isDraining = false;
    }

    private void Start()
    {
        _health = GetComponent<Health>();
        _drainRateTracker = _drainRate;
    }

    private void FixedUpdate()
    {
        if (!_isDraining) return;
        
        if (_drainRateTracker >= 0)
        {
            _drainRateTracker -= Time.deltaTime;
            return;
        }

        _health.Damage(_drainingValue);
        _drainRateTracker = _drainRate;


    }
}
