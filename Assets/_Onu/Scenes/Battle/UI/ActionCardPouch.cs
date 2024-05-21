using UnityEngine;
using UnityEngine.EventSystems;

public class ActionCardPouch : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] ActionCardPicker picker;
    [SerializeField] PickerCloser closer;

    void Awake()
    {
        closer.OnPointerClickEvent.AddListener((_) => SafeHide());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (picker.IsOpen)
        {
            picker.Hide();
            closer.gameObject.SetActive(false);
        }
        else
        {
            picker.Show();
            closer.gameObject.SetActive(true);
        }
    }

    public void SafeShow()
    {
        if (!picker.IsOpen)
        {
            picker.Show();
            closer.gameObject.SetActive(true);
        }
    }

    public void SafeHide()
    {
        if (picker.IsOpen)
        {
            picker.Hide();
            closer.gameObject.SetActive(false);
        }
    }
}