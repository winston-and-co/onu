using System;
using System.Collections.Generic;
using System.Linq;
using ActionCards;

public class ActionCardFactory
{
    readonly static Dictionary<int, Func<AbstractActionCard>> registeredActionCards = new()
    {
        {Wild.Id, Wild.New},
        {Draw2.Id, Draw2.New},
        {Skip.Id, Skip.New},
        {Heal.Id, Heal.New},
        {RestoreMana.Id, RestoreMana.New},
        {MaxHPUp.Id, MaxHPUp.New},
        {MaxManaUp.Id, MaxManaUp.New},
        {Generate2.Id, Generate2.New},
        {DoubleDamage.Id, DoubleDamage.New},
        {Randomize.Id, Randomize.New},
    };

    public static AbstractActionCard MakeActionCard(int id)
    {
        return registeredActionCards[id]();
    }

    /// <summary>
    /// Makes a random Action Card, returning the instance and its ID.
    /// </summary>
    /// <returns>
    /// A tuple containing the Action Card instance and its ID.
    /// </returns>
    public static (AbstractActionCard, int) MakeRandom()
    {
        int id = GetRandomId();
        return (MakeActionCard(id), id);
    }

    public static int GetRandomId()
    {
        var rand = new Random();
        var idx = rand.Next(registeredActionCards.Count);
        return registeredActionCards.Keys.ToArray()[idx];
    }

    public static bool CheckExists(int id)
    {
        return registeredActionCards.ContainsKey(id);
    }
}