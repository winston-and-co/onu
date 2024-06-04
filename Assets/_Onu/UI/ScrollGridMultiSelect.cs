using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScrollGridMultiSelect : ScrollGrid
{
    [NonSerialized]
    public UnityEvent<List<GameObject>> ConfirmedSelection = new();

    /// <summary>
    /// The minimum number of allowed selections. Default: 0.
    /// </summary>
    public int MinSelection = 0;
    /// <summary>
    /// The maximum number of allowed selections. If -1, there is no maximum. Default: -1.
    /// </summary>
    public int MaxSelection = -1;

    #region Internal
    readonly List<GameObject> selection = new();
    [SerializeField] GameObject pickerCloserPrefab;
    GameObject pickerCloser;
    [SerializeField] GameObject outlinePrefab;
    [SerializeField] Color outlineColor;
    [SerializeField] Color hoverOutlineColor;
    GameObject hoverOutline;
    [SerializeField] Button confirmButton;
    #endregion

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
                ToggleSelection(item);
            });
            emitter.OnPointerEnterEvent.AddListener((_) =>
            {
                ShowHoverOutline(item);
            });
            emitter.OnPointerExitEvent.AddListener((_) =>
            {
                HideHoverOutline();
            });
        }
    }

    void ToggleSelection(GameObject target)
    {
        GameObject outline = GetOutline(target);
        if (outline == null)
        {
            // not selected yet
            ShowOutline(target);
            selection.Add(target);
        }
        else
        {
            // selected already
            HideOutline(outline);
            selection.Remove(target);
        }
        if (selection.Count >= MinSelection &&
            (MaxSelection == -1 || selection.Count <= MaxSelection))
        {
            confirmButton.interactable = true;
        }
        else
        {
            confirmButton.interactable = false;
        }
    }

    public void ConfirmSelection()
    {
        ConfirmedSelection.Invoke(selection);
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
        pickerCloser.GetComponent<UIWindowCloser>().OnPointerClickEvent.AddListener((_) => Hide());
        if (hoverOutline == null)
        {
            hoverOutline = Instantiate(outlinePrefab);
        }
        hoverOutline.GetComponent<Image>().color = hoverOutlineColor;
        hoverOutline.transform.SetParent(base.ScrollRect.content);
        hoverOutline.transform.SetAsFirstSibling();
    }

    public new void Hide()
    {
        Destroy(pickerCloser);
        if (hoverOutline != null)
        {
            Destroy(hoverOutline);
            hoverOutline = null;
        }
        Blockers.UIPopupBlocker.StopBlocking();
        selection.Clear();
        base.Hide();
    }

    GameObject GetOutline(GameObject target)
    {
        if (target.transform.GetSiblingIndex() == 0) return null; // out of bounds check. If first child, cant have outline
        var potentialOutline = target.transform.parent.GetChild(target.transform.GetSiblingIndex() - 1).gameObject;
        if (potentialOutline.name == "ScrollGridMultiPickerOutline")
        {
            return potentialOutline;
        }
        return null;
    }

    void ShowOutline(GameObject target)
    {
        GameObject outline;
        outline = Instantiate(outlinePrefab);
        outline.name = "ScrollGridMultiPickerOutline";
        outline.GetComponent<Image>().color = outlineColor;
        outline.transform.SetParent(base.ScrollRect.content);
        outline.transform.SetSiblingIndex(target.transform.GetSiblingIndex());
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

    void HideOutline(GameObject outline)
    {
        Destroy(outline);
    }

    void ShowHoverOutline(GameObject target)
    {
        var rt = hoverOutline.GetComponent<RectTransform>();
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
        hoverOutline.SetActive(true);
    }

    void HideHoverOutline()
    {
        hoverOutline.SetActive(false);
    }
}