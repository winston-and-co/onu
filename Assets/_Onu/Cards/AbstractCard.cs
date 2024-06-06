using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    public abstract class AbstractCard : MonoBehaviour
    {
        public int Cost;
        int? _value;
        public int? Value
        {
            get => _value;
            set
            {
                _value = value;
                if (SpriteController != null)
                    SpriteController.Redraw();
            }
        }
        [SerializeField] Color _color;
        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                if (SpriteController != null)
                    SpriteController.Redraw();
            }
        }
        public CardSpriteController SpriteController;
        public AbstractEntity Entity;
        public bool GeneratedInCombat;

        public static AbstractCard New(string name, Color color, int? value, AbstractEntity cardOwner, System.Type cardType, string frontSprite, string backSprite, bool mouseEventsEnabled, bool generatedInCombat)
        {
            PrefabLoader ph = PrefabLoader.GetInstance();
            GameObject go = ph.InstantiatePrefab(PrefabType.Card);
            go.name = name;
            if (go == null) throw new System.Exception("Failed to instantiate card prefab");
            AbstractCard card = go.AddComponent(cardType) as AbstractCard;
            card.Color = color;
            card.Value = value;
            card.Cost = card.Value ?? 0;
            card.Entity = cardOwner;
            card.GeneratedInCombat = generatedInCombat;
            card.SpriteController = card.GetComponent<CardSpriteController>();
            card.SpriteController.Front = SpriteLoader.LoadSprite(frontSprite);
            card.SpriteController.Back = SpriteLoader.LoadSprite(backSprite);
            card.SpriteController.MouseEventsEnabled = mouseEventsEnabled;
            card.SpriteController.Card = card;
            card.SpriteController.SpriteRenderer.color = new(1, 1, 1, 1);
            card.SpriteController.Init();
            return card;
        }

        /// <summary>
        /// Creates a new game object to show on canvas.
        /// </summary>
        /// <param name="facingFront"></param>
        /// <returns>The GameObject instance</returns>
        public GameObject CreateCardUIObject(bool facingFront = true)
        {
            var go = PrefabLoader.GetInstance().InstantiatePrefab(PrefabType.CardUI);
            go.GetComponent<CardUIObject>().Card = this;
            Image image = go.GetComponent<Image>();
            image.sprite = SpriteLoader.LoadSprite(facingFront ? "alpha_art_card_front" : "alpha_art_card_back");
            image.color = Color;
            var text = go.GetComponentInChildren<TMP_Text>();
            text.text = Value.HasValue ? Value.ToString() : "";
            text.color = Color.white;
            text.fontSharedMaterial.shaderKeywords = new string[] { "OUTLINE_ON" };
            text.outlineColor = Color.black;
            text.outlineWidth = 0.2f;
            return go;
        }

        public void TryPlay()
        {
            GameMaster.GetInstance().TryPlayCard(this);
        }

        public virtual bool IsPlayable()
        {
            return Entity.gameRulesController.CardIsPlayable(GameMaster.GetInstance(), Entity, this).Result;
        }
    }
}