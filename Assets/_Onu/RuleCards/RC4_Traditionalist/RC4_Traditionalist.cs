namespace RuleCards
{
    public class Traditionalist : AbstractRuleCard
    {
        public static int Id => 4;
        public override string Name => "Traditionalist";
        public override string Description => "You start each battle with 0 cards, but you draw 5 cards at the start of each turn.";
        public override string SpriteName => "alpha_art_card_front";

        public static Traditionalist New() => new();

        public Traditionalist()
        {
            EventManager.beforeBattleStartEvent.AddListener(BeforeBattleStart);
        }

        void BeforeBattleStart()
        {
            Entity.StartingHandSize = 0;
            Entity.TurnStartDrawUntilPlayable = false;
            Entity.TurnStartNumCardsToDraw = 5;
        }

        public override void OnDestroy()
        {
            EventManager.beforeBattleStartEvent.RemoveListener(BeforeBattleStart);
        }
    }
}