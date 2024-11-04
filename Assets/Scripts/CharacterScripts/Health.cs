using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int _startHealth = 10;

    [SerializeField] private GameObject _healthOrbTemplate = null;
    [SerializeField] private int _numberOfOrbs;

    public int _currentHealth = 0;

    public float StartHealth { get { return _startHealth; } }
    public float CurrentHealth { get { return _currentHealth; } }

    public delegate void HealthChange(float startHealth, float currentHealth);
    public event HealthChange OnHealthChanged;

    void Awake()
    {
        _currentHealth = _startHealth;
    }

    public void Damage(int amount)
    {
        _currentHealth -= amount;

        OnHealthChanged?.Invoke(_startHealth, _currentHealth);
        if (_currentHealth <= 0)
            Kill();
    }

    public void Heal(int amount)
    {
        _currentHealth += amount;

        if (_currentHealth > _startHealth)
            _currentHealth = _startHealth;

        OnHealthChanged?.Invoke(_startHealth, _currentHealth);
    }
    void Kill()
    {
        Destroy(gameObject);

        if (_healthOrbTemplate)
        {
            Vector3 spawnPosition = GetComponent<Collider>().bounds.center;
            for (int i = 0; i < _numberOfOrbs; i++)
            {
                Instantiate(_healthOrbTemplate, spawnPosition, Quaternion.identity);
            }
        }
    }
}








