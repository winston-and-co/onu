using System;
using RuleCards;

public class RuleCardFactory
{
    public static AbstractRuleCard MakeRuleCard(RuleCard ruleCard)
    {
        return ruleCard switch
        {
            RuleCard.Purple => Purple.New(),
            RuleCard.Teal => Teal.New(),
            RuleCard.Chartreuse => Chartreuse.New(),
            RuleCard.Orange => Orange.New(),
            _ => throw new ArgumentException(),
        };
    }

    public static AbstractRuleCard MakeRuleCard(string ruleCardName)
    {
        return MakeRuleCard((RuleCard)Enum.Parse(typeof(RuleCard), ruleCardName));
    }

    public static AbstractRuleCard MakeRuleCard(int id)
    {
        return MakeRuleCard((RuleCard)id);
    }

    public static AbstractRuleCard MakeRandom()
    {
        var values = Enum.GetValues(typeof(RuleCard));
        return MakeRuleCard((RuleCard)values.GetValue(UnityEngine.Random.Range(0, values.Length)));
    }

    public static bool CheckExists(string ruleCardName)
    {
        return Enum.IsDefined(typeof(RuleCard), ruleCardName);
    }

    public static bool CheckExists(int id)
    {
        return Enum.IsDefined(typeof(RuleCard), id);
    }
}

public enum RuleCard
{
    Purple,
    Teal,
    Chartreuse,
    Orange,
}