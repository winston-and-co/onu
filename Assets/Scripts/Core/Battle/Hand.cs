using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hand : MonoBehaviour
{
    public List<Playable> hand;
    public Entity e;

    private void Awake()
    {
        Playable[] c = GetComponentsInChildren<Playable>();

        for (int i = 0; i < c.Length; i++)
        {
            AddCard(c[i]);
        }
    }

    public void AddCard(Playable c)
    {
        hand.Add(c);
        c.gameObject.SetActive(true);
        c.gameObject.transform.SetParent(transform, false);
    }

    public void RemoveCard(Playable c)
    {
        hand.Remove(c);
    }

    public Playable GetCard(int index)
    {
        return hand[index];
    }

    public int GetCardCount() { return hand.Count; }
}
