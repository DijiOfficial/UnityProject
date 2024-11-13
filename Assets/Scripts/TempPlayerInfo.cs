using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TempPlayerInfo", menuName = "Persistence")]
public class TempPlayerInfo : ScriptableObject
{
    public int _health;
    public int _totalRangedAttack;
    public int _totalHealthUpgrades;
    public int _totalDamageIncreaseOnLowHealth;
    public int _totalDamageIncrease;
    public int _totalSprint;
    public int _totalCurseResist;
    public int _totalHealthRegen;
    public int _totalCritChance;
    public int _totalAttackSpeed;
    public int _totalMovementSpeed;
    public int _totalDamageReduction;



    void OnEnable()
    {
        _health = 100;
        _totalRangedAttack = 1;
        _totalHealthUpgrades = 0;
        _totalDamageIncreaseOnLowHealth = 0;
        _totalDamageIncrease = 0;
        _totalSprint = 0;
        _totalCurseResist = 0;
        _totalHealthRegen = 0;
        _totalCritChance = 0;
        _totalAttackSpeed = 0;
        _totalMovementSpeed = 0;
        _totalDamageReduction = 0;
    }
}
