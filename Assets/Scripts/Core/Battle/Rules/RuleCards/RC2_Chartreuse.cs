namespace RuleCards
{
    public class Chartreuse : Ruleset
    {
        public override RuleResult<bool> CardIsPlayable(GameMaster gm, Entity e, Playable c)
        {
            var top = gm.discard.Peek();
            if (top != null)
            {
                if (top.Color == CardColors.Green && c.Color == CardColors.Yellow)
                    return (true, 10);
                if (top.Color == CardColors.Yellow && c.Color == CardColors.Green)
                    return (true, 10);
            }
            return (null, -1);
        }

        public override RuleResult<int> CardManaCost(GameMaster gm, Entity e, Playable c)
        {
            if (CardIsPlayable(gm, e, c))
            {
                return (0, 10);
            }
            return (null, -1);
        }
    }
}