using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthOrbBehaviour : MonoBehaviour
{
    [SerializeField] private UnityEvent _onCollideEvent;
    [SerializeField] protected AudioSource _audioSource;
    [SerializeField] protected Renderer _renderer;
    [SerializeField] protected Collider _collider;
    [SerializeField] protected TrailRenderer _trailRenderer;


    [Header("Spawn Force")]
    [SerializeField] protected float _upwardStrength;
    [SerializeField] private float _hoverHeight = 100f;

    [Header("Player Interaction")]
    [SerializeField] private float _attractionRange = 5f;
    [SerializeField] private float _moveTowardsPlayerSpeed = 15f;
    [SerializeField] private float _playerCenterOffset = 1.0f;
    [SerializeField] private int _healingPower = 5;

    [Header("References")]
    [SerializeField] protected TempPlayerInfo _tempPlayerInfo;

    private bool _isTargetingPlayer = false;
    private Transform _playerTransform;
    private Rigidbody _rigidbody;

    protected const string GROUND_STRING = "Ground";
    protected const string FRIENDLY_TAG = "Friendly";

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_tempPlayerInfo != null)
            _healingPower += Mathf.RoundToInt(_tempPlayerInfo._restorativeVitality * (_healingPower * 0.2f));
    }

    protected virtual void Start()
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

    protected virtual void FixedUpdate()
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

    protected virtual void OnTriggerEnter(Collider other)
    {
        //make sure we only hit friendly
        if (other.tag != FRIENDLY_TAG)
            return;

        if (other.name != "Player")
            return;
        _onCollideEvent?.Invoke();
        // Get the player’s health component and heal
        Health playerHealth = other.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.Heal(_healingPower);
            // Disable the visual components and play the sound
            if (_renderer != null) _renderer.enabled = false;
            if (_collider != null) _collider.enabled = false;
            if (_trailRenderer != null) _trailRenderer.enabled = false;

            if (_audioSource != null)
            {
                StartCoroutine(DestroyAfterSound(_audioSource.clip.length));
            }
            else
            {
                Destroy(gameObject);
            }
        }


    }
    private IEnumerator DestroyAfterSound(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        Destroy(gameObject);
    }
}
