using System;

namespace ActionCards
{
    public class DoubleDamage : AbstractActionCard
    {
        public static int Id => 8;
        public override string Name => "Double Damage";
        public override string Description => "You deal double damage this turn";
        public override string SpriteName => "alpha_art_card_front";

        public static DoubleDamage New() => New<DoubleDamage>();

        public override void Use(Action resolve)
        {
            PlayerData.GetInstance().Player.DamageDealingModifier *= 2;
            EventManager.endedTurnEvent.AddListener(EndTurnListener);
            resolve();
        }

        void EndTurnListener(AbstractEntity e)
        {
            if (e != PlayerData.GetInstance().Player) return;
            PlayerData.GetInstance().Player.DamageDealingModifier /= 2;
            EventManager.endedTurnEvent.RemoveListener(EndTurnListener);
        }
    }
}