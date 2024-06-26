using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScrollGridSelect : ScrollGrid
{
    [NonSerialized]
    public UnityEvent<GameObject> ItemSelected = new();

    [SerializeField] GameObject outlinePrefab;
    [SerializeField] Color outlineColor;
    GameObject outline;

    public new void SetItems(List<GameObject> items)
    {
        base.SetItems(items);
        foreach (var item in base.GetItems())
        {
            if (!item.TryGetComponent<BoxCollider2D>(out var collider))
                collider = item.AddComponent<BoxCollider2D>();
            if (!item.TryGetComponent<MouseEventEmitter>(out var emitter))
                emitter = item.AddComponent<MouseEventEmitter>();
            emitter.OnPointerClickEvent.AddListener((_) =>
            {
                ItemSelected.Invoke(item);
            });
            emitter.OnPointerEnterEvent.AddListener((_) =>
            {
                ShowOutline(item);
            });
            emitter.OnPointerExitEvent.AddListener((_) =>
            {
                HideOutline();
            });
        }
    }

    public new void Show()
    {
        base.Show();
        if (outline == null)
        {
            outline = Instantiate(outlinePrefab);
        }
        outline.GetComponent<Image>().color = outlineColor;
        outline.transform.SetParent(base.ScrollRect.content);
        outline.transform.SetAsFirstSibling();
    }

    public new void Hide()
    {
        if (outline != null)
        {
            Destroy(outline);
            outline = null;
        }
        base.Hide();
    }

    void ShowOutline(GameObject target)
    {
        var rt = outline.GetComponent<RectTransform>();
        var targetRt = target.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 1);
        rt.anchorMax = new Vector2(0, 1);
        rt.pivot = new Vector2(0, 1);
        rt.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal,
            targetRt.rect.width + 10
        );
        rt.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Vertical,
            targetRt.rect.height + 10
        );
        rt.anchoredPosition = targetRt.anchoredPosition + new Vector2(-5, 5);
        outline.SetActive(true);
    }

    void HideOutline()
    {
        outline.SetActive(false);
    }
}