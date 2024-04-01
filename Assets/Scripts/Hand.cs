using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hand : MonoBehaviour
{
    public List<Card> hand;
    public Entity e;

    private void Awake()
	{
		Card[] c = GetComponentsInChildren<Card>();

		for (int i = 0; i < c.Length; i++)
		{
			hand.Add(c[i]);
		}
        BattleEventBus.getInstance().cardPlayedEvent.AddListener(OnCardPlayed);
        BattleEventBus.getInstance().cardDrawEvent.AddListener(OnCardDrawn);
	}


	public void AddCard(Card c)
	{
		hand.Add(c);
	}
    

    public void OnCardDrawn(Entity e, Card c)
    {
        if (e != this.e)
        {
            return;
        }
        AddCard(c);
        c.gameObject.SetActive(true);
        c.gameObject.transform.SetParent(transform, false);
        
    }

    public void OnCardPlayed(Entity e, Card c) 
    {
        if (e != this.e)
        {
            return;
        }
        if(hand.Contains(c))
        {
            RemoveCard(c);
        }
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
