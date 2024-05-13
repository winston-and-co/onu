using System.Linq;
using UnityEngine;

namespace RuleCards
{
    public class Orange : RulesetBase
    {
        public override string Name => "RC3_Orange";

        public override RuleResult<bool> ColorsMatch(GameMaster gm, AbstractEntity e, Color color, Color target, int depth)
        {
            if (color == CardColor.Yellow || color == CardColor.Red)
            {
                if (target == CardColor.Yellow || target == CardColor.Red)
                    return (true, 10);

                Color opp = color == CardColor.Yellow ? CardColor.Red : CardColor.Yellow;

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