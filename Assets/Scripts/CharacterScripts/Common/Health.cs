using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int _startHealth = 10;

    [SerializeField] private GameObject _healthOrbTemplate = null;
    [SerializeField] private GameObject _soulOrbTemplate = null;
    [SerializeField] private int _defaultDropChancePercent = 5;
    [SerializeField] private int _numberOfOrbs;
    private int _currentHealth = 0;
    ComboScript _comboScript;

    public float StartHealth { get { return _startHealth; } }
    public float CurrentHealth { get { return _currentHealth; } }

    public delegate void HealthChange(float startHealth, float currentHealth);
    public event HealthChange OnHealthChanged;

    void Awake()
    {
        _currentHealth = _startHealth;
        _comboScript = GetComponent<ComboScript>();
    }

    public void Damage(int amount)
    {
        _currentHealth -= amount;

        OnHealthChanged?.Invoke(_startHealth, _currentHealth);
        if (_currentHealth <= 0)
            Kill();
        else
            GetComponent<BasicCharacter>().IsHit = true;
    }

    public void Heal(int amount)
    {
        if (_comboScript != null)
             amount = (int)(amount * _comboScript.ComboMultiplier);

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

        SpawnSoulOrb();
    }
    public void SpawnSoulOrb()
    {
        int totalDropChance = _defaultDropChancePercent + StaticVariablesManager.Instance.GetSoulDropChanceIncreasePercent;

        if (Random.Range(0, 100) < totalDropChance && _soulOrbTemplate)
        {
            Vector3 spawnPosition = GetComponent<Collider>().bounds.center;
            Instantiate(_soulOrbTemplate, spawnPosition, Quaternion.identity);
        }
    }
}








