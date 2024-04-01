using UnityEngine;

public class CardHandLayout : MonoBehaviour
{
    public Hand hand;
    public GameObject root; // Starting point of the layout
    public float spacing = 1.1f; // Space between cards

    void Start()
    {

        BattleEventBus.getInstance().cardPlayedEvent.AddListener(OnCardPlayed);
        BattleEventBus.getInstance().cardDrawEvent.AddListener(OnCardDrawn);

    }


    void ArrangeCards()
    {
        int numberOfCards = hand.GetCardCount();
        for (int i = 0; i < hand.GetCardCount(); i++)
        {
            Vector3 position = root.GetComponent<RectTransform>().position + new Vector3(i * spacing, 0, 0); 

            GameObject card = GetCardAtIndex(i);
            card.transform.position = position;

            CardSprite spriteController = card.GetComponentInChildren<CardSprite>();
            spriteController.SetOrder(i);
        }
    }

    void OnCardPlayed(Entity e, Card c)
    {
        //if(!e.name.ToLower().Equals("player"))
        //{
        //    return;
        //}
        ArrangeCards();
    }
    void OnCardDrawn(Entity e, Card c)
    {
        //if (!e.name.ToLower().Equals("player"))
        //{
        //    return;
        //}
        ArrangeCards();
    }

    GameObject GetCardAtIndex(int index)
    {
        return hand.GetCard(index).gameObject;
    }
}
