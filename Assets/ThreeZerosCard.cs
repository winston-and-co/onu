using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeZerosCard : MonoBehaviour
{
    public void Play()
    {
        GameObject c = GameObject.Instantiate(PlayerData.GetInstance().Player.deck.card_prefab);
        for(int i = 0; i < 3; i++)
        {
            c.GetComponent<Card>().Color = CardColors.Red;
            c.GetComponent<Card>().Value.Value = 0;
            PlayerData.GetInstance().Player.deck.Add(c.GetComponent<Card>());
            c.SetActive(false);
            c.transform.parent = PlayerData.GetInstance().Player.deck.transform;
        }
    }
}
