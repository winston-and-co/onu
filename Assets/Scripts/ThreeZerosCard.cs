using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;

public class ThreeZerosCard : MonoBehaviour
{
    public void Play()
    {
        GameObject c = GameObject.Instantiate(PlayerData.GetInstance().Player.deck.card_prefab);
        for (int i = 0; i < 3; i++)
        {
            c.GetComponent<AbstractCard>().Color = CardColor.Red;
            c.GetComponent<AbstractCard>().Value = 0;
            PlayerData.GetInstance().Player.deck.AddCard(c.GetComponent<AbstractCard>());
            c.SetActive(false);
            c.transform.parent = PlayerData.GetInstance().Player.deck.transform;
        }
    }
}
