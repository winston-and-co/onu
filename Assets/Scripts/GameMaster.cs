using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameMaster : MonoBehaviour
{
	[SerializeField] Deck playerDeck;
	[SerializeField] Deck enemyDeck;
	[SerializeField] Pile discard;
	[SerializeField] Hand playerHand;

	[SerializeField] Hand enemyHand;
	[SerializeField] Entity player;
	[SerializeField] Entity enemy;

    int turn = 0;
    Entity[] order;
    Entity current_turn_entity;

    float damage_accum = 0;
    float mana_accum = 0;

    void Start()
    {
        BattleEventBus.getInstance().tryPlayEvent.AddListener(OnTryPlay);
        BattleEventBus.getInstance().cardTryDrawEvent.AddListener(OnTryDraw);
        BattleEventBus.getInstance().tryEndTurnEvent.AddListener(TryEndTurn);

        order = new Entity[2];
        order[0] = player;
        order[1] = enemy;

        StartTurn(0); // player
    }
    void StartNextTurn()
    {
        turn = (turn + 1) % order.Length;
        StartTurn(turn);
	}

    void StartTurn(int turn)
    {
        damage_accum = 0;
        mana_accum = 0;
        current_turn_entity = order[turn];
        BattleEventBus.getInstance().startTurnEvent.Invoke(current_turn_entity);
    }

    void TryEndTurn(Entity e)
    {

        //if(IsEntityTurn(e))
        //{

            Entity attack_entity = order[(turn + 1) % order.Length];
            attack_entity.Damage(damage_accum);
            e.SpendMana(mana_accum);
            BattleEventBus.getInstance().entityManaSpentEvent.Invoke(e, damage_accum);

            BattleEventBus.getInstance().endTurnEvent.Invoke(current_turn_entity);
            StartNextTurn();

        //}
    }

    /*
     * Validate card being played
     */
    void OnTryPlay(Entity e, Card c)
    {
        bool turn = IsEntityTurn(e);
        bool flag = false;
        // TODO: real life logic
        if (turn)
        {
            flag = GameRules.getInstance().TryPlay(discard, c);
        }

        
        if(flag)
        {
            PlayCard(e, c);
        } else
        {
            BattleEventBus.getInstance().cardIllegalEvent.Invoke(e, c);
        }

    }

    /*
     * Validate card being drawn
     */

    void OnTryDraw(Entity e, Card c)
    {
        Hand h = e.gameObject.GetComponentInChildren<Hand>();
        bool flag = false;
        if (h != null)
        {
            flag = GameRules.getInstance().TryDraw(h);
        }

        if (flag)
        {
            BattleEventBus.getInstance().cardDrawEvent.Invoke(e, c);
        }
        else
        {
            BattleEventBus.getInstance().cardNoDrawEvent.Invoke(e, c);
        }

    }
    bool IsEntityTurn(Entity e)
    {
        return e == current_turn_entity;
    }

    void PlayCard(Entity e, Card c)
    {
        Entity attack_entity;
        if(e.e_name.ToLower().Equals("player"))
        {
            attack_entity = enemy;
        } else
        {
            attack_entity = player;
        }

        damage_accum += c.value;
        mana_accum += c.value;
        
        BattleEventBus.getInstance().cardPlayedEvent.Invoke(e, c);
    }


}
