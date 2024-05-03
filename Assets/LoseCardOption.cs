using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCardOption : MonoBehaviour
{
    
    [SerializeField] int num_lost = 2;
    public void Play()
    {
        GetComponentInParent<BathroomOptions>().OptionPicked(gameObject);
        Entity p = PlayerData.GetInstance().Player;
        Deck d = p.deck;

        for (int i = 0; i < num_lost; i++)
        {
            Card c = d.Remove(0);
            Destroy(c.gameObject);
        }
    }
}
