using UnityEngine;
using Cards;
using UnityEngine.UI;

namespace RuleCards
{
    /// <summary>
    /// Note for implementers: Entity will be null during constructor.
    /// </summary>
    public abstract class AbstractRuleCard : IRuleset
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string SpriteName { get; }
        public AbstractEntity Entity;
        public bool Spawnable = true;

        public GameObject ToGameObject()
        {
            if (!Spawnable) return null;
            GameObject go = new();
            var rc = go.AddComponent<RuleCardUIObject>();
            rc.RuleCard = this;
            go.name = Name;
            RectTransform rt = go.AddComponent<RectTransform>();
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100f);
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 155f);
            Sprite sprite = SpriteLoader.LoadSprite(SpriteName);
            go.AddComponent<CanvasRenderer>();
            if (sprite != null)
            {
                Image i = go.AddComponent<Image>();
                i.sprite = sprite;
            }
            rt.SetParent(Object.FindObjectOfType<Canvas>().transform);
            rt.anchoredPosition = new Vector2(0, 0);
            BoxCollider2D bc = go.AddComponent<BoxCollider2D>();
            bc.size = new Vector2(100f, 155f);
            rc.Tooltips = go.AddComponent<TooltipOwner>();
            rc.Tooltips.Add(new()
            {
                Title = Name,
                Body = Description,
            });
            return go;
        }

        public virtual void OnDestroy() { }

        public virtual RuleResult<bool> ColorsMatch(GameMaster gm, AbstractEntity e, Color color, Color target, int depth)
        {
            return (default, -1);
        }

        public virtual RuleResult<bool> CardIsPlayable(GameMaster gm, AbstractEntity e, AbstractCard c)
        {
            return (default, -1);
        }

        public virtual RuleResult<int> CardManaCost(GameMaster gm, AbstractEntity e, AbstractCard c)
        {
            return (default, -1);
        }

        public virtual RuleResult<bool> CanDraw(GameMaster gm, AbstractEntity e)
        {
            return (default, -1);
        }
    }
}
