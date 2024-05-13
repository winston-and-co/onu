using System;

public enum PlayerCharacter
{
    Character1,
}

public class PlayerFactory
{
    public static PlayerEntity MakePlayer(PlayerCharacter character)
    {
        return character switch
        {
            PlayerCharacter.Character1 => Character1(),
            _ => throw new ArgumentException("Invalid EnemyCharacter"),
        };
    }

    static PlayerEntity Character1()
    {
        PlayerEntity player = PlayerEntity.New(
            playerName: "Character1",
            deckType: DeckType.Standard,
            maxHP: 65,
            maxMana: 11,
            startingHandSize: 7
        );
        // ADD RULECARD THAT HEALS YOU WHEN YOU PLAY ACs
        return player;
    }
}