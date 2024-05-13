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

    public AbstractEntity Player;

    public readonly List<string> ActionCards = new();

    public List<AbstractActionCard> InstantiateActionCards()
    {
        List<AbstractActionCard> spawned = new();
        for (int i = 0; i < ActionCards.Count; i++)
        {
            var ac = ActionCardHelper.CreateActionCard(ActionCards[i]);
            ac.PlayerDataIndex = i;
            spawned.Add(ac);
        }
        return spawned;
    }

    /// <summary>
    /// Add an Action Card by name. Checks if it is a valid name.
    /// </summary>
    /// <param name="actionCardName">Name of the Action Card without id (e.g. "Wild")</param>
    /// <returns>Whether it was successful</returns>
    public bool AddActionCard(string actionCardName)
    {
        if (ActionCardHelper.CheckExists(actionCardName))
        {
            ActionCards.Add(actionCardName);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Remove an Action Card by index
    /// </summary>
    /// <param name="actionCard">The index to remove at</param>
    public void RemoveActionCardAt(int idx)
    {
        ActionCards.RemoveAt(idx);
    }
}
