using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttakcBehaviour : MovingAttack
{
    protected override void OnTriggerEnter(Collider other)
    {
        //make sure we only hit friendly or enemies
        if (other.tag != FRIENDLY_TAG && other.tag != ENEMY_TAG)
            return;

        //only hit the opposing team
        if (other.tag == tag)
            return;

        Health otherHealth = other.GetComponent<Health>();
        if (otherHealth != null)
        {
            if (other.tag == FRIENDLY_TAG)
            {
                SpecialPowerScript specialPowerScript = other.GetComponent<SpecialPowerScript>();
                if (_tempPlayerInfo._thornOfRetribution && specialPowerScript != null && specialPowerScript.IsActivated)
                {
                    _parentHealth.Damage((int)(_damage * 1.25f), false);
                }
            }

            otherHealth.Damage(_damage, _isCrit);
            if (!_tempPlayerInfo._rangedKiller)
                Kill();
        }
    }
}
