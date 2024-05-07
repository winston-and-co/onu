using System.Collections.Generic;
using UnityEngine;

public class DefaultRuleset : RulesetBase
{
    public override string Name => "DefaultRuleset";

    public override RuleResult<bool> ColorsMatch(GameMaster gm, Entity e, Color color, Color target, int depth)
    {
        return (color == target, 0);
    }

    public override RuleResult<bool> CardIsPlayable(GameMaster gm, Entity e, Playable c)
    {
        var discard = gm.discard;
        var topCard = discard.Peek();
        // first card
        if (topCard == null) return (true, 0);
        // colorless cards are always playable, and you can play anything on them
        if (topCard.Color == CardColors.Colorless || c.Color == CardColors.Colorless) return (true, 0);
        // matching color
        if (e.gameRules.ColorsMatch(gm, e, c.Color, topCard.Color))
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

    public override RuleResult<int> CardManaCost(GameMaster gm, Entity e, Playable c)
    {
        var topCard = gm.discard.Peek();
        if (topCard != null && !e.gameRules.ColorsMatch(gm, e, c.Color, topCard.Color))
        {
            return (c.Value, 0);
        }
        return (0, 0);
    }

    public override RuleResult<bool> CanDraw(GameMaster gm, Entity e)
    {
        // there may be effects preventing draw
        return (true, 0);
    }
}
