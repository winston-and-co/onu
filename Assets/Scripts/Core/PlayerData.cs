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

        GameObject acContainer = new()
        {
            name = "ActionCards"
        };
        acContainer.transform.SetParent(transform);
        acContainer.SetActive(false);

        AddActionCard("AC0_Wild");
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
    public ReadOnlyCollection<ActionCardBase> ActionCards => actionCards.AsReadOnly();

    public void AddActionCard(string actionCardName)
    {
        var go = Instantiate(Resources.Load<GameObject>($"Prefabs/ActionCards/{actionCardName}"));
        go.transform.SetParent(transform.GetChild(0));
        var ac = go.GetComponent<ActionCardBase>();
        actionCards.Add(ac);
    }

    public bool RemoveActionCard(ActionCardBase actionCard)
    {
        var res = actionCards.Remove(actionCard);
        Destroy(actionCard);
        return res;
    }
}
