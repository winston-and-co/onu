using System;
using System.Collections;
using System.Collections.Generic;
using ActionCards;
using UnityEngine;
using UnityEngine.Events;

public class GameMaster : MonoBehaviour
{
    private static GameMaster INSTANCE;
    public static GameMaster GetInstance() => INSTANCE;

    public Pile discard;
    public Entity player;
    [SerializeField] Deck playerDeck;
    [SerializeField] Hand playerHand;
    private GameRulesController playerRules;
    public Entity enemy;
    [SerializeField] Deck enemyDeck;
    [SerializeField] Hand enemyHand;
    private GameRulesController enemyRules;

    int turn = 0;
    Entity[] order;
    public Entity current_turn_entity;

    public int turnNumber;

    public Entity victor;

    void Awake()
    {
        // https://gamedevbeginner.com/singletons-in-unity-the-right-way/
        if (INSTANCE != null && INSTANCE != this) Destroy(this);
        else INSTANCE = this;

        BattleEventBus bus = BattleEventBus.getInstance();
        bus.cardTryPlayedEvent.AddListener(OnTryPlayCard);
        bus.actionCardTryUseEvent.AddListener(OnTryUseActionCard);
        bus.tryEndTurnEvent.AddListener(TryEndTurn);
        bus.entityDamageEvent.AddListener(CheckVictory);
    }

    void Start()
    {
        player.gameRules = new();
        player.gameRules.Add(new RuleCards.Purple());
        player.deck = playerDeck;
        player.hand = playerHand;
        enemy.gameRules = new();
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
        foreach (Playable c in current_turn_entity.hand.hand)
        {
            if (current_turn_entity.gameRules.CardIsPlayable(this, current_turn_entity, c))
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
            while (!current_turn_entity.gameRules.CardIsPlayable(this, current_turn_entity, cardDrawn));
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
    void OnTryPlayCard(Entity e, Playable card)
    {
        if (card.IsPlayable())
        {
            PlayCard(e, card);
        }
        else
        {
            BattleEventBus.getInstance().cardIllegalEvent.Invoke(e, card);
        }
    }

    void PlayCard(Entity e, Playable c)
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
        int cost = e.gameRules.CardManaCost(this, e, c);
        switch (discard.Peek())
        {
            case Card top:
                if (c.Value.IsNull) goto default;

                e.SpendMana(cost);
                if (top.Value == 0)
                    e.Heal(c.Value.OrIfNull(0));
                else
                    target.Damage(c.Value.OrIfNull(0));

                e.hand.RemoveCard(c);
                BattleEventBus.getInstance().cardPlayedEvent.Invoke(e, c);
                break;
            case IActionCard:
            case null:
            default:
                target.Damage(c.Value.OrIfNull(0));
                e.hand.RemoveCard(c);
                BattleEventBus.getInstance().cardPlayedEvent.Invoke(e, c);
                break;
        }

        if (e.hand.GetCardCount() == 0)
        {
            e.Refresh();
        }
    }

    void OnTryUseActionCard(Entity e, IActionCard ac)
    {
        if (ac.IsUsable())
        {
            UseActionCard(e, ac);
        }
    }

    void UseActionCard(Entity e, IActionCard ac)
    {
        ac.Use();
        BattleEventBus.getInstance().actionCardUsedEvent.Invoke(e, ac);
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
