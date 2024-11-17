using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] private TempPlayerInfo _tempPlayerInfo;
    [SerializeField] private string sceneToLoad;
    [SerializeField] private UnityEvent _onTeleportEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            //this doesnt work but one can always hope
            _onTeleportEvent?.Invoke();

            _tempPlayerInfo._health = other.GetComponent<Health>().CurrentHealth;
            if (StaticVariablesManager.Instance.CurrentLevel == 5 && sceneToLoad == "SandBoxScene")
                SceneManager.LoadScene("BossRoom");
            else
                SceneManager.LoadScene(sceneToLoad);

        }
    }
}
