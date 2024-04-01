using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
	public float maxHP;
	public float maxMana;

	public float hp;
	public float mana;

	public String e_name;

    void Start()
    {
		hp = maxHP;
		mana = maxMana;
	}


	public void Damage(float damage)
	{
		hp -= damage;
		hp = Math.Max(0, hp);
        BattleEventBus.getInstance().entityDamageEvent.Invoke(this, damage);
    }

	public void SpendMana(float amount)
	{
		mana -= amount;

	}

	int GetMana()
	{
		throw new NotImplementedException();
	}

	int GetHP()
	{
		throw new NotImplementedException();
	}

	Deck GetDeck()
	{
		throw new NotImplementedException();
	}

	Hand GetHand()
	{
		throw new NotImplementedException();
	}
}
