using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
}