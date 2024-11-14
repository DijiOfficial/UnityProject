using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    protected const string KILL_METHOD = "Kill";
    [SerializeField] protected float _lifeTime = 0.2f;
    [SerializeField] protected int _damage = 5;
    [Header("References")]
    [SerializeField] protected TempPlayerInfo _tempPlayerInfo;

    private bool _isCrit = false;
    protected virtual void Awake()
    {
        Invoke(KILL_METHOD, _lifeTime);

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

        // Add a random calculation for crit chance
        if (_tempPlayerInfo._keenEdge >= 10)
        {
            _damage *= 2;
            _isCrit = true;
            return;
        }

        float critChance = 0.1f * _tempPlayerInfo._keenEdge;
        if (Random.value < critChance)
        {
            _damage *= 2;
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
            otherHealth.Damage(_damage, _isCrit);
            Kill();
        }
    }
}
