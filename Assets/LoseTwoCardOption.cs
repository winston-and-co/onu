using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseTwoCardOption : MonoBehaviour
{
    public void Play()
    {
        Entity p = PlayerData.GetInstance().Player;
        Deck d = p.deck;

        for(int i = 0; i < 2; i++)
        {
            Card c = d.Remove(0);
            Destroy(c.gameObject);
        }
    }
}
