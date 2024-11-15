using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void OnEnable()
    {
        CustomReset();
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
    }
}