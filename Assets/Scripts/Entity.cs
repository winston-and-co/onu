using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Entity : MonoBehaviour
{
	public TMP_Text hpText;
	public TMP_Text manaText;

	public Deck deck;
	public Hand hand;

	public int maxHP;
	public int maxMana;

	public int hp;
	public int mana;

	public int startingHandSize;

	public String e_name;

	void Start()
	{
		hp = maxHP;
		mana = maxMana;

		hpText.SetText(hp.ToString() + " / " + maxHP.ToString());
		manaText.SetText(mana.ToString() + " / " + maxMana.ToString());
	}

	public void Damage(int damage)
	{
		hp -= damage;
		hp = Math.Max(0, hp);
		BattleEventBus.getInstance().entityDamageEvent.Invoke(this, damage);
		hpText.SetText(hp.ToString() + " / " + maxHP.ToString());
		manaText.SetText(mana.ToString() + " / " + maxMana.ToString());
	}

	public void SpendMana(int amount)
	{
		mana -= amount;
		BattleEventBus.getInstance().entityManaSpentEvent.Invoke(this, amount);
		hpText.SetText(hp.ToString() + " / " + maxHP.ToString());
		manaText.SetText(mana.ToString() + " / " + maxMana.ToString());
	}

	public void Refresh()
	{
		hp = maxHP;
		mana = maxMana;
		hpText.SetText(hp.ToString() + " / " + maxHP.ToString());
		manaText.SetText(mana.ToString() + " / " + maxMana.ToString());
		deck.Shuffle();
		for (int i = 0; i < startingHandSize; i++)
		{
			deck.Draw();
		}
	}

	int GetMana()
	{
		return mana;
	}

	int GetHP()
	{
		return hp;
	}

	Deck GetDeck()
	{
		return deck;
	}

	Hand GetHand()
	{
		return hand;
	}
}
