using UnityEngine;
using UnityEngine.Events;

public class BasicAttack : MonoBehaviour
{
    protected const string KILL_METHOD = "Kill";
    [SerializeField] protected float _lifeTime = 0.2f;
    [SerializeField] protected int _damage = 5;
    [Header("References")]
    [SerializeField] protected TempPlayerInfo _tempPlayerInfo;
    protected Health _parentHealth;
    protected bool _isCrit = false;
    protected virtual void Awake()
    {
        Invoke(KILL_METHOD, _lifeTime);

        // Find the Health component on the parent GameObject
        // I'm running short on time forgive me
        Transform parentTransform = transform.parent;
        while (parentTransform != null)
        {
            _parentHealth = parentTransform.GetComponent<Health>();
            if (_parentHealth != null)
                break;
            parentTransform = parentTransform.parent;
        }

        // Check if the script is on the SwordAttack GameObject
        if (gameObject.name != "SwordAttack(Clone)" && gameObject.name != "PlayerProjectile Variant(Clone)")
            return;

        _damage += Mathf.RoundToInt(_tempPlayerInfo._berserkerFury * (_damage * 0.2f));

        // Find the player by name
        GameObject player = GameObject.Find("Player");
        if (player == null) return;
        // Get the Health component
        Health healthScript = player.GetComponent<Health>();
        if (healthScript == null) return;
        if (healthScript.CurrentHealth < healthScript.StartHealth * 0.5f)
        {
            // Increase the damage based on _tempPlayerInfo._adrenalineRush
            float additionalDamage = _tempPlayerInfo._adrenalineRush * (_damage * 0.2f);
            _damage += Mathf.RoundToInt(additionalDamage);
        }

        if (!_tempPlayerInfo._keenEdgeUnlock) return;
        int damageMultiplier = 2;
        if (!_tempPlayerInfo._deathBlow)
            damageMultiplier = 4;
        // Add a random calculation for crit chance
        if (_tempPlayerInfo._keenEdge >= 10)
        {
            _damage *= damageMultiplier;
            _isCrit = true;
            return;
        }

        float critChance = 0.1f * _tempPlayerInfo._keenEdge;
        if (Random.value < critChance)
        {
            _damage *= damageMultiplier;
            _isCrit = true;
        }
    }

    protected virtual void Kill()
    {
        Destroy(gameObject);
    }

    protected const string FRIENDLY_TAG = "Friendly";
    protected const string ENEMY_TAG = "Enemy";
    protected virtual void OnTriggerEnter(Collider other)
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
            Kill();
        }
    }
}
