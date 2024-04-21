using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
    Map,
    Bathroom,
    Battle,
    Boss,
    ShadyMan,
}

public class OnuSceneManager
{
    public static void ChangeScene(Scenes scene)
    {
        Debug.Log(scene);
        switch (scene)
        {
            case Scenes.Bathroom:
                SceneManager.LoadScene("Bathroom");
                break;
            case Scenes.Battle:
                SceneManager.LoadScene("Battle");
                break;
            case Scenes.Boss:
                SceneManager.LoadScene("Boss");
                break;
            case Scenes.ShadyMan:
                SceneManager.LoadScene("ShadyMan");
                break;
        }
    }

    // private IEnumerator DelayedChangedScene()
    // {
    //     yield return new WaitForSeconds(2);
    //     SceneManager.LoadScene(sceneToGoTo);
    // }
}
