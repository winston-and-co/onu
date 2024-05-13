using System;
using ActionCards;

public class ActionCardHelper
{
    public static AbstractActionCard CreateActionCard(ActionCard actionCard)
    {
        return actionCard switch
        {
            ActionCard.Wild => Wild.New(),
            _ => throw new ArgumentException(),
        };
    }

    public static AbstractActionCard CreateActionCard(string actionCardName)
    {
        return CreateActionCard((ActionCard)Enum.Parse(typeof(ActionCard), actionCardName));
    }

    public static bool CheckExists(string actionCardName)
    {
        return Enum.TryParse<ActionCard>(actionCardName, out _);
    }
}

public enum ActionCard
{
    Wild
}