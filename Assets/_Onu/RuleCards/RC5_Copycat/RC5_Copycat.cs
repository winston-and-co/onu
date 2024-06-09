using Cards;

namespace RuleCards
{
    public class Copycat : AbstractRuleCard
    {
        public static int Id => 5;
        public override string Name => "Copycat";
        public override string Description => "When your opponent draws a card during their turn, you also draw a card.";
        public override string SpriteName => "alpha_art_card_front";

        public static Copycat New() => new();

        public Copycat()
        {
            EventManager.startedBattleEvent.AddListener(OnStartBattle);
            EventManager.endedBattleEvent.AddListener(OnEndBattle);
        }

        void OnStartBattle()
        {
            EventManager.cardDrawnEvent.AddListener(OnCardDrawn);
        }

        void OnEndBattle()
        {
            EventManager.cardDrawnEvent.RemoveListener(OnCardDrawn);
        }

        void OnCardDrawn(AbstractEntity e, AbstractCard c)
        {
            if (e != Entity && e == GameMaster.GetInstance().CurrentEntity)
            {
                Entity.Draw();
            }
        }

        public override void OnDestroy()
        {
            EventManager.startedBattleEvent.RemoveListener(OnStartBattle);
            EventManager.endedBattleEvent.RemoveListener(OnEndBattle);
        }
    }
}