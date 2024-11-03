using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    private const string KILL_METHOD = "Kill";
    [SerializeField] private float _lifeTime = 0.2f;
    [SerializeField] private int _damage = 5;

    private void Awake()
    {
        Invoke(KILL_METHOD, _lifeTime);
    }
    void Kill()
    {
        Destroy(gameObject);
    }

    const string FRIENDLY_TAG = "Friendly";
    const string ENEMY_TAG = "Enemy";
    void OnTriggerEnter(Collider other)
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
