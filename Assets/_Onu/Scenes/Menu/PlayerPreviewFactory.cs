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
            _ => throw new ArgumentException("Invalid PlayerCharacter"),
        };
    }

    static PlayerPreview Character1()
    {
        return new PlayerPreview(
            name: "Character1",
            deck: DeckType.Standard,
            maxHP: 65,
            maxMana: 11,
            startingHandSize: 7,
            starterActionCards: new[] { ActionCard.Wild, },
            starterRuleCards: new[] { RuleCard.Purple, }
        );
    }
}