using System.Collections.Generic;
using System.Linq;
using Cards;
using UnityEngine;
using UnityEngine.Events;

public class DeckMultiSelect : ScrollGridMultiSelect
{
    public new UnityEvent<List<AbstractCard>> ConfirmedSelection = new();
    List<GameObject> objects;

    void Awake()
    {
        base.Setup();
        base.ConfirmedSelection.AddListener(list =>
        {
            ConfirmedSelection.Invoke(list.Select(obj => obj.GetComponent<CardUIObject>().Card).ToList());
        });
    }

    CardComparer comparer;
    public new void Show()
    {
        var pd = PlayerData.GetInstance();
        objects = pd.Player.deck.GetCards().Select(v => v.CreateCardUIObject()).ToList();
        comparer ??= new();
        objects.Sort((a, b) =>
        {
            var cardA = a.GetComponent<CardUIObject>().Card;
            var cardB = b.GetComponent<CardUIObject>().Card;
            return comparer.Compare(cardA, cardB);
        });
        SetItems(objects);
        base.Show();
    }

    public new void Hide()
    {
        base.Hide();
        for (int i = 0; i < objects.Count; i++)
        {
            Destroy(objects[i]);
            objects.RemoveAt(i);
        }
    }
}