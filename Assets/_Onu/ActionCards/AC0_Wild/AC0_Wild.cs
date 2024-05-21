using System;
using UnityEngine;
using UnityEngine.Events;

namespace ActionCards
{
    public class Wild : AbstractActionCard
    {
        public override int Id => 0;
        public override string Name => "Wild";
        [SerializeField] WildCard wildCardPrefab;
        [SerializeField] WildCard cardInstance;
        [SerializeField] GameObject colorPickerInstance;

        public static Wild New()
        {
            var wild = New<Wild>(true) as Wild;
            wild.tooltips.Add(new()
            {
                Title = wild.Name,
                Body = "Can change to any Color.",
            });
            return wild;
        }

        public override bool IsUsable()
        {
            if (cardInstance != null) Destroy(cardInstance);
            cardInstance = WildCard.New(PlayerData.GetInstance().Player);
            var gm = GameMaster.GetInstance();
            cardInstance.Entity = gm.Player;
            if (gm.CurrentEntity == gm.Player
            && gm.Player.gameRulesController.CardIsPlayable(gm, gm.Player, cardInstance))
            {
                return true;
            }
            Destroy(cardInstance);
            return false;
        }

        public override void TryUse()
        {
            EventQueue.GetInstance().actionCardTryUseEvent.AddToBack(GameMaster.GetInstance().Player, this);
        }

        public override void Use(Action onResolved)
        {
            var canvas = FindObjectOfType<Canvas>();
            var canvasRt = canvas.GetComponent<RectTransform>();
            if (colorPickerInstance == null)
            {
                colorPickerInstance = PrefabHelper.GetInstance().GetInstantiatedPrefab(PrefabType.UI_ColorPicker);
            }
            var rt = colorPickerInstance.GetComponent<RectTransform>();
            rt.SetParent(canvasRt);
            rt.anchoredPosition = new Vector2(0, 0);
            var cp = colorPickerInstance.GetComponent<ColorPicker>();
            cp.OnColorPicked.AddListener(OnColorSelected(onResolved));
            cp.Show();
        }

        public UnityAction<Color> OnColorSelected(Action onResolved)
        {
            return (Color color) =>
            {
                var cp = colorPickerInstance.GetComponent<ColorPicker>();
                cp.Hide();
                var gm = GameMaster.GetInstance();
                EventQueue.GetInstance().cardTryPlayedEvent.AddToBack(gm.Player, cardInstance);
                cardInstance.Color = color;
                cardInstance.SpriteController.SpriteRenderer.color = Color.white;
                onResolved();
            };
        }
    }
}