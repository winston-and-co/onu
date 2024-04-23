using System.Linq;
using UnityEngine;

namespace RuleCards
{
    public class Chartreuse : Ruleset
    {
        public override string Name => "RC2_Chartreuse";

        public override RuleResult<bool> ColorsMatch(GameMaster gm, Entity e, Color color, Color target, int depth)
        {
            if (color == CardColors.Green || color == CardColors.Yellow)
            {
                if (target == CardColors.Green || target == CardColors.Yellow)
                    return (true, 10);

                Color opp = color == CardColors.Green ? CardColors.Yellow : CardColors.Green;

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