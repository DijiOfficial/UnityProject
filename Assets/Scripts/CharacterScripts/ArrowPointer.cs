using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPointer : MonoBehaviour
{
    private GameObject _target;

    void Update()
    {
        GameObject closestEnemy = FindClosestEnemy();
        if (closestEnemy != null)
        {
            Vector3 direction = closestEnemy.transform.position - transform.parent.position;
            direction.y = 0; // Keep the direction on the horizontal plane
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg; // Adjusted to use x and z components
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    private GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.parent.position;

        foreach (GameObject enemy in enemies)
        {
            if (IsValidEnemy(enemy))
            {
                float distance = Vector3.Distance(enemy.transform.position, currentPosition);
                if (distance < minDistance)
                {
                    closestEnemy = enemy;
                    minDistance = distance;
                }
            }
        }

        return closestEnemy;
    }

    private bool IsValidEnemy(GameObject enemy)
    {
        string[] validEnemyNames = { "Archer", "ZOMBIE", "Necromancer", "SKELETON" };
        foreach (string name in validEnemyNames)
        {
            if (enemy.name.Contains(name))
            {
                return true;
            }
        }
        return false;
    }
}


