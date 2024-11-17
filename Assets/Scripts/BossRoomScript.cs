using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossRoomScript : MonoBehaviour
{
    private GameObject _boss;

    // Start is called before the first frame update
    void Start()
    {
        _boss = GameObject.Find("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        if (_boss == null)
        {
            StartCoroutine(LoadEndScreen());
        }
    }

    private IEnumerator LoadEndScreen()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("EndScreen");
    }
}
