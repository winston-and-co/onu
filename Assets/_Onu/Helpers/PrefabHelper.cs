using UnityEngine;

public enum PrefabType
{
    Card,
    CardUI,
    Deck,
    Entity,
    RewardItem,
    Tooltip,
    UI_ColorPicker,
    UI_ActionCardPicker,
    UI_DeckViewer,
    UI_DeckMultiSelect,
}

public class PrefabHelper : MonoBehaviour
{
    [SerializeField] GameObject cardPrefab;
    [SerializeField] GameObject cardUIPrefab;
    [SerializeField] GameObject deckPrefab;
    [SerializeField] GameObject entityPrefab;
    [SerializeField] GameObject rewardItemPrefab;
    [SerializeField] GameObject tooltipPrefab;
    [SerializeField] GameObject colorPickerPrefab;
    [SerializeField] GameObject actionCardPickerPrefab;
    [SerializeField] GameObject deckViewerPrefab;
    [SerializeField] GameObject deckMultiSelectPrefab;

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
            PrefabType.CardUI => cardUIPrefab,
            PrefabType.Deck => deckPrefab,
            PrefabType.Entity => entityPrefab,
            PrefabType.RewardItem => rewardItemPrefab,
            PrefabType.Tooltip => tooltipPrefab,
            PrefabType.UI_ColorPicker => colorPickerPrefab,
            PrefabType.UI_ActionCardPicker => actionCardPickerPrefab,
            PrefabType.UI_DeckViewer => deckViewerPrefab,
            PrefabType.UI_DeckMultiSelect => deckMultiSelectPrefab,
            _ => throw new System.ArgumentException(),
        };
    }

    /// <summary>
    /// Returns an instantiated prefab from given type.
    /// </summary>
    /// <returns>The instantiated prefab GameObject</returns>
    /// <exception cref="System.ArgumentException"></exception>
    public GameObject InstantiatePrefab(PrefabType type)
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