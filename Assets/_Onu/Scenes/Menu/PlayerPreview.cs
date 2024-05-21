using System;
using System.Linq;
using ActionCards;
using RuleCards;

public class PlayerPreview
{
    public string Name;
    public RuleCard[] StarterRuleCards;
    public AbstractRuleCard[] StarterRuleCardInstances;
    public ActionCard[] StarterActionCards;
    public AbstractActionCard[] StarterActionCardInstances;
    public DeckType Deck;
    public int MaxHP;
    public int MaxMana;
    public int StartingHandSize;

    public PlayerPreview(string name, RuleCard[] starterRuleCards, ActionCard[] starterActionCards, DeckType deck, int maxHP, int maxMana, int startingHandSize)
    {
        Name = name;
        StarterRuleCards = starterRuleCards;
        StarterActionCards = starterActionCards;
        Deck = deck;
        MaxHP = maxHP;
        MaxMana = maxMana;
        StartingHandSize = startingHandSize;
    }

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
            StarterRuleCardInstances = StarterRuleCards.Select(v => RuleCardFactory.MakeRuleCard(v)).ToArray();
            foreach (var rc in StarterRuleCardInstances)
            {
                rc.gameObject.SetActive(false);
                ret += $"\n{rc.Name}";
            }
            ret += "\n";
        }
        if (StarterActionCards.Length > 0)
        {
            ret += "\nAction Cards:";
            StarterActionCardInstances = StarterActionCards.Select(v => ActionCardFactory.MakeActionCard(v)).ToArray();
            foreach (var ac in StarterActionCardInstances)
            {
                ac.gameObject.SetActive(false);
                ret += $"\n{ac.Name}";
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
        return p;
    }
}