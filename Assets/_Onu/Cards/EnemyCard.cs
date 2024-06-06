using UnityEngine;
using UnityEngine.Rendering;

namespace Cards
{
    public class EnemyCard : AbstractCard
    {
        public static EnemyCard New(Color color, int? value, AbstractEntity entity, bool generatedInCombat)
        {
            var card = New(
                name: "Card",
                color: color,
                value: value,
                cardOwner: entity,
                cardType: typeof(EnemyCard),
                frontSprite: "alpha_art_card_front",
                backSprite: "alpha_art_card_back",
                mouseEventsEnabled: false,
                generatedInCombat: generatedInCombat
            ) as EnemyCard;
            var rt = card.GetComponent<RectTransform>();
            rt.localScale = new(0.15f, 0.15f);
            card.SpriteController.Flip();
            card.SpriteController.Init();
            card.SpriteController.CardInHandScale = new(0.15f, 0.15f);
            card.SpriteController.CardInPileScale = new(0.2f, 0.2f);
            card.SpriteController.HandSortingLayer = "EnemyHand";
            return card;
        }
    }
}