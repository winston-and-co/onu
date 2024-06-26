using Cards;
using UnityEngine.EventSystems;

public class Make20CardService : AbstractShadyManService
{
    public override bool ConditionMet()
    {
        return true;
    }

    public override void OnPointerClick(PointerEventData _)
    {
        Deck d = PlayerData.GetInstance().Player.deck;
        System.Random rand = new();
        AbstractCard c = d.Get(rand.Next(d.Size()));
        c.Value = 20;
        base.Done();
    }
}
