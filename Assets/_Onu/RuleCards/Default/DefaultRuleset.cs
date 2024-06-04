using UnityEngine;
using Cards;

namespace RuleCards
{
    public class DefaultRuleset : AbstractRuleCard
    {
        public override string Name => "";
        public override string Description => "";
        public override string SpriteName => "";

        public static DefaultRuleset New() => new();

        public override RuleResult<bool> ColorsMatch(GameMaster gm, AbstractEntity e, Color color, Color target, int depth)
        {
            return (color == target, 0);
        }

        public override RuleResult<bool> CardIsPlayable(GameMaster gm, AbstractEntity e, AbstractCard c)
        {
            var discard = gm.DiscardPile;
            var topCard = discard.Peek();
            // first card
            if (topCard == null) return (true, 0);
            // colorless cards are always playable, and you can play anything on them
            if (topCard.Color == CardColor.Colorless || c.Color == CardColor.Colorless) return (true, 0);
            // matching color
            if (e.gameRulesController.ColorsMatch(gm, e, c.Color, topCard.Color))
                return (true, 0);
            // matching value only
            if (c.Value == topCard.Value)
            {
                if (e.mana >= c.Value)
                {
                    return (true, 0);
                }
                return (false, 0);
            }
            return (false, 0);
        }

        public override RuleResult<int> CardManaCost(GameMaster gm, AbstractEntity e, AbstractCard c)
        {
            var topCard = gm.DiscardPile.Peek();
            if (topCard != null && !e.gameRulesController.ColorsMatch(gm, e, c.Color, topCard.Color))
            {
                return (c.Cost, 0);
            }
            return (0, 0);
        }

        public override RuleResult<bool> CanDraw(GameMaster gm, AbstractEntity e)
        {
            // there may be effects preventing draw
            return (true, 0);
        }
    }
}
