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

    // Start is called before the first frame update
    void Start()
    {
        BattleEventBus.getInstance().tryPlayEvent.AddListener(TryPlay);
        BattleEventBus.getInstance().cardTryDrawEvent.AddListener(OnTryDraw);
    }
    void StartTurn()
    {
	}
    void TryPlay(Entity e, Card c)
    {
        // TODO: real life logic
        // successful:
        if (true)
        {
            PlayCard(e, c,true);
        }
        else
        {
            BattleEventBus.getInstance().cardIllegalEvent.Invoke(e, c);
        }
    }

    void PlayCard(Entity e, Card c, bool wasPlayer)
    {
        if(e.e_name.ToLower().Equals("player"))
        {
            float dmg = c.value;
            enemy.Damage(dmg);
        }
        BattleEventBus.getInstance().cardPlayedEvent.Invoke(e,c);
    }

    void OnTryDraw(Entity e, Card c)
    {
        if(playerHand.GetCardCount() < 7) // logic
        {
            BattleEventBus.getInstance().cardDrawEvent.Invoke(e, c);
        } else
        {
            BattleEventBus.getInstance().cardNoDrawEvent.Invoke(e, c);
        }
    }

	void OnPlayerDraw(Card c)
	{
		player.SpendMana(c.value);
	}
}
