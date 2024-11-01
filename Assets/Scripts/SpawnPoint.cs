using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RandomSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawnTemplate = null;

    private Transform _playerTransform;
    private void Start()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            _playerTransform = player.transform;
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

        do
        {
            randomSpawnPosition = new Vector3(Random.Range(-49, 50), 0, Random.Range(-49, 50));
            distanceToPlayerSqr = (_playerTransform.position - randomSpawnPosition).sqrMagnitude;
        }
        while (distanceToPlayerSqr <= minDistanceSqr);

        return Instantiate(_spawnTemplate, randomSpawnPosition, Quaternion.identity);
    }
}

