using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public struct Tooltip
{
    public string Title;
    public string Body;
}

public class TooltipOwner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] List<Tooltip> tooltips = new();
    readonly List<GameObject> instances = new();

    public void Add(Tooltip tooltip)
    {
        tooltips.Add(tooltip);
    }

    public void Clear()
    {
        tooltips.Clear();
    }

    public void Copy(TooltipOwner tooltipOwner)
    {
        tooltips = tooltipOwner.tooltips;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (var tt in tooltips)
        {
            var canvas = FindObjectOfType<Canvas>();
            var rt = GetComponent<RectTransform>();
            // Get world positions of this object's top corners
            Vector3[] v = new Vector3[4];
            rt.GetWorldCorners(v);
            var topRightCornerWorldPos = v[2];
            var topLeftCornerWorldPos = v[1];
            Vector2 tooltipTargetCanvasPos;
            var tooltipInstance = PrefabHelper.GetInstance().GetInstantiatedPrefab(PrefabType.Tooltip);
            tooltipInstance.transform.SetParent(canvas.transform);
            var tooltipRt = tooltipInstance.GetComponent<RectTransform>();
            // Check if tooltip will be off screen
            canvas.GetComponent<RectTransform>().GetWorldCorners(v);
            if (topRightCornerWorldPos.x + rt.rect.width < v[2].x)
            {
                // Not off screen
                // Get TR corner position relative to canvas
                Vector2 topRightCornerCanvasPos = topRightCornerWorldPos - canvas.transform.position;
                tooltipRt.pivot = new Vector2(0, 1);
                tooltipTargetCanvasPos = topRightCornerCanvasPos + new Vector2(10, 0);
            }
            else
            {
                // Is off screen
                // Get TL corner position relative to canvas
                Vector2 topLeftCornerCanvasPos = topLeftCornerWorldPos - canvas.transform.position;
                tooltipRt.pivot = new Vector2(1, 1);
                tooltipTargetCanvasPos = topLeftCornerCanvasPos + new Vector2(-10, 0);
            }
            tooltipRt.anchorMin = new Vector2(0.5f, 0.5f);
            tooltipRt.anchorMax = new Vector2(0.5f, 0.5f);
            tooltipRt.anchoredPosition = tooltipTargetCanvasPos;
            var titleTMP = tooltipInstance.GetComponentsInChildren<TMP_Text>()[0];
            titleTMP.text = tt.Title;
            var bodyTMP = tooltipInstance.GetComponentsInChildren<TMP_Text>()[1];
            bodyTMP.text = tt.Body;
            instances.Add(tooltipInstance);
        }
        StartCoroutine(PositionTooltips());
    }

    IEnumerator PositionTooltips()
    {
        yield return new WaitForEndOfFrame();
        float yOffset = 0;
        foreach (var instance in instances)
        {
            var rt = instance.GetComponent<RectTransform>();
            rt.anchoredPosition = new(
                rt.anchoredPosition.x,
                rt.anchoredPosition.y - yOffset
            );
            yOffset = rt.sizeDelta.y;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DestroyAll();
    }

    void OnDestroy()
    {
        DestroyAll();
    }

    public void DestroyAll()
    {
        if (instances.Count > 0)
        {
            for (int i = instances.Count - 1; i >= 0; i--)
            {
                var instance = instances[i];
                Destroy(instance);
            }
            instances.Clear();
        }
    }
}