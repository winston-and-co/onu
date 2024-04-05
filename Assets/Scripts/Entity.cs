using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Entity : MonoBehaviour
{
	public Deck deck;
	public Hand hand;

	public int maxHP;
	public int maxMana;

	public int hp;
	public int mana;

	public int startingHandSize;

	public string e_name;

	void Start()
	{
		hp = maxHP;
		mana = maxMana;
	}

	public void Damage(int amount)
	{
		hp -= amount;
		hp = Math.Max(0, hp);
		var bus = BattleEventBus.getInstance();
		bus.entityDamageEvent.Invoke(this, amount);
		bus.entityHealthChangedEvent.Invoke(this, amount);
	}

	public void Heal(int amount)
	{
		hp += amount;
		hp = Math.Min(maxHP, hp);
		var bus = BattleEventBus.getInstance();
		bus.entityHealEvent.Invoke(this, amount);
		bus.entityHealthChangedEvent.Invoke(this, amount);
	}

	public void SpendMana(int amount)
	{
		mana -= amount;
		mana = Math.Max(0, mana);
		BattleEventBus.getInstance().entityManaSpentEvent.Invoke(this, amount);
		BattleEventBus.getInstance().entityManaChangedEvent.Invoke(this, amount);
	}

	public void RestoreMana(int amount)
	{
		mana += amount;
		mana = Math.Min(maxMana, mana);
		BattleEventBus.getInstance().entityManaChangedEvent.Invoke(this, amount);
	}

	public Card Draw()
	{
		if (GameRules.getInstance().CanDraw(this))
		{
			Card drawn = deck.Draw();
			hand.AddCard(drawn);
			BattleEventBus.getInstance().cardDrawEvent.Invoke(this, drawn);
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
		BattleEventBus.getInstance().entityRefreshEvent.Invoke(this);
	}

}
