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

    public PlayerEntity Player;

    public List<int> ActionCards { get; set; } = new();

    /// <summary>
    /// Adds an Action Card id.
    /// </summary>
    public void AddActionCard(int actionCardId)
    {
        for (int i = 0; i < ActionCards.Count; i++)
        {
            if (ActionCards[i] > actionCardId)
            {
                ActionCards.Insert(i - 1, actionCardId);
            }
        }
        ActionCards.Add(actionCardId);
    }

    /// <summary>
    /// Removes an Action Card id by index and destroys the game object.
    /// </summary>
    public void RemoveActionCard(AbstractActionCard instance)
    {
        ActionCards.RemoveAt(instance.PlayerDataIndex);
        Destroy(instance.gameObject);
    }

    /// <summary>
    /// Instantiates list of Action Cards.
    /// </summary>
    public List<AbstractActionCard> GetActionCardInstances()
    {
        List<AbstractActionCard> res = new();
        for (int i = 0; i < ActionCards.Count; i++)
        {
            var instance = ActionCardFactory.MakeActionCard(ActionCards[i]);
            instance.PlayerDataIndex = i;
            res.Add(instance);
        }
        return res;
    }

    bool _firstFoundRuleCardFlag = false;
    /// <summary>
    /// Calls matching method in player entity's GameRulesController.
    /// </summary>
    public bool AddRuleCard(AbstractRuleCard ruleCard)
    {
        if (!_firstFoundRuleCardFlag)
        {
            Tutorial.Instance.RuleCardsTutorial();
            _firstFoundRuleCardFlag = true;
        }
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
