using UnityEngine;
using UnityEngine.SceneManagement;

public class SingletonsManager : MonoBehaviour
{
    static SingletonsManager Instance;
    public static SingletonsManager GetInstance() => Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SpawnSingletons();
    }

    [SerializeField] GameObject onuSceneManagerPrefab;
    [SerializeField] GameObject playerDataPrefab;
    [SerializeField] GameObject prefabLoaderPrefab;
    [SerializeField] GameObject eventManagerPrefab;
    [SerializeField] GameObject eventTrackerPrefab;
    [SerializeField] GameObject gameCursorPrefab;
    [SerializeField] GameObject tutorialPrefab;
    [SerializeField] GameObject soundManagerPrefab;

    void OnEnable() { SceneManager.sceneLoaded += OnSceneLoaded; }
    void OnDisable() { SceneManager.sceneLoaded -= OnSceneLoaded; }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        var go = Instantiate(tutorialPrefab);
        go.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
        go.transform.SetAsLastSibling();
    }

    void SpawnSingletons()
    {
        Instantiate(onuSceneManagerPrefab);
        Instantiate(playerDataPrefab);
        Instantiate(prefabLoaderPrefab);
        Instantiate(eventManagerPrefab);
        Instantiate(eventTrackerPrefab);
        Instantiate(gameCursorPrefab);
        Instantiate(soundManagerPrefab);
    }
}