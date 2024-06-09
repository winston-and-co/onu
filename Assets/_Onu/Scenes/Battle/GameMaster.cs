using Cards;
using ActionCards;
using UnityEngine;
using System.Collections;

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

    [SerializeField] GameObject victoryPanel;
    [SerializeField] GameObject defeatPanel;

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

        victoryPanel.SetActive(true);
        defeatPanel.SetActive(true);
    }

    IEnumerator Start()
    {
        Blockers.GameBlocker.StartBlocking();
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
        EventManager.startedBattleEvent.AddToBack();
        Player.deck.Shuffle();
        Coroutine a = StartCoroutine(Player.DrawMany(Player.StartingHandSize));
        Enemy.deck.Shuffle();
        Coroutine b = StartCoroutine(Enemy.DrawMany(Enemy.StartingHandSize));
        yield return a;
        yield return b;

        Tutorial.Instance.FirstBattleTutorial();

        Blockers.GameBlocker.StopBlocking();
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
        EventManager.startedTurnEvent.AddToBack(CurrentEntity);

        // Check if hand already contains playable cards
        bool hasPlayable = false;
        foreach (AbstractCard c in CurrentEntity.hand.Cards)
        {
            if (CurrentEntity.gameRulesController.CardIsPlayable(this, CurrentEntity, c))
            {
                hasPlayable = true;
                break;
            }
        }
        // Draw TurnStartNumCardsToDraw
        for (int i = 0; i < CurrentEntity.TurnStartNumCardsToDraw; i++)
        {
            var cardDrawn = CurrentEntity.Draw();
            if (CurrentEntity.gameRulesController.CardIsPlayable(this, CurrentEntity, cardDrawn))
            {
                hasPlayable = true;
            }
        }
        // Keep drawing cards if TurnStartDrawUntilPlayable and no playable card drawn yet
        while (!hasPlayable && CurrentEntity.TurnStartDrawUntilPlayable)
        {
            var cardDrawn = CurrentEntity.Draw();
            if (CurrentEntity.gameRulesController.CardIsPlayable(this, CurrentEntity, cardDrawn))
            {
                hasPlayable = true;
            }
        }
    }

    void TryEndTurn(AbstractEntity e)
    {
        if (Blockers.GameBlocker.IsBlocked()) return;
        EventManager.endedTurnEvent.AddToBack(CurrentEntity);
        StartNextTurn();
    }

    /*
     * Validate card being played
     */
    public void TryPlayCard(AbstractCard card)
    {
        if (Blockers.GameBlocker.IsBlocked()) return;
        if (card.Entity == null || card == null) return;
        if (card.Entity != CurrentEntity) return;
        if (card.IsPlayable())
        {
            PlayCard(card);
        }
    }

    bool _firstSpentManaFlag = false;
    void PlayCard(AbstractCard c)
    {
        AbstractEntity cardPlayer = c.Entity;
        AbstractEntity cardTarget = (cardPlayer == Player) ? Enemy : Player;
        int cost = cardPlayer.gameRulesController.CardManaCost(this, cardPlayer, c);
        EventManager.cardPlayedEvent.AddToBack(cardPlayer, c);
        cardPlayer.SpendMana(cost);
        if (cardPlayer.isPlayer && cost > 0 && !_firstSpentManaFlag)
        {
            _firstSpentManaFlag = true;
            Tutorial.Instance.ManaTutorial();
        }
        AbstractCard top = DiscardPile.Peek();
        if (c.Value.HasValue)
        {
            if (top != null && top.Value == 0)
            {
                cardPlayer.Heal(c.Value ?? default);
            }
            else
            {
                int modifiedAmount = (int)((c.Value ?? default) * cardPlayer.DamageDealingModifier);
                cardTarget.Damage(modifiedAmount);
            }
        }
        cardPlayer.hand.RemoveCard(c);
    }

    public void TryUseActionCard(AbstractActionCard ac)
    {
        if (Blockers.GameBlocker.IsBlocked()) return;
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
        EventManager.endedTurnEvent.AddToBack(CurrentEntity);
        CurrentEntity = null;
        EventManager.endedBattleEvent.AddToBack();
    }

    public bool PlayerWon()
    {
        return Victor == Player;
    }
}
