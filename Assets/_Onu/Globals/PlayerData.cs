using System.Collections.Generic;
using System.Collections.ObjectModel;
using ActionCards;
using RuleCards;
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

        ActionCardsParent = new GameObject("ActionCards");
        ActionCardsParent.transform.SetParent(gameObject.transform);
        ActionCardsParent.SetActive(false);
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

    public GameObject ActionCardsParent;
    List<AbstractActionCard> _actionCards = new();
    public List<AbstractActionCard> ActionCards
    {
        get => _actionCards;
        set
        {
            _actionCards = new();
            foreach (var ac in value)
            {
                AddActionCard(ac);
            }
        }
    }

    /// <summary>
    /// Adds an Action Card instance. Sets PlayerData.ActionCards object as parent object.
    /// </summary>
    public void AddActionCard(AbstractActionCard actionCard)
    {
        _actionCards.Add(actionCard);
        actionCard.gameObject.SetActive(true);
        actionCard.transform.SetParent(ActionCardsParent.transform);
    }

    /// <summary>
    /// Removes an Action Card instance.
    /// </summary>
    public void RemoveActionCard(AbstractActionCard actionCard)
    {
        int i = _actionCards.IndexOf(actionCard);
        _actionCards.RemoveAt(i);
        Destroy(actionCard.gameObject);
    }

    /// <summary>
    /// Calls matching method in player entity's GameRulesController.
    /// </summary>
    public bool AddRuleCard(AbstractRuleCard ruleCard)
    {
        return Player.gameRulesController.AddRuleCard(ruleCard);
    }

    /// <summary>
    /// Calls matching method in player entity's GameRulesController.
    /// </summary>
    public bool RemoveRuleCard(AbstractRuleCard ruleCard)
    {
        return Player.gameRulesController.RemoveRuleCard(ruleCard);
    }
}
