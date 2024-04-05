using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    [SerializeField]
    private GameMaster gm;
    private static GameRules INSTANCE;
    public static GameRules getInstance()
    {
        return INSTANCE;
    }

    // https://gamedevbeginner.com/singletons-in-unity-the-right-way/
    void Awake()
    {
        if (INSTANCE != null && INSTANCE != this)
        {
            Destroy(this);
        }
        else
        {
            INSTANCE = this;
        }
    }

    public GameRules(GameMaster gm)
    {
        this.gm = gm;
    }

    public bool CardIsPlayable(Entity e, Card c)
    {
        var discard = gm.discard;
        var topCard = discard.Peek();
        if (topCard == null)
        {
            return true;
        }
        if (c.value == topCard.value)
        {
            // if changing color
            if (c.color != topCard.color)
            {
                if (e.mana >= c.value)
                {
                    return true;
                }
                return false;
            }
            return true;
        }
        if (c.color == topCard.color)
        {
            return true;
        }
        return false;
    }

    public bool CanDraw(Entity e)
    {
        // there may be effects preventing draw
        return true;
    }
}
