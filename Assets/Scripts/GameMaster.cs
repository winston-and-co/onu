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

        player.deck = playerDeck;
        player.hand = playerHand;
        enemy.deck = enemyDeck;
        enemy.hand = enemyHand;

        // Init combat
        order = new Entity[2];
        order[0] = player;
        order[1] = enemy;

        player.hp = player.maxHP;
        player.mana = player.maxMana;
        enemy.hp = enemy.maxHP;
        enemy.mana = enemy.maxMana;

        playerDeck.Shuffle();
        enemyDeck.Shuffle();
        for (int i = 0; i < player.startingHandSize; i++)
        {
            playerDeck.TryDraw();
        }
        for (int i = 0; i < enemy.startingHandSize; i++)
        {
            enemyDeck.TryDraw();
        }

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
        bool flag = false;
        // TODO: real life logic
        if (turn)
        {
            flag = GameRules.getInstance().TryPlay(discard, c);
        }

        if (flag)
        {
            PlayCard(e, c);
        }
        else
        {
            BattleEventBus.getInstance().cardIllegalEvent.Invoke(e, c);
        }

    }

    /*
     * Validate card being drawn
     */
    void OnTryDraw(Entity e, Card c)
    {
        bool canDraw = GameRules.getInstance().CanDraw(this, e);

        if (canDraw)
        {
            DrawCard(e, c);
        }
    }
    bool IsEntityTurn(Entity e)
    {
        return e == current_turn_entity;
    }

    void DrawCard(Entity e, Card c)
    {
        Card drawn = e.deck.Draw();
        e.hand.AddCard(drawn);
        BattleEventBus.getInstance().cardDrawEvent.Invoke(e, c);
    }

    void PlayCard(Entity e, Card c)
    {
        Entity target;
        if (e.e_name.ToLower().Equals("player"))
        {
            target = enemy;
        }
        else
        {
            target = player;
        }

        if (discard.Peek() != null && discard.Peek().color != c.color)
        {
            e.SpendMana(c.value);
        }
        target.Damage(c.value);
        e.hand.RemoveCard(c);
        BattleEventBus.getInstance().cardPlayedEvent.Invoke(e, c);

        if (e.hand.GetCardCount() == 0)
        {
            Refresh(e);
        }
    }

    void Refresh(Entity e)
    {
        e.Refresh();
        BattleEventBus.getInstance().entityRefreshEvent.Invoke(e);
    }
}
