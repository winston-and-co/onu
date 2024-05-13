using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Cards;

public class CardSprite : MonoBehaviour
{
    public bool MouseEventsEnabled;
    public bool Flipped;
    public SpriteRenderer SpriteRenderer;
    public Sprite Front;
    public Sprite Back;
    public TextMeshPro CardText;
    public AbstractCard Card;

    protected int Order;
    private int lastOrder;

    public Vector3 cardInHandScale = new Vector3(0.3f, 0.3f, 1.0f);
    public Vector3 cardInPileScale = new Vector3(0.2f, 0.2f, 1.0f);
    public Vector3 enlargedScale = new Vector3(1.2f, 1.2f, 1.2f);
    public Vector3 originalScale = new Vector3(1.0f, 1.0f, 1.0f);

    void Awake()
    {
        BattleEventBus.getInstance().afterCardPlayedEvent.AddListener(AfterCardPlayed);
        BattleEventBus.getInstance().cardDrawEvent.AddListener(OnCardDrawn);

        if (!TryGetComponent<SpriteRenderer>(out SpriteRenderer)) throw new System.NullReferenceException();

        // SetupCard();
    }

    public void SetupCard()
    {
        if (!Flipped)
        {
            SpriteRenderer.sprite = Front;
            SpriteRenderer.color = Card.Color;
            if (CardText != null)
            {
                CardText.text = Card.Value.HasValue ? Card.Value.ToString() : "";
                CardText.color = Color.white.WithAlpha(1.0f);
                // https://forum.unity.com/threads/asset-text-mesh-pro-api-outline.503171/
                CardText.fontSharedMaterial.shaderKeywords = new string[] { "OUTLINE_ON" };
                CardText.outlineColor = Color.black;
                CardText.outlineWidth = 0.2f;
            }
        }
        else
        {
            SpriteRenderer.sprite = Back;
            SpriteRenderer.color = Color.white;
            if (CardText != null)
            {
                CardText.text = null;
            }
        }
    }

    void AfterCardPlayed(AbstractEntity _, AbstractCard p)
    {
        if (p != Card) return;
        Flipped = false;
        SetupCard();
        var rt = GetComponentsInParent<RectTransform>()[1];
        rt.localScale = cardInPileScale;

        var items = GameMaster.GetInstance().discard.Items;
        if (items.Count > 1)
        {
            var secondFromTop = items[items.Count - 2];
            foreach (var r in secondFromTop.GetComponentsInChildren<Renderer>())
            {
                r.enabled = false;
            }
            var tmp = secondFromTop.GetComponentInChildren<TextMeshPro>();
            if (tmp != null)
            {
                tmp.enabled = false;
            }
        }
        GetComponentInParent<BoxCollider2D>().enabled = false;
    }

    void OnCardDrawn(AbstractEntity e, AbstractCard c)
    {
        if (c != Card) return;

        var rt = GetComponentsInParent<RectTransform>()[1];
        rt.localScale = cardInHandScale;
        foreach (var r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = true;
        }
        var tmp = GetComponentInChildren<TextMeshPro>();
        tmp.enabled = true;
        GetComponentInParent<BoxCollider2D>().enabled = true;
    }

    public void Flip()
    {
        Flipped = !Flipped;
        SetupCard();
    }

    public void MouseEnter()
    {
        if (MouseEventsEnabled)
        {
            lastOrder = SpriteRenderer.sortingOrder;
            transform.localScale = enlargedScale;
            SetOrder(9999);
        }
    }

    public void MouseLeave()
    {
        if (MouseEventsEnabled)
        {
            transform.localScale = originalScale;
            SetOrder(lastOrder);
        }
    }

    public void SetOrder(int order)
    {
        if (SpriteRenderer != null)
            SpriteRenderer.sortingOrder = order;
        if (CardText != null)
            CardText.sortingOrder = order + 1;
        Order = order;
    }
}
