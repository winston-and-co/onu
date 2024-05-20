using UnityEngine;

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
    [SerializeField] GameObject prefabHelperPrefab;

    void SpawnSingletons()
    {
        GameObject go;
        go = GameObject.Find(onuSceneManagerPrefab.name);
        if (go == null) Instantiate(onuSceneManagerPrefab);
        go = GameObject.Find(playerDataPrefab.name);
        if (go == null) Instantiate(playerDataPrefab);
        go = GameObject.Find(prefabHelperPrefab.name);
        if (go == null) Instantiate(prefabHelperPrefab);
    }
}