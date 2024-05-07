using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string Title;
    public string Body;
    static GameObject tooltipInstance;

    public void OnPointerEnter(PointerEventData eventData)
    {
        var canvas = FindObjectOfType<Canvas>();
        var rt = GetComponent<RectTransform>();
        // Get world positions of this object's top corners
        Vector3[] v = new Vector3[4];
        rt.GetWorldCorners(v);
        var topRightCornerWorldPos = v[2];
        var topLeftCornerWorldPos = v[1];
        Vector2 tooltipTargetCanvasPos;
        if (tooltipInstance != null) Destroy(tooltipInstance);
        tooltipInstance = Instantiate(Resources.Load<GameObject>("Prefabs/Tooltip"));
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
        titleTMP.text = Title;
        var bodyTMP = tooltipInstance.GetComponentsInChildren<TMP_Text>()[1];
        bodyTMP.text = Body;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltipInstance != null) Destroy(tooltipInstance);
    }

    void OnDestroy()
    {
        Destroy(tooltipInstance);
    }
}