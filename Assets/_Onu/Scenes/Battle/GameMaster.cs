using Cards;
using ActionCards;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private static GameMaster INSTANCE;
    public static GameMaster GetInstance() => INSTANCE;

    public Pile DiscardPile;
    public AbstractEntity Player;
    public AbstractEntity Enemy;

    int turn = 0;
    AbstractEntity[] order;
    public AbstractEntity CurrentEntity;

    public int TurnNumber;

    public AbstractEntity Victor;

    void Awake()
    {
        // https://gamedevbeginner.com/singletons-in-unity-the-right-way/
        if (INSTANCE != null && INSTANCE != this)
        {
            Destroy(this);
            return;
        }
        else INSTANCE = this;

        if (DiscardPile == null)
        {
            DiscardPile = FindObjectOfType<Pile>();
        }

        EventQueue eq = EventQueue.GetInstance();
        eq.cardTryPlayedEvent.AddListener(OnTryPlayCard);
        eq.actionCardTryUseEvent.AddListener(OnTryUseActionCard);
        eq.tryEndTurnEvent.AddListener(TryEndTurn);
        eq.entityHealthChangedEvent.AddListener(OnEntityHealthChanged);
    }

    void Start()
    {
        // Init combat
        order = new AbstractEntity[2];
        order[0] = Player;
        order[1] = Enemy;

        Player.mana = Player.maxMana;
        Enemy.hp = Enemy.maxHP;
        Enemy.mana = Enemy.maxMana;

        TurnNumber = 0;
        Victor = null;

        // Begin combat
        EventQueue.GetInstance().startBattleEvent.AddToBack(this);
        Player.deck.Shuffle();
        for (int i = 0; i < Player.startingHandSize; i++)
        {
            Player.Draw();
        }
        Enemy.deck.Shuffle();
        for (int i = 0; i < Enemy.startingHandSize; i++)
        {
            Enemy.Draw();
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
        CurrentEntity = order[turn];

        if (CurrentEntity == Player)
        {
            TurnNumber++;
        }

        EventQueue.GetInstance().startTurnEvent.AddToBack(CurrentEntity);

        // if your hand contains no playable cards
        //   draw cards until you draw a playable one
        // else
        //   draw one card
        bool hasPlayable = false;
        foreach (AbstractCard c in CurrentEntity.hand.hand)
        {
            if (CurrentEntity.gameRulesController.CardIsPlayable(this, CurrentEntity, c))
            {
                hasPlayable = true;
                break;
            }
        }
        do
        {
            var cardDrawn = CurrentEntity.Draw();
            if (CurrentEntity.gameRulesController.CardIsPlayable(this, CurrentEntity, cardDrawn))
            {
                hasPlayable = true;
            }
        }
        while (!hasPlayable);
    }

    void TryEndTurn(AbstractEntity e)
    {
        if (Blockers.UIPopupBlocker.IsBlocked()) return;
        EventQueue.GetInstance().endTurnEvent.AddToBack(CurrentEntity);
        StartNextTurn();
    }

    /*
     * Validate card being played
     */
    void OnTryPlayCard(AbstractEntity e, AbstractCard card)
    {
        if (Blockers.UIPopupBlocker.IsBlocked()) return;
        if (e == null || card == null) return;
        if (e != CurrentEntity) return;
        if (card.IsPlayable())
        {
            PlayCard(e, card);
        }
    }

    void PlayCard(AbstractEntity e, AbstractCard c)
    {
        AbstractEntity target = (e == Player) ? Enemy : Player;
        int cost = e.gameRulesController.CardManaCost(this, e, c);
        EventQueue.GetInstance().cardPlayedEvent.AddToBack(e, c);
        e.SpendMana(cost);
        AbstractCard top = DiscardPile.Peek();
        if (top != null && top.Value == 0)
        {
            e.Heal(c.Value ?? 0);
        }
        else
        {
            target.Damage(c.Value ?? 0);
        }
        e.hand.RemoveCard(c);
    }

    void OnTryUseActionCard(AbstractEntity e, AbstractActionCard ac)
    {
        if (Blockers.UIPopupBlocker.IsBlocked()) return;
        if (ac.IsUsable())
        {
            UseActionCard(e, ac);
        }
    }

    void UseActionCard(AbstractEntity e, AbstractActionCard ac)
    {
        ac.Use(() =>
        {
            EventQueue.GetInstance().actionCardUsedEvent.AddToBack(e, ac);
            PlayerData.GetInstance().RemoveActionCard(ac);
        });
    }

    void OnEntityHealthChanged(AbstractEntity _, int __)
    {
        CheckVictory();
    }

    void CheckVictory()
    {
        if (Player.hp <= 0)
        {
            Victor = Enemy;
        }
        else if (Enemy.hp <= 0)
        {
            Victor = Player;
        }
        else
        {
            return;
        }
        EventQueue.GetInstance().endTurnEvent.AddToBack(CurrentEntity);
        CurrentEntity = null;
        EventQueue.GetInstance().endBattleEvent.AddToBack(this);
    }

    public bool PlayerWon()
    {
        return Victor == Player;
    }
}
