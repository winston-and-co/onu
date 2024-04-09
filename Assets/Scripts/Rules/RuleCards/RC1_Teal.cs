namespace RuleCards
{
    public class Teal : Ruleset
    {
        public override RuleResult CardIsPlayable(GameMaster gm, Entity e, Playable c)
        {
            var top = gm.discard.Peek();
            if (top != null)
            {
                if (top.Color == CardColors.Blue || top.Color == CardColors.Green
                && c.Color == CardColors.Blue || c.Color == CardColors.Green)
                {
                    return (true, 10);
                }
            }
            return (null, -1);
        }
    }
}