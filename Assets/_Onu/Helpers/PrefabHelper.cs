using UnityEngine;

public enum PrefabType
{
    Card,
    Deck,
    Entity,
    RewardItem,
    Tooltip,
    UI_ColorPicker,
}

public class PrefabHelper : MonoBehaviour
{
    [SerializeField] GameObject cardPrefab;
    [SerializeField] GameObject deckPrefab;
    [SerializeField] GameObject colorPickerPrefab;
    [SerializeField] GameObject entityPrefab;
    [SerializeField] GameObject rewardItemPrefab;
    [SerializeField] GameObject tooltipPrefab;

    static PrefabHelper Instance;
    public static PrefabHelper GetInstance() => Instance;

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

    /// <summary>
    /// Returns the prefab from given type.
    /// </summary>
    /// <returns>The prefab GameObject</returns>
    /// <exception cref="System.ArgumentException"></exception>
    public GameObject GetPrefab(PrefabType type)
    {
        return type switch
        {
            PrefabType.Card => cardPrefab,
            PrefabType.Deck => deckPrefab,
            PrefabType.Entity => entityPrefab,
            PrefabType.RewardItem => rewardItemPrefab,
            PrefabType.Tooltip => tooltipPrefab,
            PrefabType.UI_ColorPicker => colorPickerPrefab,
            _ => throw new System.ArgumentException(),
        };
    }

    /// <summary>
    /// Returns an instantiated prefab from given type.
    /// </summary>
    /// <returns>The instantiated prefab GameObject</returns>
    /// <exception cref="System.ArgumentException"></exception>
    public GameObject GetInstantiatedPrefab(PrefabType type)
    {
        return Instantiate(GetPrefab(type));
    }

    /// <summary>
    /// Returns the prefab found using the Resources API.
    /// </summary>
    public static GameObject LoadPrefab(string path)
    {
        return Resources.Load<GameObject>(path);
    }
}