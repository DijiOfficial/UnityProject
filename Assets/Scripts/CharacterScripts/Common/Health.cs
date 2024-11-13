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
    [SerializeField] protected GameObject _damageTextTemplate = null;
    [SerializeField] private int _defaultDropChancePercent = 5;
    [SerializeField] private int _numberOfOrbs;
    private int _currentHealth = 0;
    private ComboScript _comboScript;
    private Collider _collider;
    private GameObject _healthBar;
    public float StartHealth { get { return _startHealth; } }
    public int CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; } }

    public delegate void HealthChange(float startHealth, float currentHealth);
    public event HealthChange OnHealthChanged;

    private void Awake()
    {
        _currentHealth = _startHealth;
        _comboScript = GetComponent<ComboScript>();
        _collider = GetComponent<Collider>();
    }
    private void Start()
    {
        var gameObject = transform.Find("HealthBarDisplay");
        if (gameObject)
            _healthBar = gameObject.gameObject;

        if (_healthBar != null)
            _healthBar.SetActive(false);
    }

    public void Damage(int amount)
    {
        _currentHealth -= amount;

        OnHealthChanged?.Invoke(_startHealth, _currentHealth);
        if (_currentHealth <= 0)
            Kill();
        else
            GetComponent<BasicCharacter>().IsHit = true;

        if (_healthBar != null)
            _healthBar.SetActive(true);

        if (_damageTextTemplate)
            ShowDamageText(amount);
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

        int totalDropChance = _defaultDropChancePercent + StaticVariablesManager.Instance.GetSoulDropChanceIncreasePercent;

        if (Random.Range(0, 100) < totalDropChance && _soulOrbTemplate)
        {
            Vector3 spawnPosition = GetComponent<Collider>().bounds.center;
            Instantiate(_soulOrbTemplate, spawnPosition, Quaternion.identity);
        }
    }
    public void SpawnSoulOrb(Vector3 spawnPosition)
    {
        int totalDropChance = _defaultDropChancePercent + StaticVariablesManager.Instance.GetSoulDropChanceIncreasePercent;
        if (Random.Range(0, 100) < totalDropChance && _soulOrbTemplate)
        {
            Instantiate(_soulOrbTemplate, spawnPosition, Quaternion.identity);
        }
    }
    private void ShowDamageText(int amount)
    {
        // Calculate a random x position within the collider's width
        float randomX = Random.Range(-_collider.bounds.extents.x * 2.0f, _collider.bounds.extents.x * 2.0f);
        float randomY = Random.Range(0.2f, 0.5f);
        // Calculate the position above the enemy based on its size and the random x position
        Vector3 spawnPosition = _collider.bounds.center + new Vector3(randomX, _collider.bounds.size.y * randomY, 0);

        // Instantiate the damage text at the calculated position
        var text = Instantiate(_damageTextTemplate, spawnPosition, Quaternion.identity);
        text.GetComponent<TextMesh>().text = amount.ToString();
    }
}








