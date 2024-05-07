using System.Collections.Generic;
using System.Collections.ObjectModel;
using ActionCards;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    static PlayerData Instance;
    public static PlayerData GetInstance() => Instance;

    private void Awake()
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
    /// Tuple of (level index, node index)
    /// </summary>
    public (int, int) CurrentNodeLocation = (-1, -1);
    public MapNode CurrentNode
    {
        get
        {
            if (CurrentNodeLocation.Item1 == -1 || CurrentNodeLocation.Item2 == -1)
            {
                return null;
            }
            return FindObjectOfType<Map>().levels[CurrentNodeLocation.Item1].nodes[CurrentNodeLocation.Item2];
        }
    }

    public Entity Player;

    readonly List<ActionCardBase> actionCards = new();
    /// <summary>
    /// Holds a list of prefabs.
    /// </summary>
    public ReadOnlyCollection<ActionCardBase> ActionCards => actionCards.AsReadOnly();

    public List<ActionCardBase> InstantiateActionCards()
    {
        List<ActionCardBase> spawned = new();
        for (int i = 0; i < actionCards.Count; i++)
        {
            var obj = Instantiate(actionCards[i]);
            obj.PlayerDataIndex = i;
            spawned.Add(obj);
        }
        return spawned;
    }

    /// <summary>
    /// Add an Action Card by name. Checks if the prefab exists.
    /// </summary>
    /// <param name="actionCardName">Name of the prefab</param>
    /// <returns>Whether it was successful</returns>
    public bool AddActionCard(string actionCardName)
    {
        var res = Resources.Load<ActionCardBase>($"Prefabs/ActionCards/{actionCardName}");
        if (res == null) return false;
        actionCards.Add(res);
        return true;
    }

    /// <summary>
    /// Remove an Action Card by index
    /// </summary>
    /// <param name="actionCard">The index to remove at</param>
    public void RemoveActionCardAt(int idx)
    {
        actionCards.RemoveAt(idx);
    }
}
