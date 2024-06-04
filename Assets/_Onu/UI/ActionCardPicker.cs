using System.Collections.Generic;
using System.Linq;
using ActionCards;
using UnityEngine;

public class ActionCardPicker : ScrollGridSelect
{
    List<GameObject> options = new();

    void Awake()
    {
        base.Setup();
        base.ItemSelected.AddListener(OnActionCardPicked);
    }

    void OnActionCardPicked(GameObject go)
    {
        Hide();
        AbstractActionCard actionCard = go.GetComponent<AbstractActionCard>();
        actionCard.TryUse();
    }

    public new void Show()
    {
        options = PlayerData.GetInstance().GetActionCardInstances().Select(v => v.gameObject).ToList();
        base.SetItems(options);
        base.Show();
    }

    public new void Hide()
    {
        base.Hide();
        for (int i = 0; i < options.Count; i++)
        {
            Destroy(options[i]);
            options.RemoveAt(i);
        }
    }
}