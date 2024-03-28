using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    List<Card> hand;

    public void AddCard(Card card)
    {
        hand.Add(card);

    }

    public void RemoveCard(Card card)
    {
        hand.Remove(card);
    }

    public bool Empty()
    {
        return hand.Count == 0;
    }


}
