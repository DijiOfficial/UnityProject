using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _startHealth = 10;
    [SerializeField] private TempPlayerInfo _tempPlayerInfo;
    [SerializeField] private GameObject _explosion;

    [SerializeField] private GameObject _coinOrbTemplate = null;
    [SerializeField] private GameObject _healthOrbTemplate = null;
    [SerializeField] private GameObject _soulOrbTemplate = null;
    [SerializeField] protected GameObject _damageTextTemplate = null;
    [SerializeField] private int _defaultDropChancePercent = 5;
    [SerializeField] private int _defaultGoldDropChance = 5;
    [SerializeField] private int _numberOfOrbs;
    [SerializeField] private UnityEvent _onExplosion;

    private float _specialDamageReduction;
    private bool _isPlayer = false;
    private int _currentHealth = 0;
    private ComboScript _comboScript;
    private Collider _collider;
    private GameObject _healthBar;
    public float StartHealth { get { return _startHealth; } }
    public float SpecialDamageReduction { set { _specialDamageReduction = value; } }
    public int CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; } }

    public delegate void HealthChange(float startHealth, float currentHealth);
    public event HealthChange OnHealthChanged;

    private void Awake()
    {
        _isPlayer = GetComponent<PlayerCharacter>() != null;

        if (_isPlayer)
            _startHealth += _tempPlayerInfo._vitalEssence * 25;

        _currentHealth = _startHealth;
        _comboScript = GetComponent<ComboScript>();
        _collider = GetComponent<Collider>();
        if (_tempPlayerInfo != null)
        {
            _defaultGoldDropChance = _defaultGoldDropChance + _tempPlayerInfo._gravelordChosen;
            if (_tempPlayerInfo._soulSiphon)
                _numberOfOrbs *= 2;
        }
    }
    private void Start()
    {
        if (_isPlayer) return;

        var gameObject = transform.Find("HealthBarDisplay");
        if (gameObject!=null)
            _healthBar = gameObject.gameObject;
        else
        {
            gameObject = transform.Find("HealthBarScale");
            if (gameObject != null)
            {
                var child = gameObject.Find("HealthBarDisplay");
                if (child != null)
                    _healthBar = child.gameObject;
            }
        }

        if (_healthBar != null)
            _healthBar.SetActive(false);
    }

    public void CallHealthChange()
    {
        OnHealthChanged?.Invoke(_startHealth, _currentHealth);
    }

    public void Damage(int amount, bool isCrit = false)
    {
        if (_isPlayer)
        {
            amount -= (int)(_tempPlayerInfo._fortifiedResolve * 5);
            amount = (int)(amount * (1 - _specialDamageReduction));
        }


        // Cap the damage at a minimum of 1
        amount = Mathf.Max(amount, 1);
        if (_isPlayer && _specialDamageReduction == 1.0f)
            amount = 0;

        _currentHealth -= amount;

        if (_isPlayer && _currentHealth < 0 && !_tempPlayerInfo._hasRevived && _tempPlayerInfo._phoenixHeart)
        {
            _currentHealth = _startHealth;
            _tempPlayerInfo._hasRevived = true;

            Instantiate(_explosion, transform.position, Quaternion.identity);
            _onExplosion?.Invoke();
        }

        OnHealthChanged?.Invoke(_startHealth, _currentHealth);
        if (_currentHealth <= 0)
            Kill();
        else
            GetComponent<BasicCharacter>().IsHit = true;

        if (_healthBar != null)
            _healthBar.SetActive(true);

        if (_damageTextTemplate)
            ShowDamageText(amount, isCrit);
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
    public void IncreaseHealth(int amount)
    {
        _currentHealth += amount;
        _startHealth += amount;

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

        SpawnCoin();
    }
    public void SpawnSoulOrb(Vector3 spawnPosition)
    {
        int totalDropChance = _defaultDropChancePercent + StaticVariablesManager.Instance.GetSoulDropChanceIncreasePercent;
        if (Random.Range(0, 100) < totalDropChance && _soulOrbTemplate)
        {
            Instantiate(_soulOrbTemplate, spawnPosition, Quaternion.identity);
        }
    }

    private void SpawnCoin()
    {
        if (Random.Range(0, 100) < _defaultGoldDropChance && _coinOrbTemplate)
        {
            Vector3 spawnPosition = GetComponent<Collider>().bounds.center;
            Instantiate(_coinOrbTemplate, spawnPosition, Quaternion.identity);
        }
    }

    private void ShowDamageText(int amount, bool isCrit = false)
    {
        // Calculate a random x position within the collider's width
        float randomX = Random.Range(-_collider.bounds.extents.x * 2.0f, _collider.bounds.extents.x * 2.0f);
        float randomY = Random.Range(0.2f, 0.5f);
        // Calculate the position above the enemy based on its size and the random x position
        Vector3 spawnPosition = _collider.bounds.center + new Vector3(randomX, _collider.bounds.size.y * randomY, 0);

        // Instantiate the damage text at the calculated position
        var text = Instantiate(_damageTextTemplate, spawnPosition, Quaternion.identity);
        var textMesh = text.GetComponent<TextMesh>();
        textMesh.text = amount.ToString();

        // Change text color to red if it's a critical hit
        if (isCrit)
            textMesh.color = Color.red;
    }
}








