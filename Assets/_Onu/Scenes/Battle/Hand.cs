using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cards;

public class Hand : MonoBehaviour
{
    public List<AbstractCard> hand;
    public AbstractEntity e;

    private void Awake()
    {
        AbstractCard[] c = GetComponentsInChildren<AbstractCard>();

        for (int i = 0; i < c.Length; i++)
        {
            AddCard(c[i]);
        }
    }

    public void AddCard(AbstractCard c)
    {
        hand.Add(c);
        c.gameObject.SetActive(true);
        c.gameObject.transform.SetParent(transform, false);
    }

    public void RemoveCard(AbstractCard c)
    {
        hand.Remove(c);
    }

    public AbstractCard GetCard(int index)
    {
        return hand[index];
    }

    public int GetCardCount() { return hand.Count; }
}
