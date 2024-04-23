using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class GameRulesController : Ruleset
{
    private readonly HashSet<Ruleset> rules = new() { new DefaultRuleset() };
    public HashSet<Ruleset> Rules { get => rules; }

    public bool Add(Ruleset ruleset)
    {
        return rules.Add(ruleset);
    }
    public bool Remove(Ruleset ruleset)
    {
        return rules.Remove(ruleset);
    }

    /// <param name="depth">Unused</param>
    public override RuleResult<bool> ColorsMatch(GameMaster gm, Entity e, Color color, Color target, int depth = 0)
    {
        var res = rules
            .Select(r => r.ColorsMatch(gm, e, color, target, 0))
            .OrderBy(r => -r.Precedence)
            .ToList();

        if (res.Count == 0)
            throw new Exception("Rule check (ColorsMatch) had no valid result. Is GameRulesController.rules missing DefaultRuleset?");

        return (res[0].Result, int.MaxValue);
    }

    public override RuleResult<bool> CardIsPlayable(GameMaster gm, Entity e, Playable c)
    {
        var res = rules
            .Select(r => r.CardIsPlayable(gm, e, c))
            .OrderBy(r => -r.Precedence)
            .ToList();

        if (res.Count == 0)
            throw new Exception("Rule check (CardIsPlayable) had no valid result. Is GameRulesController.rules missing DefaultRuleset?");

        return (res[0].Result, int.MaxValue);
    }

    public override RuleResult<int> CardManaCost(GameMaster gm, Entity e, Playable c)
    {
        var res = rules
            .Select(r => r.CardManaCost(gm, e, c))
            .OrderBy(r => -r.Precedence)
            .ToList();

        if (res.Count == 0)
            throw new Exception("Rule check (CardIsPlayable) had no valid result. Is GameRulesController.rules missing DefaultRuleset?");

        return (res[0].Result, int.MaxValue);
    }

    public override RuleResult<bool> CanDraw(GameMaster gm, Entity e)
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
