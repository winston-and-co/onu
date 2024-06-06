using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
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
            SceneManager.LoadScene("MainMenu");
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

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
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
                case "Boss":
                    CleanBossScene();
                    break;
                default:
                    break;
            }
        }

        // scene initialization
        switch (scene.name)
        {
            case "Map":
                InitMapScene();
                break;
            case "Battle":
                InitBattleScene();
                break;
            case "Boss":
                InitBossScene();
                break;
            default:
                break;
        }
        lastSceneName = scene.name;
    }

    void InitMapScene()
    {
        Map map = FindObjectOfType<Map>(true);
        if (map != null) map.gameObject.SetActive(true);
        // AudioSource bgSource = SoundManager.GetInstance().backgroundSource;
        // bgSource.clip = SoundManager.GetInstance().backgroundMap;
        // bgSource.loop = true;
        // bgSource.Play();
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
        gm.Player = pd.Player;
        // enemy
        EnemyEntity enemy;
        var floor = pd.CurrentNodeLocation.Item1; // 0-index
        if (floor < 5)
            enemy = EnemyFactory.RandomFromPool(EnemyPool.Easy1);
        else
            enemy = EnemyFactory.RandomFromPool(EnemyPool.Hard1);
        gm.Enemy = enemy;
    }
    void CleanBattleScene()
    {
        var pd = PlayerData.GetInstance();
        if (pd.Player == null) throw new System.Exception();
        pd.Player.gameObject.SetActive(false);
        if (pd.Player.deck == null) throw new System.Exception();
        pd.Player.deck.gameObject.SetActive(false);
    }

    void InitBossScene()
    {
        // player
        var pd = PlayerData.GetInstance();
        if (pd.Player == null) throw new System.Exception();
        pd.Player.gameObject.SetActive(true);
        if (pd.Player.deck == null) throw new System.Exception();
        pd.Player.deck.gameObject.SetActive(true);
        var gm = GameMaster.GetInstance();
        gm.Player = pd.Player;
        // enemy
        EnemyEntity enemy;
        enemy = EnemyFactory.RandomFromPool(EnemyPool.Boss1);
        gm.Enemy = enemy;
    }
    void CleanBossScene()
    {
        var pd = PlayerData.GetInstance();
        if (pd.Player == null) throw new System.Exception();
        pd.Player.gameObject.SetActive(false);
        if (pd.Player.deck == null) throw new System.Exception();
        pd.Player.deck.gameObject.SetActive(false);
    }

    public void ChangeScene(SceneType scene)
    {
        Debug.Log("ChangeScene to: " + scene);
        switch (scene)
        {
            case SceneType.Map:
                SceneManager.LoadScene("Map");
                break;
            case SceneType.Bathroom:
                SceneManager.LoadSceneAsync("Bathroom");
                break;
            case SceneType.Battle:
                SceneManager.LoadSceneAsync("Battle");
                break;
            case SceneType.Boss:
                SceneManager.LoadSceneAsync("Battle");
                break;
            case SceneType.ShadyMan:
                SceneManager.LoadSceneAsync("ShadyMan");
                break;
        }
    }

    /// <param name="delay">Time to wait in seconds</param>
    public void ChangeSceneWithDelay(SceneType scene, int delay)
    {
        Debug.Log("ChangeSceneWithDelay: " + delay);
        StartCoroutine(WaitThenChange(scene, delay));
    }

    private IEnumerator WaitThenChange(SceneType scene, int delay)
    {
        yield return new WaitForSeconds(delay);
        ChangeScene(scene);
    }
}
