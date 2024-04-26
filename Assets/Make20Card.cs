using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Make20Card : MonoBehaviour
{
    public void Play()
    {
        Deck d = PlayerData.GetInstance().Player.deck;
        Card c = d.get(UnityEngine.Random.Range(0, d.size()));
        c.Value.Value = 20;
    }
}
