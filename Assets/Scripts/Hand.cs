using System;
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
            AddCard(c[i]);
        }
    }

    public void AddCard(Card c)
    {
        hand.Add(c);
        c.gameObject.SetActive(true);
        c.gameObject.transform.SetParent(transform, false);
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
