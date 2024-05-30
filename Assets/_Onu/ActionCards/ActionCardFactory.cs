using System;
using System.Collections.Generic;
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
    };

    public static AbstractActionCard MakeActionCard(int id)
    {
        return registeredActionCards[id]();
    }

    public static AbstractActionCard MakeRandom()
    {
        return MakeActionCard(UnityEngine.Random.Range(0, registeredActionCards.Count));
    }

    public static bool CheckExists(int id)
    {
        return registeredActionCards.ContainsKey(id);
    }
}