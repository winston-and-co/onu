using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    Deck playerDeck;
    Deck enemyDeck;
    Hand playerHand;
    Hand enemyHand;
    int startingHandSize;


    private void Start()
    {
        for(int i = 0; i < startingHandSize; i++)
        {
            playerHand.AddCard(GetPlayerDeck().Draw());
            enemyHand.AddCard(GetPlayerDeck().Draw());
        }     
        
      
    }

    Deck GetPlayerDeck()
    {
        return playerDeck;

    }

    Deck GetEnemyDeck()
    {
        return enemyDeck;
    }

    bool TryPlay(Move m)
    {
        throw new NotImplementedException();
    }
}
