using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ActionCards
{
    public abstract class AbstractActionCard : MonoBehaviour, IUsable
    {
        public abstract int Id { get; }
        public abstract string Name { get; }
        public string SpriteName;
        public bool IsUIObject;
        protected TooltipOwner tooltips;
        public List<string> TooltipBodyLines;
        public int PlayerDataIndex { get; set; } = -1;

        public static AbstractActionCard New<T>(bool isUIObject) => New(typeof(T), isUIObject);
        public static AbstractActionCard New(System.Type actionCardType, bool isUIObject)
        {
            GameObject go = new();
            AbstractActionCard ac = go.AddComponent(actionCardType) as AbstractActionCard;
            go.name = ac.Name;
            RectTransform rt = go.AddComponent<RectTransform>();
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 110f);
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 170f);
            ac.SpriteName = $"AC{ac.Id}_{ac.Name}";
            Sprite sprite = SpriteLoader.LoadSprite(ac.SpriteName);
            if (isUIObject)
            {
                go.AddComponent<CanvasRenderer>();
                if (sprite != null)
                {
                    Image i = go.AddComponent<Image>();
                    i.sprite = sprite;
                }
                rt.SetParent(FindObjectOfType<Canvas>().transform);
                rt.anchoredPosition = new Vector2(0, 0);
            }
            else
            {
                if (sprite != null)
                {
                    SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
                    sr.sprite = sprite;
                }
            }
            BoxCollider2D bc = go.AddComponent<BoxCollider2D>();
            bc.size = new Vector2(110f, 170f);
            ac.IsUIObject = isUIObject;
            ac.tooltips = go.AddComponent<TooltipOwner>();
            return ac;
        }

        public abstract bool IsUsable();

        public abstract void TryUse();

        public abstract void Use(Action onResolved);
    }
}