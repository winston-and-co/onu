using System;

namespace ActionCards
{
    public class MaxManaUp : AbstractActionCard
    {
        public static int Id => 6;
        public override string Name => "Max Mana Up";
        public override string Description => "Gain 5 Max Mana";
        public override string SpriteName => "alpha_art_card_front";

        public static MaxManaUp New() => New<MaxManaUp>();

        public override bool IsUsable()
        {
            return true;
        }

        public override void Use(Action resolve)
        {
            var p = PlayerData.GetInstance().Player;
            p.ChangeMaxMana(5);
            resolve();
        }
    }
}