using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboScript : MonoBehaviour
{
    [SerializeField] private float _maxComboTimer = 1.5f;
    [SerializeField] private int _maxTotalCombo = 10;
    [SerializeField] private GameObject _comboVFX;
    private float _ComboTimer = 0;
    private float _comboMultiplier = 1;
    private int _totalCombo;
    private Health _healthScript;
    public float ComboMultiplier { get { return _comboMultiplier; } }

    public delegate void ComboChanged(int combo, float start, float current);
    public event ComboChanged OnComboChange;

    private void Start()
    {
        StaticVariablesManager.Instance.OnEnemyKilled += EnemyKilled;
        _healthScript = GetComponent<Health>();
    }
    private void Update()
    {
        if (_ComboTimer > 0)
            _ComboTimer -= Time.deltaTime;
        else
        {
            _comboMultiplier = 1;
            _totalCombo = 0;
        }

        OnComboChange?.Invoke(_totalCombo, _maxComboTimer, _ComboTimer);
    }

    public void EnemyKilled()
    {
        if (_healthScript == null) return;
        _ComboTimer = _maxComboTimer;
        _comboMultiplier += 0.1f;
        _totalCombo++;

        if (_totalCombo >= _maxTotalCombo)
        {
            _ComboTimer = 0;
            _comboMultiplier = 1;
            _totalCombo = 0;
            _healthScript.Heal(999);

            Vector3 spawnPosition = transform.position + transform.forward * 5.0f + transform.up * 1.5f;
            _healthScript.SpawnSoulOrb(spawnPosition);

            if (_comboVFX) Instantiate(_comboVFX, spawnPosition, Quaternion.identity);
            //dont instatiate when stopping the game because it clears all the nemy and spawns this vfx
        }

        OnComboChange?.Invoke(_totalCombo, _maxComboTimer, _ComboTimer);
    }
}
