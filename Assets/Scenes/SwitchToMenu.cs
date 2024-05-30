using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchToMenu : MonoBehaviour
{
    [SerializeField] string SceneName;
    [SerializeField] float waitTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartSwitchCount());
    }

    IEnumerator StartSwitchCount()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneName);
    }
}
