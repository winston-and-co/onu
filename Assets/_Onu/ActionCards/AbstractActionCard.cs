using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ActionCards
{
    public abstract class AbstractActionCard : MonoBehaviour, IUsable
    {
        public abstract int Id { get; }
        public abstract string Name { get; }
        public bool IsUIObject;
        protected TooltipOwner tooltips;
        public List<string> TooltipBodyLines;
        public int PlayerDataIndex { get; set; } = -1;

        public static AbstractActionCard New(string name, string spriteName, System.Type actionCardType, bool isUIObject)
        {
            GameObject go = new(name);
            RectTransform rt = go.AddComponent<RectTransform>();
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 110f);
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 170f);
            Sprite sprite = SpriteLoader.LoadSprite(spriteName);
            if (isUIObject)
            {
                go.AddComponent<CanvasRenderer>();
                Image i = go.AddComponent<Image>();
                i.sprite = sprite;
                rt.SetParent(FindObjectOfType<Canvas>().transform);
                rt.anchoredPosition = new Vector2(0, 0);
            }
            else
            {
                SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
                sr.sprite = sprite;
            }
            BoxCollider2D bc = go.AddComponent<BoxCollider2D>();
            bc.size = new Vector2(110f, 170f);
            AbstractActionCard ac = go.AddComponent(actionCardType) as AbstractActionCard;
            ac.IsUIObject = isUIObject;
            ac.tooltips = go.AddComponent<TooltipOwner>();
            return ac;
        }

        public virtual bool IsUsable() { return false; }

        public virtual void TryUse() { }

        public virtual void Use() { }
    }
}