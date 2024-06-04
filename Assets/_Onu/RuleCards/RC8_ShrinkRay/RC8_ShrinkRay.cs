namespace RuleCards
{
    public class ShrinkRay : AbstractRuleCard
    {
        public static int Id => 8;
        public override string Name => "ShrinkRay";
        public override string Description => "Your opponent has a max hand size of 5.";
        public override string SpriteName => "alpha_art_card_front";

        AbstractEntity target;
        ShrinkRayEffect shrinkRayEffect;

        public static ShrinkRay New() => new();

        public ShrinkRay()
        {
            EventManager.startedBattleEvent.AddListener(OnStartBattle);
            EventManager.endedBattleEvent.AddListener(OnEndBattle);
        }

        void OnStartBattle()
        {
            if (Entity.isPlayer)
                target = GameMaster.GetInstance().Enemy;
            else
                target = GameMaster.GetInstance().Player;
            shrinkRayEffect = new ShrinkRayEffect();
            target.gameRulesController.AddRuleCard(shrinkRayEffect);
        }

        void OnEndBattle()
        {
            target.gameRulesController.RemoveRuleCard(shrinkRayEffect);
        }

        public override void OnDestroy()
        {
            EventManager.startedBattleEvent.RemoveListener(OnStartBattle);
            EventManager.endedBattleEvent.RemoveListener(OnEndBattle);
        }
    }

    public class ShrinkRayEffect : AbstractRuleCard
    {
        public static int Id = -1;
        public override string Name => "";
        public override string Description => "";
        public override string SpriteName => "";

        public ShrinkRayEffect()
        {
            Spawnable = false;
        }

        public override RuleResult<bool> CanDraw(GameMaster gm, AbstractEntity e)
        {
            if (e.hand.GetCardCount() >= 5)
                return (false, 100);
            return (true, -1);
        }
    }
}