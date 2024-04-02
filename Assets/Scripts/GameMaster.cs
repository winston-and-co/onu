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
        current_turn_entity = order[turn];
        BattleEventBus.getInstance().startTurnEvent.Invoke(current_turn_entity);
    }

    void TryEndTurn(Entity e)
    {
        BattleEventBus.getInstance().endTurnEvent.Invoke(current_turn_entity);
        StartNextTurn();
    }

    /*
     * Validate card being played
     */
    void OnTryPlay(Entity e, Card c)
    {
        bool turn = IsEntityTurn(e);
        // TODO: real life logic
        if (turn)
        {
            if(discard.Peek() == null)
            {
                PlayCard(e, c);
                return;
            }
            if(c.value == discard.Peek().value || c.color == discard.Peek().color)
            {
                PlayCard(e, c);
                return;
            }
        }
        BattleEventBus.getInstance().cardIllegalEvent.Invoke(e, c);
        
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
        
        float dmg = c.value;
        
        attack_entity.Damage(dmg);
        if(discard.Peek()?.color != c.color)
        {
            e.SpendMana(dmg);
            BattleEventBus.getInstance().entityManaSpentEvent.Invoke(e, dmg);
        }

        BattleEventBus.getInstance().cardPlayedEvent.Invoke(e, c);
    }

    void OnTryDraw(Entity e, Card c)
    {
        if (e.e_name.ToLower().Equals("player"))
        {
            if (playerHand.GetCardCount() < 7) // logic
            {
                BattleEventBus.getInstance().cardDrawEvent.Invoke(e, c);
            }
            else
            {
                BattleEventBus.getInstance().cardNoDrawEvent.Invoke(e, c);
            }

        } else
        {
            if(enemyHand.GetCardCount() < 7)
            {
                BattleEventBus.getInstance().cardDrawEvent.Invoke(e, c);
            }
        }
    }

	void OnPlayerDraw(Card c)
	{
		player.SpendMana(c.value);
	}
}
