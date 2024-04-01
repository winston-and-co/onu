using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CardSprite : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public TextMeshPro textMeshPro;
    public Card c;

    protected int Order;

    private int lastOrder;

    public Vector3 enlargedScale = new Vector3(0.31f, 0.31f, 0.31f);
    public Vector3 originalScale = new Vector3(0.29f, 0.29f, 0.29f);


    public void SetupCard()
    {
        textMeshPro.text = c.value.ToString();
        textMeshPro.color = c.color.WithAlpha(1.0f);
    }

    public void MouseEnter()
    {
        lastOrder = spriteRenderer.sortingOrder;
        transform.localScale = enlargedScale;
        SetOrder(9999);
    }

    public void MouseLeave()
    {
        transform.localScale = originalScale;
        SetOrder(lastOrder);
    }

    public void SetOrder(int order)
    {
        spriteRenderer.sortingOrder = order;
        textMeshPro.sortingOrder = order + 1;
        Order = order;
    }

    public void OnEnable()
    {
        SetupCard();

    }
}


