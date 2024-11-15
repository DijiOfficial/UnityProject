using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private GameObject _player = null;
    [SerializeField] private TempPlayerInfo _tempPlayerInfo;

    private void Update()
    {
        if (_player == null)
            TriggerRespawn();
    }

    void TriggerRespawn()
    {
        _tempPlayerInfo.CustomReset();
        StaticVariablesManager.Instance.CurrentLevel = 1;
        SceneManager.LoadScene(1);
    }
}

