using System;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    static PlayerEntity preview;

    public void ConfirmSelection()
    {
        if (PlayerData.GetInstance().Player != null)
            throw new System.Exception("Why are there 2 players???");
        if (preview == null)
            return;
        var pd = PlayerData.GetInstance();
        if (pd.Player == null)
        {
            pd.Player = preview;
            DontDestroyOnLoad(pd.Player);
        }
    }

    public PlayerEntity PreviewPlayerCharacter(PlayerCharacter character)
    {
        PlayerData.GetInstance().ActionCards.Clear();
        PlayerEntity player;
        switch (character)
        {
            case PlayerCharacter.Character1:
                player = PlayerFactory.MakePlayer(PlayerCharacter.Character1);
                PlayerData.GetInstance().AddActionCard("Wild");
                break;
            default:
                throw new ArgumentException();
        }
        preview = player;
        return player;
    }
}