using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SpecialPowerScript : MonoBehaviour
{
    [SerializeField] private UnityEvent _onShieldStart;
    [SerializeField] private UnityEvent _onShieldEnd;


    [Header("Cooldown")]
    [SerializeField] private float _cooldown;
    [SerializeField] private float _duration;
    [SerializeField] private float _specialDamageReduction = 0.5f;
    [SerializeField] private GameObject _specialPowerVFX;
    [SerializeField] private GameObject _explosion;

    [Header("References")]
    [SerializeField] protected TempPlayerInfo _tempPlayerInfo;


    public delegate void ShieldChange(float start, float current, float timer, float cooldown);
    public event ShieldChange OnShieldChange;

    private float _timer;
    private float _durationTimer;
    private Health _health;
    private bool _isActivated = false;

    public float Duration { get { return _duration; } }
    public bool IsActivated { get { return _isActivated; } }

    public float Cooldown
    {
        get { return _cooldown; }
    } 
    private void Awake()
    {
        if (_tempPlayerInfo != null)
        {
            if (_tempPlayerInfo._aegisShield)
                _specialDamageReduction += 0.25f;
            if (_tempPlayerInfo._ironWall)
                _specialDamageReduction += 0.25f;

            if(_tempPlayerInfo._guardianRespite)
                _cooldown *= 0.75f;
        }
    }
    private void Start()
    {
        _health = GetComponent<Health>();
    }

    private void Update()
    {
        if (_timer > 0.0f)
            _timer -= Time.deltaTime;

        OnShieldChange?.Invoke(_duration, _durationTimer, _timer, _cooldown); // yeah yeah it should be at the end
        if (!_isActivated) return;
     
        if (_durationTimer > 0.0f)
            _durationTimer -= Time.deltaTime;
        else
        {
            _isActivated = false;
            _health.SpecialDamageReduction = 0.0f;
            if(_tempPlayerInfo._detonationGuard)
                Instantiate(_explosion, transform.position, Quaternion.identity);

            _onShieldEnd?.Invoke();
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

        // Instantiate the special power VFX as a child of the player
        GameObject vfxInstance = Instantiate(_specialPowerVFX, transform.position, Quaternion.identity);
        vfxInstance.transform.SetParent(transform);
        _onShieldStart?.Invoke();
    }
}




