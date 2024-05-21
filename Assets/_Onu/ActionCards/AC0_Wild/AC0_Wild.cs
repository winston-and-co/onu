using System;
using UnityEngine;
using UnityEngine.Events;

namespace ActionCards
{
    public class Wild : AbstractActionCard
    {
        public override int Id => 0;
        public override string Name => "Wild";
        public override string Description => "Can change to any Color";
        public override string SpriteName => "AC0_Wild";

        [SerializeField] WildCard cardInstance;
        [SerializeField] GameObject colorPickerInstance;

        public static Wild New()
        {
            var wild = New<Wild>(true) as Wild;
            wild.Tooltips.Add(Keywords.Stacking);
            return wild;
        }

        public override bool IsUsable()
        {
            if (cardInstance != null) Destroy(cardInstance);
            cardInstance = WildCard.New(PlayerData.GetInstance().Player);
            cardInstance.gameObject.SetActive(false);
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

        public override void Use(Action onResolved)
        {
            var canvas = FindObjectOfType<Canvas>();
            var canvasRt = canvas.GetComponent<RectTransform>();
            colorPickerInstance = PrefabHelper.GetInstance().GetInstantiatedPrefab(PrefabType.UI_ColorPicker);
            var rt = colorPickerInstance.GetComponent<RectTransform>();
            rt.SetParent(canvasRt);
            rt.anchoredPosition = new Vector2(0, 0);
            var cp = colorPickerInstance.GetComponent<ColorPicker>();
            cp.OnColorPicked.AddListener(OnColorSelected(onResolved));
            cp.Show();
        }

        public UnityAction<Color> OnColorSelected(Action resolve)
        {
            return (Color color) =>
            {
                var cp = colorPickerInstance.GetComponent<ColorPicker>();
                cp.Hide();
                Destroy(cp.gameObject);
                var gm = GameMaster.GetInstance();
                cardInstance.gameObject.SetActive(true);
                EventQueue.GetInstance().cardTryPlayedEvent.AddToBack(gm.Player, cardInstance);
                cardInstance.Color = color;
                cardInstance.SpriteController.SpriteRenderer.color = Color.white;
                resolve();
            };
        }
    }
}