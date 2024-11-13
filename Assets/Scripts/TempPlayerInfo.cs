using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TempPlayerInfo", menuName = "Persistence")]
public class TempPlayerInfo : ScriptableObject
{
    public int _health;

    void OnEnable()
    {
        _health = 100;
    }
}
