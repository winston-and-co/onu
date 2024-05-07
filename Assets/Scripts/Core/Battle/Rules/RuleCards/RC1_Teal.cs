using System.Linq;
using UnityEngine;

namespace RuleCards
{
    public class Teal : RulesetBase
    {
        public override string Name => "RC1_Teal";

        public override RuleResult<bool> ColorsMatch(GameMaster gm, Entity e, Color color, Color target, int depth)
        {
            if (color == CardColors.Blue || color == CardColors.Green)
            {
                if (target == CardColors.Blue || target == CardColors.Green)
                    return (true, 10);

                Color opp = color == CardColors.Blue ? CardColors.Green : CardColors.Blue;

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