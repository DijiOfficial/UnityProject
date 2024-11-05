using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrbBehaviour : MonoBehaviour
{
    [Header("Spawn Force")]
    [SerializeField] protected float _upwardStrength;
    [SerializeField] private float _hoverHeight = 100f;

    [Header("Player Interaction")]
    [SerializeField] private float _attractionRange = 5f;
    [SerializeField] private float _moveTowardsPlayerSpeed = 15f;
    [SerializeField] private float _playerCenterOffset = 1.0f; 
    [SerializeField] private int _healingPower = 20;

    private bool _isTargetingPlayer = false;
    private Transform _playerTransform;
    private Rigidbody _rigidbody;

    protected const string GROUND_STRING = "Ground";

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Vector3 randomDirection = new Vector3(Random.Range(-0.5f, 0.5f), 1f, Random.Range(-0.5f, 0.5f)).normalized;
        _rigidbody.AddForce(randomDirection * _upwardStrength, ForceMode.Impulse);

        // Find player in the scene and get its transform
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();
        if (player != null)
        {
            _playerTransform = player.transform;
        }
    }

    void FixedUpdate()
    {
        // Check if player is within attraction range
        //todo: use square distance instead
        if (_isTargetingPlayer || (_playerTransform != null && Vector3.Distance(transform.position, _playerTransform.position) <= _attractionRange))
        {
            _isTargetingPlayer = true;

            // Adjust target position to player's center by adding vertical offset
            Vector3 targetPosition = _playerTransform.position + Vector3.up * _playerCenterOffset;
            Vector3 directionToPlayer = (targetPosition - transform.position).normalized;
            _rigidbody.velocity = directionToPlayer * _moveTowardsPlayerSpeed;
            return;
        }

        // Hover behavior if player is out of range
        Vector3 rayOrigin = transform.position + Vector3.up * 0.2f;

        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, _hoverHeight, LayerMask.GetMask(GROUND_STRING)))
        {
            _rigidbody.useGravity = false;
            _rigidbody.velocity = Vector3.zero;

            // Gradually move it up if below hover height
            float distanceToTarget = _hoverHeight - hit.distance;
            if (distanceToTarget > 0.01f)
            {
                _rigidbody.position += 10f * distanceToTarget * Time.deltaTime * Vector3.up;
            }
        }
        else
        {
            _rigidbody.useGravity = true;
        }
    }

    const string FRIENDLY_TAG = "Friendly";
    void OnTriggerEnter(Collider other)
    {
        //make sure we only hit friendly
        if (other.tag != FRIENDLY_TAG)
            return;

        if (other.name != "Player")
            return;

        // Get the player’s health component and heal
        Health playerHealth = other.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.Heal(_healingPower);
            Destroy(gameObject); // Destroy the health orb after healing
        }
    }
}
