using UnityEngine;
using UnityEngine.Rendering;

namespace Cards
{
    public class PlayerCard : AbstractCard
    {
        public static PlayerCard New(Color color, int? value, AbstractEntity entity, bool generatedInCombat)
        {
            var card = New(
                name: "Card",
                color: color,
                value: value,
                cardOwner: entity,
                cardType: typeof(PlayerCard),
                frontSprite: "alpha_art_card_front",
                backSprite: "alpha_art_card_back",
                mouseEventsEnabled: true,
                generatedInCombat: generatedInCombat
            ) as PlayerCard;
            var rt = card.GetComponent<RectTransform>();
            rt.localScale = new(0.3f, 0.3f);
            card.SpriteController.CardInHandScale = new(0.3f, 0.3f);
            card.SpriteController.CardInPileScale = new(0.2f, 0.2f);
            card.SpriteController.HandSortingLayer = "PlayerHand";
            return card;
        }
    }
}