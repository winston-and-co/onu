using UnityEngine;

namespace ActionCards
{
    public class Wild : Playable, IActionCard
    {
        public GameMaster GM;
        public string Name => "AC0_Wild";

        public void Start()
        {
            GM = FindObjectOfType<GameMaster>();
            Value = new NullableInt { Value = 0, IsNull = true };
            Color = CardColors.Colorless;
            entity = GM.player;
        }

        public void TryUse()
        {
            BattleEventBus.getInstance().actionCardTryUseEvent.Invoke(GM.player, this);
        }

        public bool IsUsable()
        {
            if (GM.current_turn_entity == GM.player)
            {
                print("ac0 playable");
                return true;
            }
            print("ac0 not playable");
            return false;
        }

        public void Use()
        {
            Value.IsNull = true;
            Color = CardColors.Colorless;

            BattleEventBus.getInstance().cardTryPlayedEvent.Invoke(GM.player, this);

            var picked = PickColor();
            Color = picked;
        }

        public Color PickColor()
        {
            return CardColors.Blue;
        }

        public void OnMouseDown()
        {
            // temp proof of concept
            TryUse();
        }
    }
}