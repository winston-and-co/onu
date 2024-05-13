using System.Linq;
using UnityEngine;

namespace RuleCards
{
    public class Chartreuse : RulesetBase
    {
        public override string Name => "RC2_Chartreuse";

        public override RuleResult<bool> ColorsMatch(GameMaster gm, AbstractEntity e, Color color, Color target, int depth)
        {
            if (color == CardColor.Green || color == CardColor.Yellow)
            {
                if (target == CardColor.Green || target == CardColor.Yellow)
                    return (true, 10);

                Color opp = color == CardColor.Green ? CardColor.Yellow : CardColor.Green;

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