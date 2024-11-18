using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSkip : MonoBehaviour
{
    [SerializeField] private TempPlayerInfo _tempPlayerInfo;
    [SerializeField] private GameObject _teleportationPad;

    // Start is called before the first frame update
    void Start()
    {
        if (_tempPlayerInfo._isTutorialCompleted)
            _teleportationPad.SetActive(true);
        else
            _teleportationPad.SetActive(false);
    }
}


