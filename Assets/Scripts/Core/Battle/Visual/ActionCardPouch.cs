using UnityEngine;
using UnityEngine.EventSystems;

public class ActionCardPouch : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] ActionCardPicker picker;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (picker.IsOpen)
            picker.Hide();
        else
            picker.Show();
    }

    public void SafeShow()
    {
        if (!picker.IsOpen) picker.Show();
    }

    public void SafeHide()
    {
        if (picker.IsOpen) picker.Hide();
    }
}