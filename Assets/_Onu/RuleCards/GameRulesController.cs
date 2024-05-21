using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Cards;
using RuleCards;
using UnityEngine;

class AbstractRuleCardComparer : IEqualityComparer<AbstractRuleCard>
{
    public bool Equals(AbstractRuleCard x, AbstractRuleCard y)
    {
        return x.Id == y.Id;
    }

    public int GetHashCode(AbstractRuleCard obj)
    {
        return obj.Id;
    }
}

public class GameRulesController : IRuleset
{
    readonly GameObject ruleCardsContainer;
    public readonly HashSet<AbstractRuleCard> Rules = new(new AbstractRuleCardComparer());

    public GameRulesController(AbstractEntity entity)
    {
        GameObject go = new("RuleCards");
        go.transform.SetParent(entity.transform);
        ruleCardsContainer = go;
    }

    public bool AddRuleCard(AbstractRuleCard ruleCard)
    {
        var success = Rules.Add(ruleCard);
        if (success)
        {
            ruleCard.gameObject.SetActive(true);
            ruleCard.transform.SetParent(ruleCardsContainer.transform);
        }
        else
        {
            UnityEngine.Object.Destroy(ruleCard.gameObject);
        }
        return success;
    }
    public bool RemoveRuleCard(AbstractRuleCard ruleCard)
    {
        var success = Rules.Remove(ruleCard);
        UnityEngine.Object.Destroy(ruleCard.gameObject);
        return success;
    }

    public void ReturnToContainer()
    {
        foreach (var r in Rules)
        {
            r.transform.SetParent(ruleCardsContainer.transform);
        }
    }

    /// <param name="depth">Unused</param>
    public RuleResult<bool> ColorsMatch(GameMaster gm, AbstractEntity e, Color color, Color target, int depth = 0)
    {
        var res = Rules
            .Select(r => r.ColorsMatch(gm, e, color, target, 0))
            .OrderBy(r => -r.Precedence)
            .ToList();

        if (res.Count == 0)
            throw new Exception("Rule check (ColorsMatch) had no valid result. Is GameRulesController.rules missing DefaultRuleset?");

        return (res[0].Result, int.MaxValue);
    }

    public RuleResult<bool> CardIsPlayable(GameMaster gm, AbstractEntity e, AbstractCard c)
    {
        var res = Rules
            .Select(r => r.CardIsPlayable(gm, e, c))
            .OrderBy(r => -r.Precedence)
            .ToList();

        if (res.Count == 0)
            throw new Exception("Rule check (CardIsPlayable) had no valid result. Is GameRulesController.rules missing DefaultRuleset?");

        return (res[0].Result, int.MaxValue);
    }

    public RuleResult<int> CardManaCost(GameMaster gm, AbstractEntity e, AbstractCard c)
    {
        var res = Rules
            .Select(r => r.CardManaCost(gm, e, c))
            .OrderBy(r => -r.Precedence)
            .ToList();

        if (res.Count == 0)
            throw new Exception("Rule check (CardManaCost) had no valid result. Is GameRulesController.rules missing DefaultRuleset?");

        return (res[0].Result, int.MaxValue);
    }

    public RuleResult<bool> CanDraw(GameMaster gm, AbstractEntity e)
    {
        var res = Rules
            .Select(r => r.CanDraw(gm, e))
            .OrderBy(r => -r.Precedence)
            .ToList();

        if (res.Count == 0)
            throw new Exception("Rule check (CanDraw) had no valid result. Is GameRulesController.rules missing DefaultRuleset?");

        return (res[0].Result, int.MaxValue);
    }
}
