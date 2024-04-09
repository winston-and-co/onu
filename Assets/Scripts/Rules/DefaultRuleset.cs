public class DefaultRuleset : Ruleset
{
    public override RuleResult CardIsPlayable(GameMaster gm, Entity e, Playable c)
    {
        if (e == null || c == null) return (false, 0);

        // must be their turn
        if (e != gm.current_turn_entity) return (false, 0);

        var discard = gm.discard;
        var topCard = discard.Peek();
        // first card
        if (topCard == null) return (true, 0);
        // colorless cards are always playable, and you can play anything on them
        if (topCard.Color == CardColors.Colorless || c.Color == CardColors.Colorless) return (true, 0);
        // matching color
        if (c.Color == topCard.Color) return (true, 0);
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

    public override RuleResult CanDraw(GameMaster gm, Entity e)
    {
        // there may be effects preventing draw
        return (true, 0);
    }
}
