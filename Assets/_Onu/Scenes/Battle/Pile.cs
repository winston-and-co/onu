using System.Collections.Generic;
using UnityEngine;
using Cards;

public class Pile : MonoBehaviour
{
    readonly public List<AbstractCard> Cards = new();

    void OnEnable()
    {
        EventManager.cardPlayedEvent.AddListener(OnCardPlayed);
    }

    void OnCardPlayed(AbstractEntity e, AbstractCard card)
    {
        AddToTop(card);
    }

    ///<returns>Top of pile, or <c>null</c> if empty.</returns>
    public AbstractCard Peek()
    {
        if (Cards.Count == 0)
        {
            return null;
        }
        return Cards[Cards.Count - 1];
    }

    void AddToTop(AbstractCard card)
    {
        Cards.Add(card);
    }

    /// <summary>
    /// Called when an entity draws and their deck is empty. Returns that
    /// entity's cards to their deck (except the top card).
    /// </summary>
    /// <param name="e"></param>
    public void OnEmptyDeckDraw(AbstractEntity e)
    {
        Cards.RemoveAll(c =>
        {
            if (c.Entity == e && c != Peek())
            {
                e.deck.AddCard(c);
                return true;
            }
            return false;
        });
    }
}