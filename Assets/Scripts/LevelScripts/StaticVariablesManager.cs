using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // Add this line for file operations

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
        SaveSoulCoinsToFile(); // Save the _soulCoins value to a file when the application quits
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

        LoadSoulCoinsFromFile(); // Load the _soulCoins value from a file when the application starts
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
    private static int _soulCoins;
    private static int _soulDropChanceIncreasePercent = 5;
    private const string STATIC_INFO_FILE = "data.txt"; // File name to save/load _soulCoins

    public int GetCoinAmount { get { return _soulCoins; } }
    public int GetSoulDropChanceIncreasePercent { get { return _soulDropChanceIncreasePercent; } }
    public int CurrentLevel
    {
        get { return _currentLevel; }
        set 
        { 
            _currentLevel = value; 
            OnLevelChange?.Invoke(_currentLevel);
        }
    }
    public int EnemyCount
    {
        get { return _enemyCount; }
        //set { _enemyCount = value; }
    }

    public delegate void EnemyKilled();
    public event EnemyKilled OnEnemyKilled;
    public void DecreaseEnemyCount()
    {
        _enemyCount--;
        OnEnemyKilled?.Invoke();
        OnEnemyChange?.Invoke(_enemyCount);
    }
    public void IncreaseEnemyCount()
    {
        _enemyCount++;
        OnEnemyChange?.Invoke(_enemyCount);
    }
    public void ResetEnemyCount()
    {
        _enemyCount = 0;
    }

    public delegate void SoulCoinChange(int coins);
    public event SoulCoinChange OnSoulCoinChanged;

    public delegate void LevelChange(int coins);
    public event LevelChange OnLevelChange;

    public delegate void EnemyChange(int enemies);
    public event EnemyChange OnEnemyChange;
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

    private void LoadSoulCoinsFromFile()
    {
        if (File.Exists(STATIC_INFO_FILE))
        {
            string fileContents = File.ReadAllText(STATIC_INFO_FILE);
            if (int.TryParse(fileContents, out int loadedCoins))
            {
                _soulCoins = loadedCoins;
            }
        }
    }

    private void SaveSoulCoinsToFile()
    {
        File.WriteAllText(STATIC_INFO_FILE, _soulCoins.ToString());
    }
}
