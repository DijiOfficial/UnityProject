using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] private TempPlayerInfo _tempPlayerInfo;
    [SerializeField] private string sceneToLoad;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            _tempPlayerInfo._health = other.GetComponent<Health>().CurrentHealth;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
