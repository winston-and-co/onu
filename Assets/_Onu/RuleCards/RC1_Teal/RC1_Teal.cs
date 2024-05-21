using System.Linq;
using UnityEngine;

namespace RuleCards
{
    public class Teal : AbstractRuleCard
    {
        public override int Id => 1;
        public override string Name => "Teal";
        public override string Description => "Blue and Green are the same Color.";
        public override string SpriteName => "RC1_Teal";

        public static Teal New() => New<Teal>() as Teal;

        public override RuleResult<bool> ColorsMatch(GameMaster gm, AbstractEntity e, Color color, Color target, int depth)
        {
            if (color == CardColor.Blue || color == CardColor.Green)
            {
                if (target == CardColor.Blue || target == CardColor.Green)
                    return (true, 10);

                Color opp = color == CardColor.Blue ? CardColor.Green : CardColor.Blue;

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