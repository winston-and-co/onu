using UnityEngine;

namespace Cards
{
    public abstract class AbstractCard : MonoBehaviour
    {
        public int Cost;
        public int? Value;
        [SerializeField] private Color _color;
        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                var sr = GetComponentInChildren<SpriteRenderer>();
                if (sr != null)
                {
                    sr.color = value;
                    return;
                }
            }
        }
        protected CardSprite SpriteController;
        public AbstractEntity Entity;

        public static AbstractCard New(string name, Color color, int? value, AbstractEntity cardOwner, System.Type cardType, string frontSprite, string backSprite, bool mouseEventsEnabled)
        {
            PrefabHelper ph = PrefabHelper.GetInstance();
            GameObject go = ph.GetInstantiatedPrefab(PrefabType.Card);
            go.name = name;
            if (go == null) throw new System.Exception("Failed to instantiate card prefab");
            AbstractCard card = go.AddComponent(cardType) as AbstractCard;
            card.Color = color;
            card.Value = value;
            card.Cost = card.Value ?? 0;
            card.Entity = cardOwner;
            card.SpriteController = card.GetComponentInChildren<CardSprite>();
            card.SpriteController.Front = SpriteLoader.LoadSprite(frontSprite);
            card.SpriteController.Back = SpriteLoader.LoadSprite(backSprite);
            card.SpriteController.MouseEventsEnabled = mouseEventsEnabled;
            card.SpriteController.Card = card;
            card.SpriteController.SpriteRenderer.color = new(1, 1, 1, 1);
            card.SpriteController.SetupCard();
            return card;
        }

        public virtual bool IsPlayable()
        {
            return Entity.gameRules.CardIsPlayable(GameMaster.GetInstance(), Entity, this).Result;
        }

        public void OnMouseDown()
        {
            BattleEventBus.getInstance().cardTryPlayedEvent.Invoke(Entity, this);
        }

        public void OnMouseEnter()
        {
            SpriteController.MouseEnter();
        }
        public void OnMouseExit()
        {
            SpriteController.MouseLeave();
        }
    }
}