using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RuleCards
{
    public class Purple : AbstractRuleCard
    {
        public override int Id => 0;
        public override string Name => "Purple";

        public static Purple New()
        {
            var purple = New<Purple>() as Purple;
            purple.tooltips.Add(new()
            {
                Title = purple.Name,
                Body = "Red and Blue are the same Color.",
            });
            return purple;
        }

        public override RuleResult<bool> ColorsMatch(GameMaster gm, AbstractEntity e, Color color, Color target, int depth)
        {
            if (color == CardColor.Red || color == CardColor.Blue)
            {
                if (target == CardColor.Red || target == CardColor.Blue)
                    return (true, 10);

                Color opp = color == CardColor.Red ? CardColor.Blue : CardColor.Red;

                if (depth == e.gameRulesController.Rules.Count)
                {
                    return (target == opp, 10);
                }
                var otherRulesets = e.gameRulesController.Rules.Where((r) => r.Name != Name);
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