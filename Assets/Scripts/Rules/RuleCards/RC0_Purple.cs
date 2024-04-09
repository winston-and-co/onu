namespace RuleCards
{
    public class Purple : Ruleset
    {
        public override RuleResult CardIsPlayable(GameMaster gm, Entity e, Playable c)
        {
            var top = gm.discard.Peek();
            if (top != null)
            {
                if (top.Color == CardColors.Blue || top.Color == CardColors.Red
                && c.Color == CardColors.Blue || c.Color == CardColors.Red)
                {
                    return (true, 10);
                }
            }
            return (null, -1);
        }
    }
}