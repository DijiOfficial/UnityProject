using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private GameObject _player = null;

    private void Update()
    {
        if (_player == null)
            TriggerRespawn();
    }

    void TriggerRespawn()
    {
        StaticVariablesManager._currentLevel = 1;
        SceneManager.LoadScene(0);
    }
}

