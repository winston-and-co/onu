using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;

public class LoseTwoCardOption : MonoBehaviour
{
    public void Play()
    {
        AbstractEntity p = PlayerData.GetInstance().Player;
        Deck d = p.deck;

        for (int i = 0; i < 2; i++)
        {
            AbstractCard c = d.Remove(0);
            Destroy(c.gameObject);
        }
    }
}
