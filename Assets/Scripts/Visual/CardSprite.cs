using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CardSprite : MonoBehaviour
{
    [SerializeField] bool EnableMouseEvents;
    public bool flipped;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite front;
    [SerializeField] Sprite back;
    [SerializeField] TextMeshPro textMeshPro;
    public Playable playable;

    protected int Order;
    private int lastOrder;

    public Vector3 enlargedScale = new Vector3(0.31f, 0.31f, 0.31f);
    public Vector3 originalScale = new Vector3(0.29f, 0.29f, 0.29f);

    public void OnEnable()
    {
        if (spriteRenderer == null) throw new System.NullReferenceException();

        SetupCard();
    }

    public void SetupCard()
    {
        if (!flipped)
        {
            spriteRenderer.sprite = front;
            spriteRenderer.color = playable.Color;
            if (textMeshPro != null)
            {
                textMeshPro.text = playable.Value.ToString();
                textMeshPro.color = Color.white.WithAlpha(1.0f);
                // https://forum.unity.com/threads/asset-text-mesh-pro-api-outline.503171/
                textMeshPro.fontSharedMaterial.shaderKeywords = new string[] { "OUTLINE_ON" };
                textMeshPro.outlineColor = Color.black;
                textMeshPro.outlineWidth = 0.2f;
            }
        }
        else
        {
            spriteRenderer.sprite = back;
            spriteRenderer.color = Color.white;
            if (textMeshPro != null)
            {
                textMeshPro.text = null;
            }
        }
    }

    public void Awake()
    {
        BattleEventBus.getInstance().cardPlayedEvent.AddListener(OnCardPlayed);
    }

    void OnCardPlayed(Entity _, Playable p)
    {
        if (p != playable) return;
        flipped = false;
        SetupCard();
        var rt = GetComponentsInParent<RectTransform>()[1];
        rt.localScale = new Vector3(0.2f, 0.2f, 1.0f);
    }

    public void Flip()
    {
        flipped = !flipped;
        SetupCard();
    }

    public void MouseEnter()
    {
        if (EnableMouseEvents)
        {
            lastOrder = spriteRenderer.sortingOrder;
            transform.localScale = enlargedScale;
            SetOrder(9999);
        }
    }

    public void MouseLeave()
    {
        if (EnableMouseEvents)
        {
            transform.localScale = originalScale;
            SetOrder(lastOrder);
        }
    }

    public void SetOrder(int order)
    {
        if (spriteRenderer != null)
            spriteRenderer.sortingOrder = order;
        if (textMeshPro != null)
            textMeshPro.sortingOrder = order + 1;
        Order = order;
    }
}
