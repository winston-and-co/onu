using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionCardPouch : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    ActionCardPicker picker;

    void Awake()
    {
        var go = PrefabHelper.GetInstance().InstantiatePrefab(PrefabType.UI_ActionCardPicker);
        go.transform.SetParent(transform.parent, false);
        go.transform.SetSiblingIndex(transform.GetSiblingIndex());
        picker = go.GetComponent<ActionCardPicker>();
        picker.Hide();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Shake();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (picker.IsOpen)
        {
            SafeHide();
        }
        else
        {
            SafeShow();
        }
    }

    public void SafeShow()
    {
        if (PlayerData.GetInstance().ActionCards.Count == 0)
        {
            Shake();
            return;
        }
        if (!picker.IsOpen)
        {
            picker.Show();
        }
    }

    public void SafeHide()
    {
        if (picker.IsOpen)
        {
            picker.Hide();
        }
    }

    void Shake()
    {
        ((RectTransform)transform).DOShakePosition(0.1f, new Vector2(3, 0), 20);
    }
}