/// <summary>
/// Defines rules that other implementations of Ruleset can override. Returned
/// precedence is the order in which the rules will be checked by
/// GameRulesController.
/// <br/><br/>
/// E.g.<br/>
/// GameRulesController.rules contains:<br/>
///   ClassA.CanDraw => (true, 1)<br/>
///   ClassB.CanDraw => (true, -1)<br/>
///   ClassC.CanDraw => (false, 0)<br/>
/// <br/>
/// ClassA returned with highest precedence, so its return value is used.
/// </summary>
public class Ruleset
{
    /// <summary>
    /// Check whether a card is playable given current game state.
    /// </summary>
    /// <returns>
    /// <c>RuleResult</c> whether the card is playable.
    /// </returns>
    public virtual RuleResult<bool> CardIsPlayable(GameMaster gm, Entity e, Playable c)
    {
        return (false, -1);
    }

    /// <summary>
    /// Check the cost of playing a card given current game state.
    /// </summary>
    /// <param name="gm"></param>
    /// <param name="e"></param>
    /// <param name="c"></param>
    /// <returns>
    /// <c>RuleResult</c> cost of card if played.
    /// </returns>
    public virtual RuleResult<int> CardManaCost(GameMaster gm, Entity e, Playable c)
    {
        return (false, -1);
    }

    /// <summary>
    /// Check whether an entity can draw given current game state.
    /// </summary>
    /// <returns>
    /// <c>RuleResult</c> whether the entity can draw.
    /// </returns>
    public virtual RuleResult<bool> CanDraw(GameMaster gm, Entity e)
    {
        return (false, -1);
    }
}

/// <summary>
/// Returned by ruleset methods.
/// <br/><br/>
/// Precedence values:<br/>
/// &lt; 0: Guarantees result will not be used<br/>
/// 0 - 1000: Reserved for base game<br/>
/// 1000+: May be used for custom rules
/// </summary>
public struct RuleResult<T>
{
    public T Result;
    public int Precedence;

    public RuleResult(T result, int precedence)
    {
        Result = result;
        Precedence = precedence;
    }

    public static implicit operator RuleResult<T>((T b, int i) input) => new(input.b, input.i);
    public static implicit operator RuleResult<T>((object, int i) input) => new(default, input.i);
    public static implicit operator T(RuleResult<T> result) => result.Result;
}
