using UnityEngine;
using Cards;

public abstract class RulesetBase : IRuleset
{
    public abstract string Name { get; }

    public virtual RuleResult<bool> ColorsMatch(GameMaster gm, AbstractEntity e, Color color, Color target, int depth)
    {
        return (default, -1);
    }

    public virtual RuleResult<bool> CardIsPlayable(GameMaster gm, AbstractEntity e, AbstractCard c)
    {
        return (default, -1);
    }

    public virtual RuleResult<int> CardManaCost(GameMaster gm, AbstractEntity e, AbstractCard c)
    {
        return (default, -1);
    }

    public virtual RuleResult<bool> CanDraw(GameMaster gm, AbstractEntity e)
    {
        return (default, -1);
    }
}
