using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RandomSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject _skeletonPrefab;
    [SerializeField]
    private GameObject _archerPrefab;
    [SerializeField]
    private GameObject _zombiePrefab;
    [SerializeField]
    private GameObject _necromancerPrefab;

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

    public GameObject Spawn(int difficulty)
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

        GameObject enemyPrefab = SelectEnemyPrefab(difficulty);

        if (enemyPrefab != null)
        {
            return Instantiate(enemyPrefab, hit.position, Quaternion.identity);
        }

        return null;
    }

    private GameObject SelectEnemyPrefab(int difficulty)
    {
        // Define the probabilities for each enemy type based on the difficulty level
        float[] probabilities;

        switch (difficulty)
        {
            case 1:
                probabilities = new float[] { 0.7f, 0.2f, 0.1f, 0.0f }; // Mostly Skeletons
                break;
            case 2:
                probabilities = new float[] { 0.5f, 0.3f, 0.2f, 0.0f }; // Skeletons and Archers
                break;
            case 3:
                probabilities = new float[] { 0.3f, 0.4f, 0.2f, 0.1f }; // More Archers and some Zombies
                break;
            case 4:
                probabilities = new float[] { 0.2f, 0.3f, 0.3f, 0.2f }; // Balanced mix
                break;
            case 5:
                probabilities = new float[] { 0.1f, 0.2f, 0.3f, 0.4f }; // Mostly stronger enemies
                break;
            default:
                probabilities = new float[] { 0.7f, 0.2f, 0.1f, 0.0f }; // Default to level 1 probabilities
                break;
        }
        float randomValue = Random.value;
        float cumulativeProbability = 0.0f;

        if (randomValue < (cumulativeProbability += probabilities[0]))
            return _skeletonPrefab;
        if (randomValue < (cumulativeProbability += probabilities[1]))
            return _archerPrefab;
        if (randomValue < (cumulativeProbability += probabilities[2]))
            return _zombiePrefab;
        if (randomValue < (cumulativeProbability += probabilities[3]))
            return _necromancerPrefab;

        return _skeletonPrefab; // Default to Skeleton if something goes wrong
    }
}

