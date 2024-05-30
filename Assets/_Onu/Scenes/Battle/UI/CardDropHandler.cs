using Cards;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDropHandler : MonoBehaviour, IDropHandler
{
    void Awake()
    {
        EventManager.startBattleEvent.AddListener(OnBattleStart);
        EventManager.endBattleEvent.AddListener(OnBattleEnd);
        EventManager.cardPlayedEvent.AddListener(OnCardPlayed);
    }

    void OnBattleStart(GameMaster _)
    {
        Hide();
    }

    void OnBattleEnd(GameMaster _)
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