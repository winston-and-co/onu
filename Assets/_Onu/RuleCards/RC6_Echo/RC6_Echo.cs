using Cards;

namespace RuleCards
{
    public class Echo : AbstractRuleCard
    {
        public static int Id => 6;
        public override string Name => "Echo";
        public override string Description => "Your first card played each turn is played twice.";
        public override string SpriteName => "alpha_art_card_front";

        bool firstCardPlayed = false;

        public static Echo New() => new();

        public Echo()
        {
            EventManager.startedTurnEvent.AddListener(OnStartTurn);
            EventManager.cardPlayedEvent.AddListener(OnCardPlayed);
        }

        void OnStartTurn(AbstractEntity e)
        {
            if (e != Entity) return;
            firstCardPlayed = false;
        }

        void OnCardPlayed(AbstractEntity e, AbstractCard c)
        {
            if (e != Entity) return;
            if (firstCardPlayed) return;
            AbstractCard card;
            if (e.isPlayer)
                card = PlayerCard.New(c.Color, c.Value, e, true);
            else
                card = EnemyCard.New(c.Color, c.Value, e, true);
            GameMaster.GetInstance().TryPlayCard(card);
        }

        public override void OnDestroy()
        {
            EventManager.startedTurnEvent.RemoveListener(OnStartTurn);
            EventManager.cardPlayedEvent.RemoveListener(OnCardPlayed);
        }
    }
}