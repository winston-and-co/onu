using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ActionCards
{
    public abstract class AbstractActionCard : MonoBehaviour, IUsable
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string SpriteName { get; }
        public int PlayerDataIndex;
        public bool IsUIObject;
        public TooltipOwner Tooltips;

        public static T New<T>(bool isUIObject = true) where T : AbstractActionCard => New(typeof(T), isUIObject) as T;
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

        /// <summary>
        /// By default, usable if in battle and during the player's turn.
        /// <code>
        /// var gm = GameMaster.GetInstance();
        /// if (gm == null) return false;
        /// if (gm.CurrentEntity == gm.Player)
        /// {
        ///     return true;
        /// }
        /// return false;
        /// </code>
        /// </summary>
        /// <returns>Whether it is usable.</returns>
        public virtual bool IsUsable()
        {
            var gm = GameMaster.GetInstance();
            if (gm == null) return false;
            if (gm.CurrentEntity == gm.Player)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// <code>
        /// GameMaster.GetInstance().TryUseActionCard(this);
        /// </code>
        /// </summary>
        public virtual void TryUse()
        {
            GameMaster.GetInstance().TryUseActionCard(this);
        }

        public abstract void Use(Action resolve);
    }
}