namespace RuleCards
{
    public class Soda : AbstractRuleCard
    {
        public static int Id => 7;
        public override string Name => "Soda";
        public override string Description => "You refresh at hand size of 1 instead of 0.";
        public override string SpriteName => "alpha_art_card_front";
        int oldRefreshThreshold;

        public static Soda New() => new();

        public Soda()
        {
            EventManager.startedBattleEvent.AddListener(OnStartBattle);
        }

        void OnStartBattle()
        {
            oldRefreshThreshold = Entity.RefreshThreshold;
            if (Entity.RefreshThreshold < 1)
                Entity.RefreshThreshold = 1;
        }

        public override void OnDestroy()
        {
            EventManager.startedBattleEvent.RemoveListener(OnStartBattle);
            Entity.RefreshThreshold = oldRefreshThreshold;
        }
    }
}