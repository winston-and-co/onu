using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;

public class Make20Card : MonoBehaviour
{
    public void Play()
    {
        Deck d = PlayerData.GetInstance().Player.deck;
        AbstractCard c = d.Get(UnityEngine.Random.Range(0, d.Size()));
        c.Value = 20;
    }
}
