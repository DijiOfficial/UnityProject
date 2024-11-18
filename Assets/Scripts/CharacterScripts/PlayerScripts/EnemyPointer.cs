using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyPointer : MonoBehaviour
{
    [SerializeField] private GameObject _arrowPrefab;
    private bool _isArrowCreated = false;
    private GameObject _arrowInstance;

    // Update is called once per frame
    void Update()
    {
        int enemyCount = StaticVariablesManager.Instance.EnemyCount;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 2)
        {
            if (!_isArrowCreated && enemyCount > 0 && enemyCount <= 5)
            {
                _arrowInstance = Instantiate(_arrowPrefab, transform.position, Quaternion.identity);
                _arrowInstance.transform.SetParent(transform);
                _isArrowCreated = true;
            }
            else if (_isArrowCreated && (enemyCount == 0 || enemyCount > 5))
            {
                Destroy(_arrowInstance);
                _isArrowCreated = false;
            }
        }
        else if (_isArrowCreated)
        {
            Destroy(_arrowInstance);
            _isArrowCreated = false;
        }
    }
}

