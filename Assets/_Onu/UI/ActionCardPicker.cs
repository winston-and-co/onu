using System.Collections.Generic;
using System.Linq;
using ActionCards;
using Blockers;
using UnityEngine;

public class ActionCardPicker : ScrollGridPicker
{
    List<GameObject> options;

    void Awake()
    {
        base.OnItemPicked.AddListener(OnActionCardPicked);
    }

    void OnActionCardPicked(GameObject go)
    {
        Hide();
        AbstractActionCard actionCard = go.GetComponent<AbstractActionCard>();
        actionCard.TryUse();
    }

    public new void Show()
    {
        options = PlayerData.GetInstance().ActionCards.Select(v => v.gameObject).ToList();
        base.SetItems(options);
        base.Show();
    }

    public new void Hide()
    {
        base.Hide();
        options.ForEach(v => v.transform.SetParent(PlayerData.GetInstance().ActionCardsParent.transform));
    }
}