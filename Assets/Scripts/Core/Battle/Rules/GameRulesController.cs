using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using Cards;

public class GameRulesController : IRuleset
{
    private readonly HashSet<RulesetBase> rules = new() { new DefaultRuleset() };
    public HashSet<RulesetBase> Rules { get => rules; }

    public bool Add(RulesetBase ruleset)
    {
        return rules.Add(ruleset);
    }
    public bool Remove(RulesetBase ruleset)
    {
        return rules.Remove(ruleset);
    }

    /// <param name="depth">Unused</param>
    public RuleResult<bool> ColorsMatch(GameMaster gm, AbstractEntity e, Color color, Color target, int depth = 0)
    {
        var res = rules
            .Select(r => r.ColorsMatch(gm, e, color, target, 0))
            .OrderBy(r => -r.Precedence)
            .ToList();

        if (res.Count == 0)
            throw new Exception("Rule check (ColorsMatch) had no valid result. Is GameRulesController.rules missing DefaultRuleset?");

        return (res[0].Result, int.MaxValue);
    }

    public RuleResult<bool> CardIsPlayable(GameMaster gm, AbstractEntity e, AbstractCard c)
    {
        var res = rules
            .Select(r => r.CardIsPlayable(gm, e, c))
            .OrderBy(r => -r.Precedence)
            .ToList();

        if (res.Count == 0)
            throw new Exception("Rule check (CardIsPlayable) had no valid result. Is GameRulesController.rules missing DefaultRuleset?");

        return (res[0].Result, int.MaxValue);
    }

    public RuleResult<int> CardManaCost(GameMaster gm, AbstractEntity e, AbstractCard c)
    {
        var res = rules
            .Select(r => r.CardManaCost(gm, e, c))
            .OrderBy(r => -r.Precedence)
            .ToList();

        if (res.Count == 0)
            throw new Exception("Rule check (CardIsPlayable) had no valid result. Is GameRulesController.rules missing DefaultRuleset?");

        return (res[0].Result, int.MaxValue);
    }

    public RuleResult<bool> CanDraw(GameMaster gm, AbstractEntity e)
    {
        var res = rules
            .Select(r => r.CanDraw(gm, e))
            .OrderBy(r => -r.Precedence)
            .ToList();

        if (res.Count == 0)
            throw new Exception("Rule check (CanDraw) had no valid result. Is GameRulesController.rules missing DefaultRuleset?");

        return (res[0].Result, int.MaxValue);
    }
}
