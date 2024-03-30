using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pile : MonoBehaviour
{
    private GameMaster gameMaster;

    private List<Card> pile;

    public void OnEnable()
    {
        gameMaster = GameObject.FindAnyObjectByType<GameMaster>();

        gameMaster.cardPlayedEvent.AddListener(OnCardPlayed);
        pile = new List<Card>();
    }

    void OnCardPlayed(Card card)
    {
        AddToTop(card);
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
