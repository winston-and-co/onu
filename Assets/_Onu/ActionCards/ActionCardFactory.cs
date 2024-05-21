using System;
using ActionCards;

public class ActionCardFactory
{
    public static AbstractActionCard MakeActionCard(ActionCard actionCard)
    {
        return actionCard switch
        {
            ActionCard.Wild => Wild.New(),
            ActionCard.Draw2 => Draw2.New(),
            _ => throw new ArgumentException(),
        };
    }

    public static AbstractActionCard MakeActionCard(string actionCardName)
    {
        return MakeActionCard((ActionCard)Enum.Parse(typeof(ActionCard), actionCardName));
    }

    public static AbstractActionCard MakeActionCard(int id)
    {
        return MakeActionCard((ActionCard)id);
    }

    public static AbstractActionCard MakeRandom()
    {
        var values = Enum.GetValues(typeof(ActionCard));
        return MakeActionCard((ActionCard)values.GetValue(UnityEngine.Random.Range(0, values.Length)));
    }

    public static bool CheckExists(string actionCardName)
    {
        return Enum.IsDefined(typeof(ActionCard), actionCardName);
    }

    public static bool CheckExists(int id)
    {
        return Enum.IsDefined(typeof(ActionCard), id);
    }
}

public enum ActionCard
{
    Wild,
    Draw2,
}