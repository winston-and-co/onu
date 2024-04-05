using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameMaster : MonoBehaviour
{
    public Pile discard;
    public Entity player;
    [SerializeField] Deck playerDeck;
    [SerializeField] Hand playerHand;
    public Entity enemy;
    [SerializeField] Deck enemyDeck;
    [SerializeField] Hand enemyHand;

    int turn = 0;
    Entity[] order;
    Entity current_turn_entity;

    public int turnNumber;

    public Entity victor;

    void Awake()
    {
        BattleEventBus bus = BattleEventBus.getInstance();
        bus.tryPlayEvent.AddListener(OnTryPlay);
        bus.tryEndTurnEvent.AddListener(TryEndTurn);
        bus.entityDamageEvent.AddListener(CheckVictory);
    }

    void Start()
    {
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

        turnNumber = 0;
        victor = null;

        // Begin combat
        BattleEventBus.getInstance().startBattleEvent.Invoke(this);

        playerDeck.Shuffle();
        for (int i = 0; i < player.startingHandSize; i++)
        {
            player.Draw();
        }
        enemyDeck.Shuffle();
        for (int i = 0; i < enemy.startingHandSize; i++)
        {
            enemy.Draw();
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

        if (current_turn_entity == player)
        {
            turnNumber++;
        }

        BattleEventBus.getInstance().startTurnEvent.Invoke(current_turn_entity);

        // Draw cards until you draw a playable one
        // Might change to just draw one at start of turn
        bool hasPlayable = false;
        foreach (Card c in current_turn_entity.hand.hand)
        {
            if (GameRules.getInstance().CardIsPlayable(current_turn_entity, c))
            {
                hasPlayable = true;
                break;
            }
        }
        if (!hasPlayable)
        {
            Card cardDrawn;
            do
            {
                cardDrawn = current_turn_entity.Draw();
            }
            while (!GameRules.getInstance().CardIsPlayable(current_turn_entity, cardDrawn));
        }
    }

    void TryEndTurn(Entity e)
    {
        BattleEventBus.getInstance().endTurnEvent.Invoke(current_turn_entity);
        StartNextTurn();
    }

    bool IsEntityTurn(Entity e)
    {
        return e == current_turn_entity;
    }

    /*
     * Validate card being played
     */
    void OnTryPlay(Entity e, Card c)
    {
        bool turn = IsEntityTurn(e);
        bool playable = GameRules.getInstance().CardIsPlayable(e, c);

        if (turn && playable)
        {
            PlayCard(e, c);
        }
        else
        {
            BattleEventBus.getInstance().cardIllegalEvent.Invoke(e, c);
        }

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
        if (discard.Peek() != null && discard.Peek().value == 0)
        {
            e.Heal(c.value);
        }
        else
        {
            target.Damage(c.value);
        }
        e.hand.RemoveCard(c);
        BattleEventBus.getInstance().cardPlayedEvent.Invoke(e, c);

        if (e.hand.GetCardCount() == 0)
        {
            e.Refresh();
        }
    }

    void CheckVictory(Entity e, int _)
    {
        if (e.hp <= 0)
        {
            if (e == player)
            {
                victor = enemy;
                BattleEventBus.getInstance().endBattleEvent.Invoke(this);
            }
            else if (e == enemy)
            {
                victor = player;
                BattleEventBus.getInstance().endBattleEvent.Invoke(this);
            }
        }
    }
}
