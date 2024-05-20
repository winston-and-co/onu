using System;

public class PlayerPreview
{
    public string Name;
    public string[] StarterRuleCards;
    public string[] StarterActionCards;
    public DeckType Deck;
    public int MaxHP;
    public int MaxMana;
    public int StartingHandSize;

    public override string ToString()
    {
        string ret = $@"{Name}

Max HP: {MaxHP}
Max Mana: {MaxMana}

Starting Hand Size: {StartingHandSize}

Deck: {Enum.GetName(typeof(DeckType), Deck)}
";
        if (StarterRuleCards.Length > 0)
        {
            ret += "\nRule Cards:";
            foreach (string rc in StarterRuleCards)
            {
                if (!RuleCardFactory.CheckExists(rc))
                    throw new ArgumentException();
                ret += $"\n{rc}";
            }
            ret += "\n";
        }
        if (StarterActionCards.Length > 0)
        {
            ret += "\nAction Cards:";
            foreach (string ac in StarterActionCards)
            {
                if (!ActionCardFactory.CheckExists(ac))
                    throw new ArgumentException();
                ret += $"\n{ac}";
            }
        }
        return ret;
    }

    public PlayerEntity ToPlayerEntity()
    {
        PlayerEntity p = PlayerEntity.New(
            playerName: Name,
            deckType: Deck,
            maxHP: MaxHP,
            maxMana: MaxMana,
            startingHandSize: StartingHandSize
        );
        foreach (string rc in StarterRuleCards)
        {
            if (!RuleCardFactory.CheckExists(rc))
                throw new ArgumentException();
            p.gameRulesController.AddRuleCard(RuleCardFactory.MakeRuleCard(rc));
        }
        foreach (string ac in StarterActionCards)
        {
            if (!ActionCardFactory.CheckExists(ac))
                throw new ArgumentException();
            PlayerData.GetInstance().AddActionCard(ac);
        }
        return p;
    }
}