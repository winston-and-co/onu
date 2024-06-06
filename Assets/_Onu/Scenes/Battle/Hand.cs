using System.Collections.Generic;
using UnityEngine;
using Cards;

public class Hand : MonoBehaviour
{
    public readonly List<AbstractCard> Cards = new();
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
        Cards.Add(c);
        c.gameObject.SetActive(true);
        c.gameObject.transform.SetParent(transform, false);
    }

    public void RemoveCard(AbstractCard c)
    {
        Cards.Remove(c);
    }

    public void RemoveCard(AbstractCard c, GameObject newParent)
    {
        Cards.Remove(c);
        c.transform.SetParent(newParent.transform);
    }

    public AbstractCard GetCard(int index)
    {
        return Cards[index];
    }

    public int GetCardCount() { return Cards.Count; }
}
