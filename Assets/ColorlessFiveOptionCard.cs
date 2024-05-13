using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards;

public class ColorlessFiveOptionCard : MonoBehaviour
{
    // Start is called before the first frame update
    public void Play()
    {
        GameObject c = GameObject.Instantiate(PlayerData.GetInstance().Player.deck.card_prefab);
        c.GetComponent<AbstractCard>().Color = CardColor.Colorless;
        c.GetComponent<AbstractCard>().Value = 5;
        PlayerData.GetInstance().Player.deck.Add(c.GetComponent<AbstractCard>());
        c.SetActive(false);
        c.transform.parent = PlayerData.GetInstance().Player.deck.transform;

    }
}
