using UnityEngine;
using Cards;

public class SpecialCard : MonoBehaviour
{
    public void PickItUp()
    {
        PlayerCard card = PlayerCard.New(CardColor.Colorless, 5, PlayerData.GetInstance().Player, true);
        PlayerData.GetInstance().Player.deck.AddCard(card);
        GetComponent<BathroomEvent>().Done();
    }

    public void LeaveIt()
    {
        GetComponent<BathroomEvent>().Done();
    }
}
