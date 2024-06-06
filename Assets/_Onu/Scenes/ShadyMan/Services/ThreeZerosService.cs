using Cards;
using UnityEngine.EventSystems;

public class ThreeZerosService : AbstractShadyManService
{
    public override bool ConditionMet()
    {
        return true;
    }

    public override void OnPointerClick(PointerEventData _)
    {
        var player = PlayerData.GetInstance().Player;
        for (int i = 0; i < 3; i++)
        {
            var card = PlayerCard.New(
                color: CardColor.RandomColor(),
                value: 0,
                entity: player,
                generatedInCombat: false
            );
            player.deck.AddCard(card);
        }
        base.Done();
    }
}
