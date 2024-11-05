using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticVariablesManager : MonoBehaviour
{
    #region SINGLETON INSTANCE
    private static StaticVariablesManager _instance;
    public static StaticVariablesManager Instance
    {
        get
        {
            if (_instance == null && !ApplicationQuitting)
            {
                _instance = FindObjectOfType<StaticVariablesManager>();
                if (_instance == null)
                {
                    GameObject newInstance = new GameObject("Singleton_SpawnManager");
                    _instance = newInstance.AddComponent<StaticVariablesManager>();
                }
            }
            return _instance;
        }
    }

    //Checks if the singleton is alive, useful to reference it when the game is about to close down to avoid memory leaks
    public static bool Exists
    {
        get
        {
            return _instance != null;
        }
    }

    public static bool ApplicationQuitting = false;
    protected virtual void OnApplicationQuit()
    {
        ApplicationQuitting = true;
    }

    private void Awake()
    {
        //we want this object to persist when a scene changes
        DontDestroyOnLoad(gameObject);
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
    #endregion

    private static int _currentLevel = 1;
    private static int _enemyCount = 0;
    private static int _soulCoins = 25;
    private static int _soulDropChanceIncreasePercent = 5;
    public int GetCoinAmount { get { return _soulCoins; } }
    public int GetSoulDropChanceIncreasePercent { get { return _soulDropChanceIncreasePercent; } }
    public int CurrentLevel
    {
        get { return _currentLevel; }
        set { _currentLevel = value; }
    }
    public int EnemyCount
    {
        get { return _enemyCount; }
        set { _enemyCount = value; }
    }
    public delegate void SoulCoinChange(int coins);
    public event SoulCoinChange OnSoulCoinChanged;
    public void AddCoin(int amount = 1)
    {
        _soulCoins += amount;

        OnSoulCoinChanged?.Invoke(_soulCoins);
    }
    public void RemoveCoin(int amount = 1)
    {
        _soulCoins -= amount;

        OnSoulCoinChanged?.Invoke(_soulCoins);
    }
}