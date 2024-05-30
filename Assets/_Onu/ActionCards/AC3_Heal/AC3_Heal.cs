using System;

namespace ActionCards
{
    public class Heal : AbstractActionCard
    {
        public static int Id => 3;
        public override string Name => "Heal";
        public override string Description => "Heals 15% of your max HP";
        public override string SpriteName => "alpha_art_card_front";

        public static Heal New() => New<Heal>();

        public override void Use(Action resolve)
        {
            var p = PlayerData.GetInstance().Player;
            p.Heal((int)(p.maxHP * 0.15f));
            resolve();
        }
    }
}