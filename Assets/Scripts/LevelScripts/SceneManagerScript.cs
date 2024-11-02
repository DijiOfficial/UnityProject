using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
