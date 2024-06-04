using UnityEngine;
using UnityEngine.EventSystems;

public abstract class AbstractShadyManService : MonoBehaviour, IPointerClickHandler
{
    public void Done()
    {
        FindObjectOfType<ShadyMan>().ServiceDone(gameObject);
    }

    public abstract bool ConditionMet();

    public abstract void OnPointerClick(PointerEventData eventData);
}