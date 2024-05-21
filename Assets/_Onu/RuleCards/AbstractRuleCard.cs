using UnityEngine;
using Cards;
using UnityEngine.UI;

namespace RuleCards
{
    public abstract class AbstractRuleCard : MonoBehaviour, IRuleset
    {
        public abstract int Id { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string SpriteName { get; }
        public TooltipOwner Tooltips;

        public static AbstractRuleCard New<T>() => New(typeof(T));
        public static AbstractRuleCard New(System.Type ruleCardType)
        {
            GameObject go = new();
            AbstractRuleCard rc = go.AddComponent(ruleCardType) as AbstractRuleCard;
            go.name = rc.Name;
            RectTransform rt = go.AddComponent<RectTransform>();
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100f);
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 155f);
            Sprite sprite = SpriteLoader.LoadSprite(rc.SpriteName);
            go.AddComponent<CanvasRenderer>();
            if (sprite != null)
            {
                Image i = go.AddComponent<Image>();
                i.sprite = sprite;
            }
            rt.SetParent(FindObjectOfType<Canvas>().transform);
            rt.anchoredPosition = new Vector2(0, 0);
            BoxCollider2D bc = go.AddComponent<BoxCollider2D>();
            bc.size = new Vector2(100f, 155f);
            rc.Tooltips = go.AddComponent<TooltipOwner>();
            rc.Tooltips.Add(new()
            {
                Title = rc.Name,
                Body = rc.Description,
            });
            return rc;
        }

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
