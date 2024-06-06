using UnityEngine;
using Cards;

public class CardHandLayout : MonoBehaviour
{
    public Hand hand;
    public GameObject root; // Starting point of the layout
    public float spacing = 1.1f; // Space between cards

    void Awake()
    {
        EventManager.cardPlayedEvent.AddListener(OnCardPlayed);
        EventManager.cardDrawnEvent.AddListener(OnCardDrawn);
    }

    void ArrangeCards()
    {
        for (int i = 0; i < hand.GetCardCount(); i++)
        {
            Vector3 position = root.GetComponent<RectTransform>().position + new Vector3(i * spacing, 0, 0);

            GameObject card = GetCardAtIndex(i);
            card.transform.position = position;

            CardSpriteController spriteController = card.GetComponentInChildren<CardSpriteController>();
            spriteController.SetOrder(i);
        }
    }

    void OnCardPlayed(AbstractEntity e, AbstractCard _)
    {
        if (!e.hand == hand)
        {
            return;
        }
        ArrangeCards();
    }

    void OnCardDrawn(AbstractEntity e, AbstractCard _)
    {
        if (!e.hand == hand)
        {
            return;
        }
        ArrangeCards();
    }

    GameObject GetCardAtIndex(int index)
    {
        return hand.GetCard(index).gameObject;
    }
}
