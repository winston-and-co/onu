using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScrollGrid : MonoBehaviour
{
    [SerializeField] List<GameObject> Items;

    public int spacingBetween = 30;
    public int columns = 5;
    public ScrollRect scrollRect;

    void Awake()
    {
        if (scrollRect == null && !TryGetComponent(out scrollRect))
        {
            throw new System.NullReferenceException("ScrollRect component is missing.");
        }
    }

    public List<GameObject> GetItems()
    {
        return Items;
    }

    public void SetItems(List<GameObject> items)
    {
        Items = items;
        float maxWidth = 0;
        float rowYPos = spacingBetween;
        for (int row = 0; row < Items.Count / columns + 1; row++)
        {
            // rows
            float rowWidth = 0;
            float rowHeight = 0;
            for (int col = 0; col < columns; col++)
            {
                if (row * columns + col >= Items.Count) break;
                // cols
                var item = Items[row * columns + col];
                var rt = item.GetComponent<RectTransform>();
                rt.anchorMin = new Vector2(0, 1);
                rt.anchorMax = new Vector2(0, 1);
                rt.pivot = new Vector2(0, 1);
                float xPos;
                if (col == 0)
                {
                    xPos = spacingBetween;
                }
                else
                {
                    var prevItem = Items[row * columns + col - 1];
                    var prevRt = prevItem.GetComponent<RectTransform>();
                    xPos = prevRt.anchoredPosition.x + prevRt.rect.width + spacingBetween;
                }
                rt.SetParent(scrollRect.content);
                rt.anchoredPosition = new Vector2(xPos, -rowYPos);
                rowWidth += spacingBetween + rt.rect.width;
                if (spacingBetween + rt.rect.height > rowHeight)
                {
                    rowHeight = spacingBetween + rt.rect.height;
                }
            }
            rowYPos += rowHeight;
            if (rowWidth > maxWidth)
            {
                maxWidth = rowWidth;
            }
        }
        var sizeVertical = rowYPos;
        scrollRect.content.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Vertical,
            sizeVertical
        );
        var sizeHorizontal = maxWidth + spacingBetween
            + scrollRect.GetComponentInChildren<Scrollbar>()
                        .GetComponent<RectTransform>().rect.width;
        scrollRect.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal,
            sizeHorizontal
        );
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}