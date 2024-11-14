using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPowerScript : MonoBehaviour
{
    [Header("Cooldown")]
    [SerializeField] private float _cooldown;
    [SerializeField] private float _duration;
    [SerializeField] private float _specialDamageReduction = 0.5f;
    private float _timer;
    private float _durationTimer;
    private Health _health;
    private bool _isActivated = false;
    private void Start()
    {
        _health = GetComponent<Health>();
    }

    private void Update()
    {
        if (_timer > 0.0f)
            _timer -= Time.deltaTime;

        if (!_isActivated) return;
     
        if (_durationTimer > 0.0f)
            _durationTimer -= Time.deltaTime;
        else
        {
            _isActivated = false;
            _health.SpecialDamageReduction = 0.0f;
        }
    }

    public void Activate()
    {
        if (_timer > 0.0f)
            return;
        else
            _timer = _cooldown + _duration;

        _isActivated = true;
        _durationTimer = _duration;
        _health.SpecialDamageReduction = _specialDamageReduction;
    }
}




