using System.Collections;
using UnityEngine;

namespace ActionCards
{
    public class Wild : Playable, IActionCard
    {
        public GameMaster GM;
        public string Name => "AC0_Wild";
        [SerializeField] WildPrompt wildPromptPrefab;

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
            if (GM.current_turn_entity == GM.player
            && GM.player.gameRules.CardIsPlayable(GM, GM.player, this))
            {
                return true;
            }
            return false;
        }

        public void Use()
        {
            var canvas = FindObjectOfType<Canvas>();
            var prompt = Instantiate(wildPromptPrefab, canvas.transform, false);
            prompt.transform.position.Set(0, 0, 0);
            prompt.wild = this;
            prompt.Show();
        }

        public void OnColorSelected(Color color)
        {
            BattleEventBus.getInstance().cardTryPlayedEvent.Invoke(GM.player, this);
            Color = color;
        }

        public void OnMouseDown()
        {
            TryUse();
        }
    }
}