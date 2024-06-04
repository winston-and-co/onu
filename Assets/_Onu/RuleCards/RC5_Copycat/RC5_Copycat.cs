using Cards;

namespace RuleCards
{
    public class Copycat : AbstractRuleCard
    {
        public static int Id => 5;
        public override string Name => "Copycat";
        public override string Description => "When your opponent draws a card, you also draw a card.";
        public override string SpriteName => "alpha_art_card_front";

        public static Copycat New() => new();

        public Copycat()
        {
            EventManager.cardDrawnEvent.AddListener(OnCardDrawn);
        }

        void OnCardDrawn(AbstractEntity e, AbstractCard c)
        {
            if (e != Entity)
            {
                Entity.Draw();
            }
        }

        public override void OnDestroy()
        {
            EventManager.cardDrawnEvent.RemoveListener(OnCardDrawn);
        }
    }
}