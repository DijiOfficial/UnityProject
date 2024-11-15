using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RandomSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawnTemplate = null;

    private Transform _playerTransform;
    private void Start()
    {
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();
        if (player != null)
        {
            _playerTransform = player.GetComponent<Transform>();
        }
        else
        {
            Debug.LogError("Player not found. Ensure the player object is tagged as 'Player'.");
        }
    }
    private void OnEnable()
    {
        SpawnManager.Instance.RegisterSpawnPoint(this);
    }
    private void OnDisable()
    {
        //this could be called when the game shuts down and all objects are destroyed, so check if the SpawnManager still exists to avoid recreating it
        if (SpawnManager.Exists)
            SpawnManager.Instance.UnRegisterSpawnPoint(this);
    }

    public GameObject Spawn()
    {
        if (_playerTransform == null)
        {
            Debug.LogWarning("Spawn attempt failed - Player not found.");
            return null;
        }

        Vector3 randomSpawnPosition;
        float distanceToPlayerSqr;
        float minDistanceSqr = 100f;
        NavMeshHit hit;
        bool validPosition = false;

        do
        {
            randomSpawnPosition = new Vector3(Random.Range(-49, 50), 0, Random.Range(-49, 50));
            distanceToPlayerSqr = (_playerTransform.position - randomSpawnPosition).sqrMagnitude;

            // Check if the position is on a valid NavMesh
            validPosition = NavMesh.SamplePosition(randomSpawnPosition, out hit, 1.0f, NavMesh.AllAreas);
        }
        while (distanceToPlayerSqr <= minDistanceSqr || !validPosition);

        return Instantiate(_spawnTemplate, hit.position, Quaternion.identity);
    }
}

