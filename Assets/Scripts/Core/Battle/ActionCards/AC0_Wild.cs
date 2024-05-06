using System.Collections;
using UnityEngine;

namespace ActionCards
{
    public class Wild : ActionCardBase, IUsable
    {
        public override string Name => "AC0_Wild";
        [SerializeField] WildPrompt wildPromptPrefab;
        [SerializeField] WildCard wildCardPrefab;
        WildCard cardInstance;

        public void TryUse()
        {
            BattleEventBus.getInstance().actionCardTryUseEvent.Invoke(GameMaster.GetInstance().player, this);
        }

        public bool IsUsable()
        {
            if (cardInstance != null) Destroy(cardInstance);
            cardInstance = Instantiate(wildCardPrefab);
            var gm = GameMaster.GetInstance();
            if (gm.current_turn_entity == gm.player
            && gm.player.gameRules.CardIsPlayable(gm, gm.player, cardInstance))
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
            var gm = GameMaster.GetInstance();
            cardInstance.Color = color;
            BattleEventBus.getInstance().cardTryPlayedEvent.Invoke(gm.player, cardInstance);
        }
    }
}