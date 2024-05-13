using UnityEngine;

namespace ActionCards
{
    public class Wild : AbstractActionCard
    {
        public override int Id => 0;
        public override string Name => "Wild";
        [SerializeField] GameObject colorPickerPrefab;
        [SerializeField] WildCard wildCardPrefab;
        [SerializeField] WildCard cardInstance;
        [SerializeField] GameObject colorPickerInstance;

        public static Wild New()
        {
            var wild = New("Wild", "AC0_Wild", typeof(Wild), true) as Wild;
            wild.tooltips.Add(new()
            {
                Title = wild.Name,
                Body = "Can change to any color.",
            });
            wild.colorPickerPrefab = PrefabHelper.GetInstance().GetPrefab(PrefabType.UI_ColorPicker);
            return wild;
        }

        public override void TryUse()
        {
            BattleEventBus.getInstance().actionCardTryUseEvent.Invoke(GameMaster.GetInstance().player, this);
        }

        public override bool IsUsable()
        {
            if (cardInstance != null) Destroy(cardInstance);
            cardInstance = WildCard.New(PlayerData.GetInstance().Player);
            var gm = GameMaster.GetInstance();
            cardInstance.Entity = gm.player;
            if (gm.current_turn_entity == gm.player
            && gm.player.gameRules.CardIsPlayable(gm, gm.player, cardInstance))
            {
                return true;
            }
            Destroy(cardInstance);
            return false;
        }

        public override void Use()
        {
            var canvas = FindObjectOfType<Canvas>();
            if (colorPickerInstance == null)
                colorPickerInstance = Instantiate(colorPickerPrefab, canvas.transform, false);
            colorPickerInstance.transform.position.Set(0, 0, 0);
            var cp = colorPickerInstance.GetComponent<ColorPicker>();
            cp.OnColorPicked.AddListener(OnColorSelected);
            cp.Show();
        }

        public void OnColorSelected(Color color)
        {
            var cp = colorPickerInstance.GetComponent<ColorPicker>();
            cp.Hide();
            var gm = GameMaster.GetInstance();
            BattleEventBus.getInstance().cardTryPlayedEvent.Invoke(gm.player, cardInstance);
            cardInstance.Color = color;
        }
    }
}