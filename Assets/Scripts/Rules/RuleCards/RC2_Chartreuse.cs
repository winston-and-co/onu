namespace RuleCards
{
    public class Chartreuse : Ruleset
    {
        public override RuleResult CardIsPlayable(GameMaster gm, Entity e, Playable c)
        {
            var top = gm.discard.Peek();
            if (top != null)
            {
                if (top.Color == CardColors.Green || top.Color == CardColors.Yellow
                && c.Color == CardColors.Green || c.Color == CardColors.Yellow)
                {
                    return (true, 10);
                }
            }
            return (null, -1);
        }
    }
}