using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hand : MonoBehaviour
{
	public int maxHandSize = 7;
	public List<Card> hand;
	private GameMaster gameMaster;

	private void Awake()
	{
		Card[] c = GetComponentsInChildren<Card>();

		for (int i = 0; i < c.Length; i++)
		{
			hand.Add(c[i]);
		}

		gameMaster = GameObject.FindAnyObjectByType<GameMaster>();
		gameMaster.cardPlayedEvent.AddListener(OnCardPlayed);
		gameMaster.playerCardDrawEvent.AddListener(OnCardDrawn);
	}

	public void OnCardDrawn(Card c)
	{
		AddCard(c);
		c.gameObject.SetActive(true);
		c.gameObject.transform.SetParent(transform, false);
	}

	public void OnCardPlayed(Card c)
	{
		if (hand.Contains(c))
		{
			RemoveCard(c);
		}
	}

	public void AddCard(Card c)
	{
		hand.Add(c);
	}

	public void RemoveCard(Card c)
	{
		hand.Remove(c);

	}

	public Card GetCard(int index)
	{
		return hand[index];
	}

	public int GetCardCount() { return hand.Count; }
}
