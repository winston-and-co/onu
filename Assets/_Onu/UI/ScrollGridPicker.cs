using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScrollGridPicker : ScrollGrid
{
    public UnityEvent<GameObject> OnItemPicked;

    [SerializeField] GameObject pickerCloserPrefab;
    GameObject pickerCloser;
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
                OnItemPicked.Invoke(item);
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
        Blockers.UIPopupBlocker.StartBlocking();
        pickerCloser = Instantiate(pickerCloserPrefab);
        pickerCloser.transform.SetParent(transform.parent);
        ((RectTransform)pickerCloser.transform).offsetMin = new(0, 0);
        ((RectTransform)pickerCloser.transform).offsetMax = new(0, 0);
        pickerCloser.transform.SetSiblingIndex(transform.GetSiblingIndex());
        pickerCloser.GetComponent<PickerCloser>().OnPointerClickEvent.AddListener((_) => Hide());
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
        Destroy(pickerCloser);
        if (outline != null)
        {
            Destroy(outline);
            outline = null;
        }
        Blockers.UIPopupBlocker.StopBlocking();
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