using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;

public class Make20Card : MonoBehaviour
{
    public void Play()
    {
        Deck d = PlayerData.GetInstance().Player.deck;
        AbstractCard c = d.get(UnityEngine.Random.Range(0, d.size()));
        c.Value = 20;
    }
}
