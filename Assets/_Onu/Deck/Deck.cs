using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Cards;

public class Deck : MonoBehaviour
{
    [SerializeField] private List<AbstractCard> m_Cards = null;
    public GameObject card_prefab;
    public AbstractEntity e;
    public Color[] colors;

    void Awake()
    {
        EventQueue.GetInstance().endBattleEvent.AddListener(OnEndBattle);
    }

    void OnEndBattle(GameMaster gm)
    {
        // return cards from this entity's deck back to the deck
        var d = GameMaster.GetInstance().discard;
        var cards = d.RemoveCardsFromEntity(e, true);
        foreach (var c in e.hand.hand)
        {
            if (c is not AbstractCard)
            {
                e.hand.RemoveCard(c);
            }
        }
        cards = cards.Concat(e.hand.hand.Select(p => p as AbstractCard)).ToList();
        e.hand.hand = new();
        foreach (var c in cards)
        {
            c.transform.SetParent(transform, false);
            c.gameObject.SetActive(false);
            m_Cards.Add(c);
        }
    }

    public void Populate(List<AbstractCard> cards)
    {
        m_Cards = cards;
    }

    public int size()
    {
        return m_Cards.Count;
    }


    public AbstractCard get(int index)
    {
        return m_Cards[index];
    }

    public void Add(AbstractCard c)
    {
        m_Cards.Add(c);
    }
    public AbstractCard Remove(int id)
    {
        AbstractCard c = m_Cards[id];
        m_Cards.RemoveAt(id);
        return c;
    }

    public AbstractCard Draw()
    {
        // TODO: When no cards in deck, maybe reshuffle cards from discard pile
        // Should probably keep track of who's cards are who's since separate
        // decks
        AbstractCard c = m_Cards[0];
        m_Cards.RemoveAt(0);

        if (m_Cards.Count == 0)
        {
            // empty deck, reshuffle own cards back in (except top card)
            var d = GameMaster.GetInstance().discard;
            var discardedCards = d.RemoveCardsFromEntity(e, false);
            foreach (var dCard in discardedCards)
            {
                dCard.transform.SetParent(transform, false);
                dCard.gameObject.SetActive(false);
                m_Cards.Add(dCard);
            }
            Shuffle();
        }
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
            AbstractCard value = m_Cards[k];
            m_Cards[k] = m_Cards[n];
            m_Cards[n] = value;
        }
    }

}
