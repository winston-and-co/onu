/// <summary>
/// Defines rules that other implementations of Ruleset can override. Returned
/// precedence is the order in which the rules will be checked by
/// GameRulesController. Null results are passed to the next rule/lower lower
/// precedence.
/// <br/><br/>
/// E.g.<br/>
/// GameRulesController.rules contains:<br/>
///   ClassA.CanDraw => (true, 1)<br/>
///   ClassB.CanDraw => (null, 3)<br/>
///   ClassC.CanDraw => (false, 0)<br/>
/// <br/>
/// Since ClassB returned with the highest precedence, it is checked first.
/// However, since it returned null, the next highest precedence is used
/// (ClassA). ClassA returned true, so GameRulesController.CanDraw returns true.
/// </summary>
public class Ruleset
{
    public virtual RuleResult CardIsPlayable(GameMaster gm, Entity e, Playable c)
    {
        return (false, -1);
    }
    public virtual RuleResult CanDraw(GameMaster gm, Entity e)
    {
        return (false, -1);
    }
}

public struct RuleResult
{
    public bool? Result;
    public int Precedence;

    public RuleResult(bool? result, int precedence)
    {
        Result = result;
        Precedence = precedence;
    }

    public static implicit operator RuleResult((bool? b, int i) input) => new(input.b, input.i);
}
