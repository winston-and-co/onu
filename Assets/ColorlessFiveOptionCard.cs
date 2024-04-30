using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorlessFiveOptionCard : MonoBehaviour
{
    // Start is called before the first frame update
    public void Play()
    {
        GameObject c = GameObject.Instantiate(PlayerData.GetInstance().Player.deck.card_prefab);
        c.GetComponent<Card>().Color = CardColors.Colorless;
        c.GetComponent<Card>().Value.Value = 5;
        PlayerData.GetInstance().Player.deck.Add(c.GetComponent<Card>());
        c.SetActive(false);
        c.transform.parent = PlayerData.GetInstance().Player.deck.transform;

    }
}
