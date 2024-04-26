using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class Pile : MonoBehaviour
{
    private List<Playable> pile;
    public ReadOnlyCollection<Playable> Items => pile.AsReadOnly();

    void OnEnable()
    {
        BattleEventBus.getInstance().cardPlayedEvent.AddListener(OnCardPlayed);
        pile = new();
    }

    void OnCardPlayed(Entity e, Playable card)
    {
        AddToTop(card);
    }

    ///<returns>Top of pile, or <c>null</c> if empty.</returns>
    public Playable Peek()
    {
        if (pile.Count == 0)
        {
            return null;
        }
        return pile[pile.Count - 1];
    }

    void AddToTop(Playable card)
    {
        pile.Add(card);
        // awful practice, decouple
        card.gameObject.transform.SetParent(gameObject.transform, false);
        card.gameObject.transform.localPosition = Vector3.zero;
    }

    public List<Card> RemoveCardsFromEntity(Entity e)
    {
        IEnumerable<Card> cards = pile
            .Where(c => c.entity == e && c != Peek() && c is Card)
            .Select(c => c as Card);
        List<Card> res = new();
        foreach (var c in cards)
        {
            res.Add(c);
            pile.Remove(c);
        }
        return res;
    }
}