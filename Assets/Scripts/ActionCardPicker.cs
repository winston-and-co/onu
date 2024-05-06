using System.Linq;
using ActionCards;
using Blockers;
using UnityEngine;

public class ActionCardPicker : ScrollGridPicker
{
    void Awake()
    {
        base.OnItemPicked.AddListener(OnActionCardPicked);
    }

    void OnActionCardPicked(GameObject go)
    {
        ActionCardBase actionCard = go.GetComponent<ActionCardBase>();
        Hide();
        if (actionCard is IUsable usable)
        {
            usable.TryUse();
        }
    }

    public new void Show()
    {
        UIPopupBlocker.StartBlocking();
        base.SetItems(PlayerData.GetInstance().ActionCards
            .Select(ac => ac.gameObject).ToList());
        base.Show();
    }

    public new void Hide()
    {
        UIPopupBlocker.StopBlocking();
        base.Hide();
    }
}