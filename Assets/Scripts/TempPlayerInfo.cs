using System.IO;
using UnityEngine;

[System.Serializable]
public class UpgradeData
{
    public string Title;
    public string Bonus;
    public string Description;
    public bool Acquired;
}

[System.Serializable]
public class UpgradeList
{
    public UpgradeData[] Upgrades;
}

[CreateAssetMenu(fileName = "TempPlayerInfo", menuName = "Persistence")]
public class TempPlayerInfo : ScriptableObject
{
    public int _vitalEssence;
    public int _secondarySkill;
    public int _adrenalineRush;
    public int _restorativeVitality;
    public int _berserkerFury;
    public int _swiftStride;
    public int _keenEdge;
    public int _blazingAgility;
    public int _fortifiedResolve;
    public int _hasteOfTheWarrior;
    public int _curseProtection;
    public int _gravelordChosen;


    public int _goldCoins;
    public int _health;

    public bool _phoenixHeart;
    public bool _soulSiphon;
    public bool _aegisShield;
    public bool _ironWall;
    public bool _thornOfRetribution;
    public bool _vengeanceBarrier;
    public bool _detonationGuard;
    public bool _keenEdgeUnlock;
    public bool _deathBlow;
    public bool _quickDash;
    public bool _blinkStep;
    public bool _bloodThirst;
    public bool _guardianRespite;
    public bool _relentlessPursuit;
    public bool _momentumMystery;
    public bool _rangedKiller;
    public bool _reaperReward;
    public bool _shrapnelBurst;

    public bool _hasRevived;
    public void AddBonus(int idx)
    {
        switch (idx)
        {
            case 1:
                _vitalEssence++;
                break;
            case 2:
                _secondarySkill++;
                break;
            case 3:
                _adrenalineRush++;
                break;
            case 4:
                _restorativeVitality++;
                break;
            case 5:
                _berserkerFury++;
                break;
            case 6:
                _swiftStride++;
                break;
            case 7:
                _keenEdge++;
                break;
            case 8:
                _blazingAgility++;
                break;
            case 9:
                _fortifiedResolve++;
                break;
            case 10:
                _hasteOfTheWarrior++;
                break;
            case 11:
                _curseProtection++;
                break;
            case 12:
                _gravelordChosen++;
                break;
            default:
                Debug.LogWarning("Invalid index: " + idx);
                break;
        }
    }

    public int GetTotalBonus(int idx)
    {
        int total = 0;
        switch (idx)
        {
            case 1:
                total = _vitalEssence;
                break;
            case 2:
                total = _secondarySkill;
                break;
            case 3:
                total = _adrenalineRush;
                break;
            case 4:
                total = _restorativeVitality;
                break;
            case 5:
                total = _berserkerFury;
                break;
            case 6:
                total = _swiftStride;
                break;
            case 7:
                total = _keenEdge;
                break;
            case 8:
                total = _blazingAgility;
                break;
            case 9:
                total = _fortifiedResolve;
                break;
            case 10:
                total = _hasteOfTheWarrior;
                break;
            case 11:
                total = _curseProtection;
                break;
            case 12:
                total = _gravelordChosen;
                break;
            default:
                Debug.LogWarning("Invalid index: " + idx);
                break;
        }

        return total;
    }

    void OnEnable()
    {
        CustomReset();
        LoadPermanentUpgrades();
    }
    private void LoadPermanentUpgrades()
    {
        string filePath = Path.Combine(Application.dataPath, "PermanentUpgrades.txt");
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("PermanentUpgrades.txt not found at: " + filePath);
            return;
        }

        string json = File.ReadAllText(filePath);
        UpgradeList upgradeList = JsonUtility.FromJson<UpgradeList>(json);

        foreach (UpgradeData upgrade in upgradeList.Upgrades)
        {
            SetUpgrade(upgrade.Title, upgrade.Acquired);
        }
    }

    private void SetUpgrade(string title, bool acquired)
    {
        switch (title)
        {
            case "Phoenix Heart":
                _phoenixHeart = acquired;
                break;
            case "Soul Siphon":
                _soulSiphon = acquired;
                break;
            case "Aegis Shield":
                _aegisShield = acquired;
                break;
            case "Iron Wall":
                _ironWall = acquired;
                break;
            case "Thorns of Retribution":
                _thornOfRetribution = acquired;
                break;
            case "Vengeance Barrier":
                _vengeanceBarrier = acquired;
                break;
            case "Detonation Guard":
                _detonationGuard = acquired;
                break;
            case "Keen Edge":
                _keenEdgeUnlock = acquired;
                break;
            case "Death Blow":
                _deathBlow = acquired;
                break;
            case "Quick Dash":
                _quickDash = acquired;
                break;
            case "Blink Step":
                _blinkStep = acquired;
                break;
            case "Bloodthirst":
                _bloodThirst = acquired;
                break;
            case "Guardian's Respite":
                _guardianRespite = acquired;
                break;
            case "Relentless Pursuit":
                _relentlessPursuit = acquired;
                break;
            case "Momentum Mastery":
                _momentumMystery = acquired;
                break;
            case "Ranged Killer":
                _rangedKiller = acquired;
                break;
            case "Reaper's Reward":
                _reaperReward = acquired;
                break;
            case "Shrapnel Burst":
                _shrapnelBurst = acquired;
                break;
            case "Unavailable":
                // Do nothing, this is a placeholder for "locked" upgrades
                break;
            default:
                Debug.LogWarning("Unknown upgrade title: " + title);
                break;
        }
    }

    public void CustomReset()
    {
        _vitalEssence = 0;
        _secondarySkill = 0;
        _adrenalineRush = 0;
        _restorativeVitality = 0;
        _berserkerFury = 0;
        _swiftStride = 0;
        _keenEdge = 0;
        _blazingAgility = 0;
        _fortifiedResolve = 0;
        _hasteOfTheWarrior = 0;
        _curseProtection = 0;
        _gravelordChosen = 0;
        _goldCoins = 0;
        _health = 100;

        _hasRevived = false;
    }
}