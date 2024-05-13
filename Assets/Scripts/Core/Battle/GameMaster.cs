using System;
using System.Collections;
using System.Collections.Generic;
using Cards;
using ActionCards;
using UnityEngine;
using UnityEngine.Events;

public class GameMaster : MonoBehaviour
{
    private static GameMaster INSTANCE;
    public static GameMaster GetInstance() => INSTANCE;

    public Pile discard;
    public AbstractEntity player;
    public AbstractEntity enemy;

    int turn = 0;
    AbstractEntity[] order;
    public AbstractEntity current_turn_entity;

    public int turnNumber;

    public AbstractEntity victor;

    bool cardNotResolved;

    void Awake()
    {
        // https://gamedevbeginner.com/singletons-in-unity-the-right-way/
        if (INSTANCE != null && INSTANCE != this)
        {
            Destroy(this);
            return;
        }
        else INSTANCE = this;

        BattleEventBus bus = BattleEventBus.getInstance();
        bus.cardTryPlayedEvent.AddListener(OnTryPlayCard);
        bus.actionCardTryUseEvent.AddListener(OnTryUseActionCard);
        bus.tryEndTurnEvent.AddListener(TryEndTurn);
        bus.entityDamageEvent.AddListener(OnEntityDamage);
    }

    void Start()
    {
        // Init combat
        order = new AbstractEntity[2];
        order[0] = player;
        order[1] = enemy;

        player.mana = player.maxMana;
        enemy.hp = enemy.maxHP;
        enemy.mana = enemy.maxMana;

        turnNumber = 0;
        victor = null;

        // Begin combat
        BattleEventBus.getInstance().startBattleEvent.Invoke(this);

        player.deck.Shuffle();
        for (int i = 0; i < player.startingHandSize; i++)
        {
            player.Draw();
        }
        enemy.deck.Shuffle();
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

        // if your hand contains no playable cards
        //   draw cards until you draw a playable one
        // else
        //   draw one card
        bool hasPlayable = false;
        foreach (AbstractCard c in current_turn_entity.hand.hand)
        {
            if (current_turn_entity.gameRules.CardIsPlayable(this, current_turn_entity, c))
            {
                hasPlayable = true;
                break;
            }
        }
        do
        {
            var cardDrawn = current_turn_entity.Draw();
            if (current_turn_entity.gameRules.CardIsPlayable(this, current_turn_entity, cardDrawn))
            {
                hasPlayable = true;
            }
        }
        while (!hasPlayable);
    }

    void TryEndTurn(AbstractEntity e)
    {
        if (Blockers.UIPopupBlocker.IsBlocked()) return;
        BattleEventBus.getInstance().endTurnEvent.Invoke(current_turn_entity);
        StartNextTurn();
    }

    bool IsEntityTurn(AbstractEntity e)
    {
        return e == current_turn_entity;
    }

    /*
     * Validate card being played
     */
    void OnTryPlayCard(AbstractEntity e, AbstractCard card)
    {
        if (Blockers.UIPopupBlocker.IsBlocked()) return;
        if (e == null || card == null) return;
        if (e != current_turn_entity) return;
        if (card.IsPlayable())
        {
            PlayCard(e, card);
        }
        else
        {
            BattleEventBus.getInstance().cardIllegalEvent.Invoke(e, card);
        }
    }

    void PlayCard(AbstractEntity e, AbstractCard c)
    {
        cardNotResolved = true;
        AbstractEntity target;
        if (e == player)
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
            case AbstractCard top:
                if (!c.Value.HasValue) goto default;

                BattleEventBus.getInstance().cardPlayedEvent.Invoke(e, c);
                e.SpendMana(cost);
                if (top.Value == 0)
                    e.Heal(c.Value ?? 0);
                else
                    target.Damage(c.Value ?? 0);

                e.hand.RemoveCard(c);
                break;
            case null:
            default:
                BattleEventBus.getInstance().cardPlayedEvent.Invoke(e, c);
                target.Damage(c.Value ?? 0);
                e.hand.RemoveCard(c);
                break;
        }

        BattleEventBus.getInstance().afterCardPlayedEvent.Invoke(e, c);
        cardNotResolved = false;
        CheckVictory();
    }

    void OnTryUseActionCard(AbstractEntity e, AbstractActionCard ac)
    {
        if (Blockers.UIPopupBlocker.IsBlocked()) return;
        if (ac is IUsable usable)
        {
            if (usable.IsUsable())
            {
                UseActionCard(e, ac);
            }
        }
    }

    void UseActionCard(AbstractEntity e, AbstractActionCard ac)
    {
        if (ac is IUsable usable)
        {
            usable.Use();
            BattleEventBus.getInstance().actionCardUsedEvent.Invoke(e, ac);
            // TODO: Move this to an event handler in PlayerData if a non-battle event bus is created
            PlayerData.GetInstance().RemoveActionCardAt(ac.PlayerDataIndex);
        }
    }

    void OnEntityDamage(AbstractEntity e, int _)
    {
        CheckVictory();
    }

    void CheckVictory()
    {
        if (cardNotResolved) return;
        if (player.hp <= 0)
        {
            victor = enemy;
        }
        else if (enemy.hp <= 0)
        {
            victor = player;
        }
        else
        {
            return;
        }
        BattleEventBus.getInstance().endTurnEvent.Invoke(current_turn_entity);
        current_turn_entity = null;
        BattleEventBus.getInstance().endBattleEvent.Invoke(this);
    }

    public bool PlayerWon()
    {
        return victor == player;
    }
}
