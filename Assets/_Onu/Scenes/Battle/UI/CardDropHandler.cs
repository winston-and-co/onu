using Cards;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDropHandler : MonoBehaviour, IDropHandler
{
    void Awake()
    {
        EventManager.startedBattleEvent.AddListener(OnBattleStart);
        EventManager.endedBattleEvent.AddListener(OnBattleEnd);
        EventManager.cardPlayedEvent.AddListener(OnCardPlayed);
    }

    void OnBattleStart()
    {
        Hide();
    }

    void OnBattleEnd()
    {
        Hide();
    }

    void OnCardPlayed(AbstractEntity _, AbstractCard __)
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject go = eventData.pointerDrag;
        if (go.TryGetComponent(out AbstractCard card))
        {
            card.TryPlay();
        }
    }
}