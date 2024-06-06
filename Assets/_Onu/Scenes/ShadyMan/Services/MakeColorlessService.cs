using Cards;
using UnityEngine.EventSystems;

public class MakeColorlessService : AbstractShadyManService
{
    public override bool ConditionMet()
    {
        return true;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        Deck d = PlayerData.GetInstance().Player.deck;
        System.Random rand = new();
        AbstractCard c = d.Get(rand.Next(d.Size()));
        c.Color = CardColor.Colorless;
        base.Done();
    }
}