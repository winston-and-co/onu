using System.Collections.Generic;
using System.Linq;
using Cards;
using UnityEngine;

public class DeckViewer : ScrollGrid
{
    List<GameObject> objects;

    void Awake()
    {
        base.Setup();
    }

    CardComparer comp;
    public new void Show()
    {
        var pd = PlayerData.GetInstance();
        objects = pd.Player.deck.GetCards().Select(v => v.CreateCardUIObject()).ToList();
        comp ??= new();
        objects.Sort((a, b) =>
        {
            var cardA = a.GetComponent<CardUIObject>().Card;
            var cardB = b.GetComponent<CardUIObject>().Card;
            return comp.Compare(cardA, cardB);
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