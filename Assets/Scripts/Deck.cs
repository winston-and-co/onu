using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
	[SerializeField] private List<Card> m_Cards;
	public GameObject card_prefab;
	public Entity e;

	Color[] colors = new Color[] {
		Color.red,
		Color.blue,
		Color.green,
		new Color(217, 195, 0),
	};

	public void Awake()
	{
		Generate();
		Shuffle();

		BattleEventBus.getInstance().cardDrawEvent.AddListener(OnConfirmDraw);
	}

	public void Generate()
	{
		for (int i = 0; i < 10; i++)
		{
			for (int colorIdx = 0; colorIdx < colors.Length; colorIdx++)
			{
				Card newCard = Instantiate(card_prefab).GetComponent<Card>();
				newCard.value = i;
				newCard.color = colors[colorIdx];

				newCard.gameObject.SetActive(false);
				newCard.transform.SetParent(transform, false);
				newCard.entity = e;

				m_Cards.Add(newCard);
			}
		}
	}

	public Card Draw()
	{
		Card c = m_Cards[0];
		m_Cards.RemoveAt(0);
		return c;
	}

	Card Peek()
	{
		return m_Cards[0];
	}

	public void TryDraw()
	{
		BattleEventBus.getInstance().cardTryDrawEvent.Invoke(e, Peek());
	}

	public void OnMouseDown()
	{
		TryDraw();
	}

	public void OnConfirmDraw(Entity e, Card c)
	{
		if (e != this.e)
		{
			return;
		}
		Draw();
	}

	public void Shuffle()
	{
		System.Random rng = new System.Random();
		int n = m_Cards.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			Card value = m_Cards[k];
			m_Cards[k] = m_Cards[n];
			m_Cards[n] = value;
		}
	}

}
