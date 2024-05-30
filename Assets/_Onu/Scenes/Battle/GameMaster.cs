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

    int turn;
    AbstractEntity[] order;
    public AbstractEntity CurrentEntity;
    public int TurnNumber;

    public AbstractEntity Victor;

    readonly GameEvent startTurnMsg = new();
    readonly GameEvent skipTurnMsg = new();

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

        EventManager.tryEndTurnEvent.AddListener(TryEndTurn);
        EventManager.entityHealthChangedEvent.AddListener(OnEntityHealthChanged);

        startTurnMsg.AddListener(StartTurn);
        skipTurnMsg.AddListener(SkipTurn);
    }

    void Start()
    {
        // Init combat
        order = new AbstractEntity[2];
        order[0] = Player;
        order[1] = Enemy;
        turn = 0;

        Player.mana = Player.maxMana;
        Enemy.hp = Enemy.maxHP;
        Enemy.mana = Enemy.maxMana;

        TurnNumber = 0;
        Victor = null;

        // Begin combat
        EventManager.startBattleEvent.AddToBack(this);
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

        StartNextTurn();
    }

    void StartNextTurn()
    {
        CurrentEntity = order[turn];
        turn = (turn + 1) % order.Length;

        if (CurrentEntity == Player)
        {
            TurnNumber++;
        }

        if (CurrentEntity.skipped)
        {
            skipTurnMsg.AddToBack();
        }
        else
        {
            startTurnMsg.AddToBack();
        }
    }

    void SkipTurn()
    {
        EventManager.skippedTurnEvent.AddToBack(CurrentEntity);
        StartNextTurn();
    }

    void StartTurn()
    {
        EventManager.startTurnEvent.AddToBack(CurrentEntity);

        // if your hand contains no playable cards
        //   draw cards until you draw a playable one
        // else
        //   draw one card
        bool hasPlayable = false;
        foreach (AbstractCard c in CurrentEntity.hand.Cards)
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
        EventManager.endTurnEvent.AddToBack(CurrentEntity);
        StartNextTurn();
    }

    /*
     * Validate card being played
     */
    public void TryPlayCard(AbstractCard card)
    {
        if (Blockers.UIPopupBlocker.IsBlocked()) return;
        if (card.Entity == null || card == null) return;
        if (card.Entity != CurrentEntity) return;
        if (card.IsPlayable())
        {
            PlayCard(card);
        }
    }

    void PlayCard(AbstractCard c)
    {
        AbstractEntity player = c.Entity;
        AbstractEntity target = (player == Player) ? Enemy : Player;
        int cost = player.gameRulesController.CardManaCost(this, player, c);
        EventManager.cardPlayedEvent.AddToBack(player, c);
        player.SpendMana(cost);
        AbstractCard top = DiscardPile.Peek();
        if (top != null && top.Value == 0)
        {
            player.Heal(c.Value ?? 0);
        }
        else
        {
            target.Damage(c.Value ?? 0);
        }
        player.hand.RemoveCard(c);
    }

    public void TryUseActionCard(AbstractActionCard ac)
    {
        if (Blockers.UIPopupBlocker.IsBlocked()) return;
        if (ac.IsUsable())
        {
            UseActionCard(ac);
        }
    }

    void UseActionCard(AbstractActionCard ac)
    {
        ac.Use(() =>
        {
            EventManager.actionCardUsedEvent.AddToBack(ac);
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
        EventManager.endTurnEvent.AddToBack(CurrentEntity);
        CurrentEntity = null;
        EventManager.endBattleEvent.AddToBack(this);
    }

    public bool PlayerWon()
    {
        return Victor == Player;
    }
}
