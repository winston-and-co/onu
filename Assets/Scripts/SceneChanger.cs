using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneToGoTo;

    public void ChangeScene(){
        StartCoroutine (DelayedChangedScene());
    }

    private IEnumerator DelayedChangedScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneToGoTo);
    }
}
