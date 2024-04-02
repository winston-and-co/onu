using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameRules 
{
    public static GameRules INSTANCE = null;
    public static GameRules getInstance()
    {
        return INSTANCE ?? (INSTANCE = new GameRules());
    }


    public bool TryPlay(Pile discard, Card play)
    {
        if (discard.Peek() == null)
        {
            return true;
        }
        if (play.value == discard.Peek().value || play.color == discard.Peek().color)
        {
            return true;
        }
        return false;
    }

    public bool TryDraw(Hand hand)
    {
        if (hand.GetCardCount() < 7) // logic
        {
            return true;
        }
        return false;
    }
}
