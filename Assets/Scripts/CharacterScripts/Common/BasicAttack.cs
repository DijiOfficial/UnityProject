using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    protected const string KILL_METHOD = "Kill";
    [SerializeField] protected float _lifeTime = 0.2f;
    [SerializeField] protected int _damage = 5;

    protected virtual void Awake()
    {
        Invoke(KILL_METHOD, _lifeTime);
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
            otherHealth.Damage(_damage);
            Kill();
        }
    }
}
