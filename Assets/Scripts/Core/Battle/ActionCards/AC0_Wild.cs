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
        WildPrompt promptInstance;

        public void TryUse()
        {
            BattleEventBus.getInstance().actionCardTryUseEvent.Invoke(GameMaster.GetInstance().player, this);
        }

        public bool IsUsable()
        {
            if (cardInstance != null) Destroy(cardInstance);
            cardInstance = Instantiate(wildCardPrefab);
            cardInstance.Value = new NullableInt { Value = 0, IsNull = true };
            cardInstance.Color = CardColors.Colorless;
            var gm = GameMaster.GetInstance();
            cardInstance.entity = gm.player;
            if (gm.current_turn_entity == gm.player
            && gm.player.gameRules.CardIsPlayable(gm, gm.player, cardInstance))
            {
                return true;
            }
            Destroy(cardInstance);
            return false;
        }

        public void Use()
        {
            var canvas = FindObjectOfType<Canvas>();
            if (promptInstance == null)
                promptInstance = Instantiate(wildPromptPrefab, canvas.transform, false);
            promptInstance.transform.position.Set(0, 0, 0);
            promptInstance.wild = this;
            promptInstance.Show();
        }

        public void OnColorSelected(Color color)
        {
            promptInstance.Hide();
            var gm = GameMaster.GetInstance();
            BattleEventBus.getInstance().cardTryPlayedEvent.Invoke(gm.player, cardInstance);
            cardInstance.Color = color;
        }
    }
}