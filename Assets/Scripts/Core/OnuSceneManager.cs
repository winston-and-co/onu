using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene
{
    Map,
    Bathroom,
    Battle,
    Boss,
    ShadyMan,
}

public class OnuSceneManager : MonoBehaviour
{
    static OnuSceneManager Instance;
    public static OnuSceneManager GetInstance() => Instance;
    private string lastSceneName = "";

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        // scene initialization
        switch (scene.name)
        {
            case "Map":
                InitMapScene();
                break;
            case "Battle":
                InitBattleScene();
                break;
            default:
                break;
        }

        // scene cleanup
        if (lastSceneName != scene.name)
        {
            switch (lastSceneName)
            {
                case "Map":
                    CleanMapScene();
                    break;
                case "Battle":
                    CleanBattleScene();
                    break;
                default:
                    break;
            }
        }

        lastSceneName = scene.name;
    }

    void InitMapScene()
    {
        Map map = FindObjectOfType<Map>(true);
        if (map != null) map.gameObject.SetActive(true);
    }
    void CleanMapScene()
    {
        Map map = FindObjectOfType<Map>(true);
        if (map != null) map.gameObject.SetActive(false);
    }

    void InitBattleScene()
    {
        // player
        var pd = PlayerData.GetInstance();
        if (pd.Player == null) throw new System.Exception();
        pd.Player.gameObject.SetActive(true);
        if (pd.Player.deck == null) throw new System.Exception();
        pd.Player.deck.gameObject.SetActive(true);
        var gm = GameMaster.GetInstance();
        gm.player = pd.Player;
        // enemy
        // https://stackoverflow.com/questions/3132126/how-do-i-select-a-random-value-from-an-enumeration
        var eg = EnemyGenerator.GetInstance();
        System.Array enemyCharacters = System.Enum.GetValues(typeof(EnemyCharacter));
        var ec = (EnemyCharacter)enemyCharacters.GetValue(Random.Range(0, enemyCharacters.Length));
        var enemy = eg.Generate(ec);
        gm.enemy = enemy;
    }
    void CleanBattleScene()
    {
        var pd = PlayerData.GetInstance();
        if (pd.Player == null) throw new System.Exception();
        pd.Player.gameObject.SetActive(false);
        if (pd.Player.deck == null) throw new System.Exception();
        pd.Player.deck.gameObject.SetActive(false);
    }

    public void ChangeScene(Scene scene)
    {
        Debug.Log("ChangeScene to: " + scene);
        switch (scene)
        {
            case Scene.Map:
                SceneManager.LoadSceneAsync("Map");
                break;
            case Scene.Bathroom:
                SceneManager.LoadSceneAsync("Bathroom");
                break;
            case Scene.Battle:
                SceneManager.LoadSceneAsync("Battle");
                break;
            case Scene.Boss:
                SceneManager.LoadSceneAsync("Boss");
                break;
            case Scene.ShadyMan:
                SceneManager.LoadSceneAsync("ShadyMan");
                break;
        }
    }

    /// <param name="delay">Time to wait in seconds</param>
    public void ChangeSceneWithDelay(Scene scene, int delay)
    {
        Debug.Log("ChangeSceneWithDelay: " + delay);
        StartCoroutine(WaitThenChange(scene, delay));
    }

    private IEnumerator WaitThenChange(Scene scene, int delay)
    {
        yield return new WaitForSeconds(delay);
        ChangeScene(scene);
    }
}
