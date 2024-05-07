using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScrollGrid : MonoBehaviour
{
    [SerializeField] List<GameObject> Items;
    public bool IsOpen = false;
    public int Spacing = 30;
    public int NumColumns = 5;
    public ScrollRect ScrollRect;

    void Awake()
    {
        if (ScrollRect == null && !TryGetComponent(out ScrollRect))
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
        float rowYPos = Spacing;
        for (int row = 0; row < Items.Count / NumColumns + 1; row++)
        {
            // rows
            float rowWidth = 0;
            float rowHeight = 0;
            for (int col = 0; col < NumColumns; col++)
            {
                if (row * NumColumns + col >= Items.Count) break;
                // cols
                var item = Items[row * NumColumns + col];
                var rt = item.GetComponent<RectTransform>();
                rt.anchorMin = new Vector2(0, 1);
                rt.anchorMax = new Vector2(0, 1);
                rt.pivot = new Vector2(0, 1);
                float xPos;
                if (col == 0)
                {
                    xPos = Spacing;
                }
                else
                {
                    var prevItem = Items[row * NumColumns + col - 1];
                    var prevRt = prevItem.GetComponent<RectTransform>();
                    xPos = prevRt.anchoredPosition.x + prevRt.rect.width + Spacing;
                }
                rt.SetParent(ScrollRect.content);
                rt.anchoredPosition = new Vector2(xPos, -rowYPos);
                rowWidth += Spacing + rt.rect.width;
                if (Spacing + rt.rect.height > rowHeight)
                {
                    rowHeight = Spacing + rt.rect.height;
                }
            }
            rowYPos += rowHeight;
            if (rowWidth > maxWidth)
            {
                maxWidth = rowWidth;
            }
        }
        var sizeVertical = rowYPos;
        ScrollRect.content.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Vertical,
            sizeVertical
        );
        var sizeHorizontal = maxWidth + Spacing
            + ScrollRect.GetComponentInChildren<Scrollbar>(true)
                        .GetComponent<RectTransform>().rect.width;
        var srrt = ScrollRect.GetComponent<RectTransform>();
        srrt.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Vertical,
            sizeVertical
        );
        srrt.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal,
            sizeHorizontal
        );
    }

    public void Show()
    {
        gameObject.SetActive(true);
        IsOpen = true;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        IsOpen = false;
    }
}