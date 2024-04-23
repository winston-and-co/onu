using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RuleCards
{
    public class Purple : Ruleset
    {
        public override string Name => "RC0_Purple";

        public override RuleResult<bool> ColorsMatch(GameMaster gm, Entity e, Color color, Color target, int depth)
        {
            if (color == CardColors.Red || color == CardColors.Blue)
            {
                if (target == CardColors.Red || target == CardColors.Blue)
                    return (true, 10);

                Color opp = color == CardColors.Red ? CardColors.Blue : CardColors.Red;

                if (depth == e.gameRules.Rules.Count)
                {
                    return (target == opp, 10);
                }
                var otherRulesets = e.gameRules.Rules.Where((r) => r.Name != Name);
                foreach (var r in otherRulesets)
                {
                    var res = r.ColorsMatch(gm, e, opp, target, depth + 1);
                    if (res)
                    {
                        return (res.Result, res.Precedence + 1);
                    }
                }
            }

            return (null, -1);
        }
    }
}