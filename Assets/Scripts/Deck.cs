using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private List<Card> m_Cards;
    public GameObject card_prefab;
    public Entity e;

    public readonly Color[] colors = new Color[] {
        CardColors.Red,
        CardColors.Blue,
        CardColors.Green,
        CardColors.Yellow,
    };

    public void Awake()
    {
        Generate();
        Shuffle();
    }

    public void Generate()
    {
        for (int sets = 0; sets < 2; sets++)
        {
            for (int value = 0; value < 10; value++)
            {
                for (int colorIdx = 0; colorIdx < colors.Length; colorIdx++)
                {
                    Card newCard = Instantiate(card_prefab).GetComponent<Card>();
                    newCard.Value = new NullableInt { Value = value, IsNull = false };
                    newCard.Color = colors[colorIdx];

                    newCard.gameObject.SetActive(false);
                    newCard.transform.SetParent(transform, false);
                    newCard.entity = e;

                    m_Cards.Add(newCard);
                }
            }
        }
    }

    public Card Draw()
    {
        Card c = m_Cards[0];
        m_Cards.RemoveAt(0);
        return c;
    }

    public void OnMouseDown()
    {
        e.Draw();
    }

    public void Shuffle()
    {
        System.Random rng = new System.Random();
        int n = m_Cards.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Card value = m_Cards[k];
            m_Cards[k] = m_Cards[n];
            m_Cards[n] = value;
        }
    }

}
