using System;

namespace ActionCards
{
    public class RestoreMana : AbstractActionCard
    {
        public static int Id => 4;
        public override string Name => "Restore Mana";
        public override string Description => "Restores all of your mana";
        public override string SpriteName => "alpha_art_card_front";

        public static RestoreMana New() => New<RestoreMana>();

        public override void Use(Action resolve)
        {
            var p = PlayerData.GetInstance().Player;
            p.RestoreMana(p.maxMana);
            resolve();
        }
    }
}