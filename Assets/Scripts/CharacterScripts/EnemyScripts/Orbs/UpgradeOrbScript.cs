using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeOrbScript : HealthOrbBehaviour
{
    protected override void OnTriggerEnter(Collider other)
    {
        //make sure we only hit friendly
        if (other.tag != FRIENDLY_TAG)
            return;

        if (other.name != "Player")
            return;

        StaticVariablesManager.Instance.AddCoin(1);
        Destroy(gameObject);
    }
}
