using System;

public enum PlayerCharacter
{
    Character1,
}

public class PlayerPreviewFactory
{
    public static PlayerPreview MakePlayerPreview(PlayerCharacter character)
    {
        return character switch
        {
            PlayerCharacter.Character1 => Character1(),
            _ => throw new ArgumentException("Invalid EnemyCharacter"),
        };
    }

    static PlayerPreview Character1()
    {
        return new()
        {
            Name = "Character1",
            Deck = DeckType.Standard,
            MaxHP = 65,
            MaxMana = 11,
            StartingHandSize = 7,
            StarterActionCards = new string[] { "Wild" },
            StarterRuleCards = new string[] { "Purple" },
        };
    }
}