using Cards;

namespace ActionCards
{
    public class WildCard : AbstractCard
    {
        public static WildCard New(AbstractEntity entity)
        {
            return New(
                name: "Wild Card",
                color: CardColor.Colorless,
                value: null,
                cardOwner: entity,
                cardType: typeof(WildCard),
                frontSprite: "AC0_Wild",
                backSprite: "alpha_art_card_back",
                mouseEventsEnabled: false
            ) as WildCard;
        }
    }
}