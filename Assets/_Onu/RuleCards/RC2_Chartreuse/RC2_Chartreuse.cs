using System.Linq;
using UnityEngine;

namespace RuleCards
{
    public class Chartreuse : AbstractRuleCard
    {
        public override int Id => 2;
        public override string Name => "Chartreuse";

        public static Chartreuse New()
        {
            var chartreuse = New<Chartreuse>() as Chartreuse;
            chartreuse.tooltips.Add(new()
            {
                Title = chartreuse.Name,
                Body = "Green and Yellow are the same Color.",
            });
            return chartreuse;
        }

        public override RuleResult<bool> ColorsMatch(GameMaster gm, AbstractEntity e, Color color, Color target, int depth)
        {
            if (color == CardColor.Green || color == CardColor.Yellow)
            {
                if (target == CardColor.Green || target == CardColor.Yellow)
                    return (true, 10);

                Color opp = color == CardColor.Green ? CardColor.Yellow : CardColor.Green;

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