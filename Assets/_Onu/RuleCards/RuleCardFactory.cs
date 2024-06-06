using System;
using System.Collections.Generic;
using System.Linq;
using RuleCards;

public class RuleCardFactory
{
    readonly static Dictionary<int, Func<AbstractRuleCard>> registeredRuleCards = new()
    {
        {Purple.Id, Purple.New},
        {Teal.Id, Teal.New},
        {Chartreuse.Id, Chartreuse.New},
        {Orange.Id, Orange.New},
        {Traditionalist.Id, Traditionalist.New},
        {Copycat.Id, Copycat.New},
        {Echo.Id, Echo.New},
        {Soda.Id, Soda.New},
        {ShrinkRay.Id, ShrinkRay.New},
    };

    public static AbstractRuleCard MakeRuleCard(int id)
    {
        return registeredRuleCards[id]();
    }

    /// <summary>
    /// Makes a random Rule Card, returning the instance.
    /// </summary>
    /// <returns>
    /// The Rule Card instance
    /// </returns>
    public static AbstractRuleCard MakeRandom()
    {
        int id = GetRandomId();
        return MakeRuleCard(id);
    }

    readonly static Random rand = new();
    public static int GetRandomId()
    {
        var idx = rand.Next(registeredRuleCards.Count);
        return registeredRuleCards.Keys.ToArray()[idx];
    }

    public static bool CheckExists(int id)
    {
        return registeredRuleCards.ContainsKey(id);
    }
}
