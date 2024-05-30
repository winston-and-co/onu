using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// https://stackoverflow.com/questions/41391708/how-to-detect-click-touch-events-on-ui-and-gameobjects
public class MouseEventEmitter : MonoBehaviour, IPointerDownHandler,
    IPointerClickHandler, IPointerUpHandler, IPointerExitHandler,
    IPointerEnterHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public UnityEvent<PointerEventData> OnBeginDragEvent = new();
    public UnityEvent<PointerEventData> OnDragEvent = new();
    public UnityEvent<PointerEventData> OnEndDragEvent = new();
    public UnityEvent<PointerEventData> OnPointerClickEvent = new();
    public UnityEvent<PointerEventData> OnPointerDownEvent = new();
    public UnityEvent<PointerEventData> OnPointerEnterEvent = new();
    public UnityEvent<PointerEventData> OnPointerExitEvent = new();
    public UnityEvent<PointerEventData> OnPointerUpEvent = new();

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDragEvent.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragEvent.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragEvent.Invoke(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClickEvent.Invoke(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownEvent.Invoke(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEnterEvent.Invoke(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerExitEvent.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpEvent.Invoke(eventData);
    }
}