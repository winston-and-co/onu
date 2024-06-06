using System.Collections.Generic;
using UnityEngine;
using Cards;
using System;

public enum DeckType
{
    Standard, // 2 of each 0-9 for RBGY
}

public class DeckFactory
{
    public static Deck MakeDeck(DeckType type, AbstractEntity owner)
    {
        var deck = PrefabLoader.GetInstance().InstantiatePrefab(PrefabType.Deck).GetComponent<Deck>();
        if (deck == null) throw new Exception("Deck prefab missing Deck script component");
        // Attach to entity
        owner.deck = deck;
        deck.e = owner;
        deck.transform.SetParent(owner.transform, false);
        List<AbstractCard> cards = type switch
        {
            DeckType.Standard => Standard(deck),
            _ => throw new ArgumentException(),
        };
        deck.SetCards(cards);
        return deck;
    }

    static List<AbstractCard> Standard(Deck deck)
    {
        Color[] colors = new Color[] {
            CardColor.Red,
            CardColor.Blue,
            CardColor.Green,
            CardColor.Yellow,
        };
        deck.colors = colors;
        List<AbstractCard> cards = new();
        for (int sets = 0; sets < 2; sets++)
        {
            for (int colorIdx = 0; colorIdx < colors.Length; colorIdx++)
            {
                for (int value = 0; value < 10; value++)
                {
                    AbstractCard newCard;
                    if (deck.e.isPlayer)
                    {
                        newCard = PlayerCard.New(colors[colorIdx], value, deck.e, false);
                    }
                    else
                    {
                        newCard = EnemyCard.New(colors[colorIdx], value, deck.e, false);
                    }
                    newCard.transform.SetParent(deck.transform, false);
                    newCard.Entity = deck.e;
                    newCard.gameObject.SetActive(false);

                    cards.Add(newCard);
                }
            }
        }
        return cards;
    }
}