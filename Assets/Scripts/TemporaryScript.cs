using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryScript : MonoBehaviour
{
    private const string KILL_METHOD = "Kill";
    private float _lifeTime = 0.2f;

    private void Awake()
    {
        // Find the player GameObject
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            // Get the SpecialPowerScript component
            SpecialPowerScript specialPowerScript = player.GetComponent<SpecialPowerScript>();
            if (specialPowerScript != null)
            {
                // Get the duration from the SpecialPowerScript
                _lifeTime = specialPowerScript.Duration;
            }
        }

        Invoke(KILL_METHOD, _lifeTime);
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
}
