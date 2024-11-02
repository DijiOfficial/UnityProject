using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulCoinTracker : MonoBehaviour
{
    public delegate void SoulCoinChange(int coins);
    public event SoulCoinChange OnSoulCoinChanged;
    public void AddCoin(int amount = 1)
    {
        StaticVariablesManager._soulCoins += amount;

        OnSoulCoinChanged?.Invoke(StaticVariablesManager._soulCoins);
    }
    public void RemoveCoin(int amount = 1)
    {
        StaticVariablesManager._soulCoins -= amount;

        OnSoulCoinChanged?.Invoke(StaticVariablesManager._soulCoins);
    }
}
