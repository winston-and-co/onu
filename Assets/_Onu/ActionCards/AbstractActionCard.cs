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
        public abstract string Description { get; }
        public abstract string SpriteName { get; }
        public bool IsUIObject;
        public TooltipOwner Tooltips;
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
            ac.Tooltips = go.AddComponent<TooltipOwner>();
            ac.Tooltips.Add(new()
            {
                Title = ac.Name,
                Body = ac.Description,
            });
            return ac;
        }

        public abstract bool IsUsable();

        public virtual void TryUse()
        {
            EventQueue.GetInstance().actionCardTryUseEvent.AddToBack(GameMaster.GetInstance().Player, this);
        }

        public abstract void Use(Action resolve);
    }
}