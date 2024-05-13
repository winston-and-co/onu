using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RuleCards
{
    public class Purple : RulesetBase
    {
        public override string Name => "RC0_Purple";

        public override RuleResult<bool> ColorsMatch(GameMaster gm, AbstractEntity e, Color color, Color target, int depth)
        {
            if (color == CardColor.Red || color == CardColor.Blue)
            {
                if (target == CardColor.Red || target == CardColor.Blue)
                    return (true, 10);

                Color opp = color == CardColor.Red ? CardColor.Blue : CardColor.Red;

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