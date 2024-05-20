using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using Cards;

public class Pile : MonoBehaviour
{
    private List<AbstractCard> pile;
    public ReadOnlyCollection<AbstractCard> Items => pile.AsReadOnly();

    void OnEnable()
    {
        BattleEventBus.GetInstance().cardPlayedEvent.AddListener(OnCardPlayed);
        pile = new();
    }

    void OnCardPlayed(AbstractEntity e, AbstractCard card)
    {
        AddToTop(card);
    }

    ///<returns>Top of pile, or <c>null</c> if empty.</returns>
    public AbstractCard Peek()
    {
        if (pile.Count == 0)
        {
            return null;
        }
        return pile[pile.Count - 1];
    }

    void AddToTop(AbstractCard card)
    {
        pile.Add(card);
        // awful practice, decouple
        card.gameObject.transform.SetParent(gameObject.transform, false);
        card.gameObject.transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// Removes cards that belonged to given entity.
    /// </summary>
    /// <param name="includeTop">Whether the top card should also be removed</param>
    /// <returns>
    /// A list containing the removed cards.
    /// </returns>
    public List<AbstractCard> RemoveCardsFromEntity(AbstractEntity e, bool includeTop)
    {
        List<AbstractCard> res = new();
        foreach (var c in pile)
        {
            if (c.Entity == e && (includeTop || c != Peek()) && c is AbstractCard)
            {
                res.Add(c as AbstractCard);
            }
        }
        foreach (var c in res)
        {
            pile.Remove(c);
        }
        return res;
    }
}