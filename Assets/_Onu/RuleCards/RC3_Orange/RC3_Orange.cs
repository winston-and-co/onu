using System.Linq;
using UnityEngine;

namespace RuleCards
{
    public class Orange : AbstractRuleCard
    {
        public override int Id => 3;
        public override string Name => "Orange";

        public static Orange New()
        {
            var orange = New<Orange>() as Orange;
            orange.tooltips.Add(new()
            {
                Title = orange.Name,
                Body = "Yellow and Red are the same Color.",
            });
            return orange;
        }

        public override RuleResult<bool> ColorsMatch(GameMaster gm, AbstractEntity e, Color color, Color target, int depth)
        {
            if (color == CardColor.Yellow || color == CardColor.Red)
            {
                if (target == CardColor.Yellow || target == CardColor.Red)
                    return (true, 10);

                Color opp = color == CardColor.Yellow ? CardColor.Red : CardColor.Yellow;

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