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

    public bool CardIsPlayable(Entity e, Playable c)
    {
        if (e == null || c == null) return false;

        // must be their turn
        if (e != gm.current_turn_entity) return false;

        var discard = gm.discard;
        var topCard = discard.Peek();
        // first card
        if (topCard == null) return true;
        // colorless cards are always playable, and you can play anything on them
        if (topCard.Color == CardColors.Colorless || c.Color == CardColors.Colorless) return true;
        // matching color
        if (c.Color == topCard.Color) return true;
        // matching value only
        if (c.Value == topCard.Value)
        {
            if (e.mana >= c.Value)
            {
                return true;
            }
            return false;
        }
        return false;
    }

    public bool CanDraw(Entity e)
    {
        // there may be effects preventing draw
        return true;
    }
}
