namespace RuleCards
{
    public class Orange : Ruleset
    {
        public override RuleResult CardIsPlayable(GameMaster gm, Entity e, Playable c)
        {
            var top = gm.discard.Peek();
            if (top != null)
            {
                if (top.Color == CardColors.Yellow || top.Color == CardColors.Red
                && c.Color == CardColors.Yellow || c.Color == CardColors.Red)
                {
                    return (true, 10);
                }
            }
            return (null, -1);
        }
    }
}