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
        return x.Name == y.Name;
    }

    public int GetHashCode(AbstractRuleCard obj)
    {
        return obj.Name.GetHashCode();
    }
}

public class GameRulesController : IRuleset
{
    public AbstractEntity Entity;
    public readonly HashSet<AbstractRuleCard> Rules = new(new AbstractRuleCardComparer());

    public GameRulesController(AbstractEntity e)
    {
        Entity = e;
    }

    public int GetCount()
    {
        // not counting default rules
        return Rules.Count - 1;
    }

    public bool AddRuleCard(AbstractRuleCard ruleCard)
    {
        if (ruleCard != null)
        {
            ruleCard.Entity = Entity;
            return Rules.Add(ruleCard);
        }
        return false;
    }
    public bool RemoveRuleCard(AbstractRuleCard ruleCard)
    {
        if (ruleCard is DefaultRuleset) return false;
        return Rules.Remove(ruleCard);
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
