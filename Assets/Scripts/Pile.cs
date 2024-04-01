using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pile : MonoBehaviour
{

    private List<Card> pile;

    public void OnEnable()
    {
        BattleEventBus.getInstance().cardPlayedEvent.AddListener(OnCardPlayed);
        pile = new List<Card>();
    }

    void OnCardPlayed(Entity e, Card card)
    {
        AddToTop(card);
    }

    public Card Peek()
    {
        if(pile.Count == 0)
        {
            return null;
        }
        return pile[pile.Count - 1];
    }

    void AddToTop(Card card) {
        if(pile.Count > 0)
        {
            foreach (Renderer r in pile[pile.Count - 1].gameObject.GetComponentsInChildren<Renderer>())
            {
                r.enabled = false;
            }
        }

        pile.Add(card);
        // awful practice, decouple
        card.gameObject.transform.SetParent(gameObject.transform, false);
        card.gameObject.transform.localPosition = Vector3.zero;
        card.GetComponent<BoxCollider2D>().enabled = false;
    }
}
