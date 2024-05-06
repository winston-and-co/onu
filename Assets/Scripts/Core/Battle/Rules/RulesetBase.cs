using UnityEngine;

public abstract class RulesetBase : IRuleset
{
    public abstract string Name { get; }

    public virtual RuleResult<bool> ColorsMatch(GameMaster gm, Entity e, Color color, Color target, int depth)
    {
        return (default, -1);
    }

    public virtual RuleResult<bool> CardIsPlayable(GameMaster gm, Entity e, Playable c)
    {
        return (default, -1);
    }

    public virtual RuleResult<int> CardManaCost(GameMaster gm, Entity e, Playable c)
    {
        return (default, -1);
    }

    public virtual RuleResult<bool> CanDraw(GameMaster gm, Entity e)
    {
        return (default, -1);
    }
}
