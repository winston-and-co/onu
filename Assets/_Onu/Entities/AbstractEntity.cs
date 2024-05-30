using System;
using UnityEngine;
using Cards;
using RuleCards;

public abstract class AbstractEntity : MonoBehaviour
{
    public bool isPlayer;
    public GameRulesController gameRulesController;
    public Deck deck;
    public Hand hand;

    public int maxHP;
    public int maxMana;

    public int hp;
    public int mana;

    public int startingHandSize;

    public string e_name;

    public bool skipped;

    public static AbstractEntity New(string name, string entityName, System.Type entityType, int maxHP, int maxMana, int startingHandSize, bool isPlayer)
    {
        PrefabHelper ph = PrefabHelper.GetInstance();
        GameObject go = ph.GetInstantiatedPrefab(PrefabType.Entity);
        go.name = name;
        if (go == null) throw new System.Exception("Failed to instantiate entity prefab");
        AbstractEntity entity = go.AddComponent(entityType) as AbstractEntity;
        entity.e_name = entityName;
        // BASE STATS
        entity.maxHP = maxHP;
        entity.hp = entity.maxHP;
        entity.maxMana = maxMana;
        entity.mana = entity.maxMana;
        entity.startingHandSize = startingHandSize;
        entity.isPlayer = isPlayer;
        // RULE CARDS
        entity.gameRulesController = new(entity);
        entity.gameRulesController.AddRuleCard(DefaultRuleset.New());
        // HAND
        Hand hand = entity.GetComponentInChildren<Hand>();
        hand.e = entity;
        entity.hand = hand;
        return entity;
    }

    void Awake()
    {
        EventManager.cardPlayedEvent.AddListener(OnCardPlayed);
    }

    void Start()
    {
        hp = maxHP;
        mana = maxMana;
    }

    void OnCardPlayed(AbstractEntity e, AbstractCard _)
    {
        if (e != this) return;
        if (hand.GetCardCount() == 0)
        {
            Refresh();
        }
    }

    public void Damage(int amount)
    {
        hp -= amount;
        hp = Math.Max(0, hp);
        EventManager.entityHealthChangedEvent.AddToBack(this, -amount);
    }

    public void Heal(int amount)
    {
        if (amount == 0) return;
        hp += amount;
        hp = Math.Min(maxHP, hp);
        EventManager.entityHealthChangedEvent.AddToBack(this, amount);
    }

    public void ChangeMaxHP(int amount)
    {
        maxHP += amount;
        EventManager.entityMaxHealthChangedEvent.AddToBack(this, amount);
        Heal(amount);
    }

    public void SpendMana(int amount)
    {
        if (amount == 0) return;
        mana -= amount;
        mana = Math.Max(0, mana);
        EventManager.entityManaChangedEvent.AddToBack(this, -amount);
    }

    public void RestoreMana(int amount)
    {
        mana += amount;
        mana = Math.Min(maxMana, mana);
        EventManager.entityManaChangedEvent.AddToBack(this, amount);
    }

    public void ChangeMaxMana(int amount)
    {
        maxMana += amount;
        EventManager.entityMaxManaChangedEvent.AddToBack(this, amount);
        RestoreMana(amount);
    }

    /// <summary>
    /// Draw a card.
    /// </summary>
    /// <returns>The card drawn or <c>null</c> if this entity cannot draw.</returns>
    public AbstractCard Draw()
    {
        if (gameRulesController.CanDraw(GameMaster.GetInstance(), this))
        {
            AbstractCard drawn = deck.Draw();
            hand.AddCard(drawn);
            EventManager.cardDrawnEvent.AddToBack(this, drawn);
            return drawn;
        }
        return null;
    }

    public void Refresh()
    {
        RestoreMana(maxMana - mana);
        deck.Shuffle();
        for (int i = 0; i < startingHandSize; i++)
        {
            Draw();
        }
        EventManager.entityRefreshEvent.AddToBack(this);
    }

    /// <summary>
    /// Skips this entity's next turn.
    /// </summary>
    public void SkipTurn()
    {
        skipped = true;
    }
}
