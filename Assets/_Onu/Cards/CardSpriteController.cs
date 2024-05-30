using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Cards;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;

public class CardSpriteController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    bool initialMouseEventsEnabled;
    public bool MouseEventsEnabled;
    bool initialFlipped;
    public bool Flipped;
    public SpriteRenderer SpriteRenderer;
    public Sprite Front;
    public Sprite Back;
    public TextMeshPro CardText;
    public string HandSortingLayer;
    public AbstractCard Card;
    public Animator Animator;
    bool initialDraggable;
    public bool Draggable = true;
    Vector3 prevPos;

    [SerializeField] private int lastOrder;

    public Vector3 CardInHandScale = new Vector3(0.3f, 0.3f, 1.0f);
    public Vector3 CardInPileScale = new Vector3(0.2f, 0.2f, 1.0f);
    public float EnlargedSizeFactor = 1.05f;

    void Awake()
    {
        EventManager.startBattleEvent.AddListener(OnStartBattle);
        EventManager.cardPlayedEvent.AddListener(AfterCardPlayed);
        EventManager.cardDrawnEvent.AddListener(OnCardDrawn);

        if (!TryGetComponent<SpriteRenderer>(out SpriteRenderer)) throw new System.NullReferenceException();

        Animator = GetComponent<Animator>();
    }

    public void Init()
    {
        initialMouseEventsEnabled = MouseEventsEnabled;
        initialDraggable = Draggable;
        initialFlipped = Flipped;
        SetupCard();
    }

    void SetupCard()
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

    void OnStartBattle(GameMaster _)
    {
        MouseEventsEnabled = initialMouseEventsEnabled;
        Draggable = initialDraggable;
        Flipped = initialFlipped;
        SetupCard();
    }

    void AfterCardPlayed(AbstractEntity _, AbstractCard p)
    {
        if (p != Card) return;
        MouseEventsEnabled = false;
        Draggable = false;
        Card.transform.SetParent(GameMaster.GetInstance().DiscardPile.transform, false);
        Card.transform.localPosition = Vector3.zero;
        Flipped = false;
        SetupCard();
        var rt = GetComponent<RectTransform>();
        rt.localScale = CardInPileScale;
        rt.SetAsLastSibling();
        var sg = GetComponent<SortingGroup>();
        sg.sortingLayerName = "OnTable";
        SetOrder(rt.GetSiblingIndex());
    }

    void OnCardDrawn(AbstractEntity e, AbstractCard c)
    {
        if (c != Card) return;
        cardDropHandler = FindObjectOfType<CardDropHandler>(true);
        var rt = GetComponent<RectTransform>();
        rt.localScale = CardInHandScale;
        foreach (var r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = true;
        }
        var tmp = GetComponentInChildren<TextMeshPro>();
        tmp.enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        var sg = GetComponent<SortingGroup>();
        sg.sortingLayerName = HandSortingLayer;
    }

    public void Flip()
    {
        Flipped = !Flipped;
        SetupCard();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (MouseEventsEnabled)
        {
            Animator.SetTrigger("CardHover");
            lastOrder = GetComponent<SortingGroup>().sortingOrder;
            transform.localScale = CardInHandScale * EnlargedSizeFactor;
            SetOrder(9999);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (MouseEventsEnabled)
        {
            transform.localScale = CardInHandScale;
            SetOrder(lastOrder);
        }
    }

    CardDropHandler cardDropHandler;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (MouseEventsEnabled && Draggable)
        {
            prevPos = transform.position;
            cardDropHandler.Show();
            SetDraggedPosition(eventData);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (MouseEventsEnabled && Draggable)
        {
            transform.position = prevPos;
            cardDropHandler.Hide();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (MouseEventsEnabled && Draggable)
        {
            SetDraggedPosition(eventData);
        }
    }

    // https://docs.unity3d.com/2018.3/Documentation/ScriptReference/EventSystems.IDragHandler.html
    void SetDraggedPosition(PointerEventData data)
    {
        var rt = GetComponent<RectTransform>();
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, data.position, data.pressEventCamera, out Vector3 globalMousePos))
        {
            rt.position = globalMousePos;
        }
    }

    public void SetOrder(int order)
    {
        var sg = GetComponent<SortingGroup>();
        sg.sortingOrder = order;
    }
}
