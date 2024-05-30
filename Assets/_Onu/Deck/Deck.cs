using System.Collections.Generic;
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
        EventManager.endBattleEvent.AddListener(OnEndBattle);
    }

    void OnEndBattle(GameMaster gm)
    {
        // RESET DISCARD PILE
        var pile = gm.DiscardPile.Cards;
        // for each card in the pile
        for (int i = pile.Count - 1; i >= 0; i--)
        {
            AbstractCard c = pile[i];
            // if it is permanent
            if (!c.GeneratedInCombat)
            {
                // return it to the owner's deck
                c.Entity.deck.AddCard(c);
                // then remove the card from the pile
                pile.Remove(c);
            }
        }
        // destroy all remaining cards and reset the pile
        for (int i = pile.Count - 1; i >= 0; i--)
        {
            AbstractCard c = pile[i];
            pile.Remove(c);
            Destroy(c.gameObject);
        }

        // RESET HAND
        var hand = e.hand.Cards;
        for (int i = hand.Count - 1; i >= 0; i--)
        {
            AbstractCard c = hand[i];
            if (!c.GeneratedInCombat)
            {
                e.deck.AddCard(c);
            }
            e.hand.RemoveCard(c);
        }
    }

    public void SetCards(List<AbstractCard> cards)
    {
        m_Cards = cards;
    }

    public int Size() { return m_Cards.Count; }

    public AbstractCard Get(int index)
    {
        return m_Cards[index];
    }

    /// <summary>
    /// Adds a card to this deck. Sets the card inactive and sets its parent to
    /// this deck.
    /// </summary>
    /// <param name="c">The card to add</param>
    public void AddCard(AbstractCard c)
    {
        m_Cards.Add(c);
        c.gameObject.SetActive(false);
        c.transform.SetParent(transform);
        c.transform.localPosition = Vector3.zero;
    }

    public AbstractCard RemoveCard(AbstractCard c)
    {
        m_Cards.Remove(c);
        return c;
    }
    public AbstractCard RemoveCard(int idx)
    {
        return RemoveCard(m_Cards[idx]);
    }

    public void RemovePermanently(AbstractCard c)
    {
        m_Cards.Remove(c);
        Destroy(c.gameObject);
    }
    public void RemovePermanently(int idx)
    {
        RemovePermanently(m_Cards[idx]);
    }

    public AbstractCard Draw()
    {
        AbstractCard c = m_Cards[0];
        m_Cards.RemoveAt(0);

        if (m_Cards.Count == 0)
        {
            // empty deck, reshuffle own cards back in (except top card)
            var pile = GameMaster.GetInstance().DiscardPile;
            pile.OnEmptyDeckDraw(e);
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
        EventManager.deckShuffledEvent.AddToBack(e, this);
    }
}
