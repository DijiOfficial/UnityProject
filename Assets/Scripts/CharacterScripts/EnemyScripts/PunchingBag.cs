using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchingBag : SimpleEnemy
{
    protected override void Update()
    {
        if (_isHit)
        {
            _isHit = false;
            _enemyAnimation.HandleHit();
        }
    }
}
