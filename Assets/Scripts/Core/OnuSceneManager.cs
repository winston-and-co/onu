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

public class OnuSceneManager : MonoBehaviour
{
    [SerializeField] Map map;
    static OnuSceneManager Instance;
    public static OnuSceneManager GetInstance() => Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Map")
        {
            map.gameObject.SetActive(false);
            return;
        }
        map.gameObject.SetActive(true);
    }

    public void ChangeScene(Scenes scene)
    {
        Debug.Log("ChangeScene to: " + scene);
        switch (scene)
        {
            case Scenes.Map:
                SceneManager.LoadSceneAsync("Map");
                break;
            case Scenes.Bathroom:
                SceneManager.LoadSceneAsync("Bathroom");
                break;
            case Scenes.Battle:
                SceneManager.LoadSceneAsync("Battle");
                break;
            case Scenes.Boss:
                SceneManager.LoadSceneAsync("Boss");
                break;
            case Scenes.ShadyMan:
                SceneManager.LoadSceneAsync("ShadyMan");
                break;
        }
    }

    /// <param name="delay">Time to wait in seconds</param>
    public void ChangeSceneWithDelay(Scenes scene, int delay)
    {
        Debug.Log("ChangeSceneWithDelay: " + delay);
        StartCoroutine(WaitThenChange(scene, delay));
    }

    private IEnumerator WaitThenChange(Scenes scene, int delay)
    {
        yield return new WaitForSeconds(delay);
        ChangeScene(scene);
    }
}
