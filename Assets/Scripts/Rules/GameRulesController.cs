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

    public override RuleResult CardIsPlayable(GameMaster gm, Entity e, Playable c)
    {
        List<RuleResult> res = rules
            .Select(r => r.CardIsPlayable(gm, e, c))
            .OrderBy(r => r.Precedence)
            .ToList();

        for (int i = 0; i < res.Count; i++)
        {
            if (res[i].Result == null)
            {
                continue;
            }
            return (res[i].Result, int.MaxValue);
        }

        throw new Exception("Rule check (CardIsPlayable) had no valid result. Is GameRulesController.rules missing DefaultRuleset?");
    }

    public override RuleResult CanDraw(GameMaster gm, Entity e)
    {
        List<RuleResult> res = rules
            .Select(r => r.CanDraw(gm, e))
            .OrderBy(r => r.Precedence)
            .ToList();

        for (int i = 0; i < res.Count; i++)
        {
            if (res[i].Result == null)
            {
                continue;
            }
            return (res[i].Result, int.MaxValue);
        }

        throw new Exception("Rule check (CanDraw) had no valid result. Is GameRulesController.rules missing DefaultRuleset?");
    }
}
