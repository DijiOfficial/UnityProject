using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelManager : MonoBehaviour
{
    const string STARTNEWWAVE_METHOD = "StartNewWave";
    private readonly float _firstWaveStart = 2.0f;
    private float _currentFrequency = 10.0f;
    //private float _waveStartFrequency = 15.0f;
    //private float _waveEndFrequency = 7.0f;
    //private float _waveFrequencyIncrement = 0.5f;

    [SerializeField] private float _difficultyModifier = 3.78f;
    private int _levelDifficulty = 1;
    private int _wavesToSpawn = 0;
    private float _totalDifficulty = 0.0f;

    [SerializeField] private GameObject _teleporterTemplate = null;
    private bool _isWaveActive = false;
    private bool _isWaveStarted = false;
    void Awake()
    {
        //_currentFrequency = _waveStartFrequency;

        _levelDifficulty = StaticVariablesManager.Instance.CurrentLevel;
        _totalDifficulty = _levelDifficulty * _difficultyModifier;
        //divide by somthing to get lower waves
        _wavesToSpawn = Mathf.CeilToInt(_totalDifficulty);
        
        Invoke(STARTNEWWAVE_METHOD, _firstWaveStart);
        _isWaveActive = true;
        _isWaveStarted = true;
    }
    
    private void Update()
    {
        //if StaticVariablesManager._enemyCount <= 0 and _isWaveActive start new waver and overwrite the Invoke (can't have no enemies)
        if (!_isWaveActive && _isWaveStarted)
        {
            if (StaticVariablesManager.Instance.EnemyCount <= 0)
            {
                //reset bools necessary?
                _isWaveStarted = false;
                _isWaveActive = false;

                Instantiate(_teleporterTemplate, Vector3.zero, Quaternion.identity);
                StaticVariablesManager.Instance.CurrentLevel++;
                StaticVariablesManager.Instance.ResetEnemyCount();
            }
        }
    }

    void StartNewWave()
    {
        SpawnManager.Instance.SpawnWave(Mathf.CeilToInt(_totalDifficulty));
        if (--_wavesToSpawn <= 0) 
        {
            _isWaveActive = false;
            return;
        }
        //_currentFrequency = Mathf.Clamp(_currentFrequency - _waveFrequencyIncrement, _waveEndFrequency, _waveStartFrequency);

        Invoke(STARTNEWWAVE_METHOD, _currentFrequency);
    }
}

