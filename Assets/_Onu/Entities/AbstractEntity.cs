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
        EventQueue.GetInstance().afterCardPlayedEvent.AddListener(AfterCardPlayed);
    }

    void Start()
    {
        hp = maxHP;
        mana = maxMana;
    }

    void AfterCardPlayed(AbstractEntity e, AbstractCard _)
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
        var bus = EventQueue.GetInstance();
        bus.entityDamageEvent.Invoke(this, amount);
        bus.entityHealthChangedEvent.Invoke(this, amount);
    }

    public void Heal(int amount)
    {
        if (amount == 0) return;
        hp += amount;
        hp = Math.Min(maxHP, hp);
        var bus = EventQueue.GetInstance();
        bus.entityHealEvent.Invoke(this, amount);
        bus.entityHealthChangedEvent.Invoke(this, amount);
    }

    public void SpendMana(int amount)
    {
        if (amount == 0) return;
        mana -= amount;
        mana = Math.Max(0, mana);
        EventQueue.GetInstance().entityManaSpentEvent.Invoke(this, amount);
        EventQueue.GetInstance().entityManaChangedEvent.Invoke(this, amount);
    }

    public void RestoreMana(int amount)
    {
        mana += amount;
        mana = Math.Min(maxMana, mana);
        EventQueue.GetInstance().entityManaChangedEvent.Invoke(this, amount);
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
            EventQueue.GetInstance().cardDrawEvent.Invoke(this, drawn);
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
        EventQueue.GetInstance().entityRefreshEvent.Invoke(this);
    }
}
