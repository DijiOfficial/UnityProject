using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesTracker : MonoBehaviour
{
    private int _coins;

    public int Coins
    {
        get { return _coins; }
    }

    public void AddCoin(int amount)
    {
        _coins += amount;
    }
}
