using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameRulesController : Ruleset
{
    private readonly List<Ruleset> rules = new() { new DefaultRuleset() };

    public void Add(Ruleset ruleset)
    {
        if (!rules.Contains(ruleset))
        {
            rules.Add(ruleset);
        }
    }
    public bool Remove(Ruleset ruleset)
    {
        return rules.Remove(ruleset);
    }

    public override RuleResult<bool> CardIsPlayable(GameMaster gm, Entity e, Playable c)
    {
        List<RuleResult<bool>> res = rules
            .Select(r => r.CardIsPlayable(gm, e, c))
            .OrderBy(r => -r.Precedence)
            .ToList();

        if (res.Count == 0)
            throw new Exception("Rule check (CardIsPlayable) had no valid result. Is GameRulesController.rules missing DefaultRuleset?");

        return (res[0].Result, int.MaxValue);
    }

    public override RuleResult<int> CardManaCost(GameMaster gm, Entity e, Playable c)
    {
        List<RuleResult<int>> res = rules
            .Select(r => r.CardManaCost(gm, e, c))
            .OrderBy(r => -r.Precedence)
            .ToList();

        if (res.Count == 0)
            throw new Exception("Rule check (CardIsPlayable) had no valid result. Is GameRulesController.rules missing DefaultRuleset?");

        return (res[0].Result, int.MaxValue);
    }

    public override RuleResult<bool> CanDraw(GameMaster gm, Entity e)
    {
        List<RuleResult<bool>> res = rules
            .Select(r => r.CanDraw(gm, e))
            .OrderBy(r => -r.Precedence)
            .ToList();

        if (res.Count == 0)
            throw new Exception("Rule check (CanDraw) had no valid result. Is GameRulesController.rules missing DefaultRuleset?");

        return (res[0].Result, int.MaxValue);
    }
}
