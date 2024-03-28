using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Deck : MonoBehaviour
{
    List<Card> currentCards;
    List<Card> discardedCards;
    

    void Pop()
    {    
        throw new System.NotImplementedException();
    }

    void Shuffle()
    {
        currentCards.AddRange(discardedCards);
        discardedCards.Clear();
        int cardCount = currentCards.Count;
        while (cardCount > 1)
        {
            cardCount--;
            int itsPosition = Random.Range(0, cardCount + 1);
            Card card = currentCards[itsPosition];
            currentCards[itsPosition] = currentCards[cardCount];
            currentCards[cardCount] = card;
        }
        
    }

    public Card Draw()
    {
        if (currentCards.Count == 0) 
        {
            Shuffle();
        }

        Card card = currentCards[0];
        currentCards.RemoveAt(0);
        return card;

        
  
    }
}
