using System;
using System.Linq;
using ActionCards;
using RuleCards;

public class PlayerPreview
{
    public string Name;
    public int[] StarterRuleCards;
    public AbstractRuleCard[] StarterRuleCardInstances;
    public int[] StarterActionCards;
    AbstractActionCard[] StarterActionCardInstances;
    public DeckType Deck;
    public int MaxHP;
    public int MaxMana;
    public int StartingHandSize;

    public PlayerPreview(string name, int[] starterRuleCards, int[] starterActionCards, DeckType deck, int maxHP, int maxMana, int startingHandSize)
    {
        Name = name;
        StarterRuleCards = starterRuleCards;
        StarterActionCards = starterActionCards;
        Deck = deck;
        MaxHP = maxHP;
        MaxMana = maxMana;
        StartingHandSize = startingHandSize;

        StarterRuleCardInstances = new AbstractRuleCard[StarterRuleCards.Length];
        for (int i = 0; i < StarterRuleCards.Length; i++)
        {
            var v = RuleCardFactory.MakeRuleCard(StarterRuleCards[i]);
            StarterRuleCardInstances[i] = v;
        }
        StarterActionCardInstances = new AbstractActionCard[StarterActionCards.Length];
        for (int i = 0; i < StarterActionCards.Length; i++)
        {
            var v = ActionCardFactory.MakeActionCard(StarterActionCards[i]);
            StarterActionCardInstances[i] = v;
        }
    }

    public override string ToString()
    {
        string ret = $@"{Name}

Max HP: {MaxHP}
Max Mana: {MaxMana}

Starting Hand Size: {StartingHandSize}

Deck: {Enum.GetName(typeof(DeckType), Deck)}
";
        ret += "\nRule Cards:";
        if (StarterRuleCards.Length == 0)
        {
            ret += "\nNone";
        }
        else
        {
            for (int i = 0; i < StarterRuleCardInstances.Length; i++)
            {
                AbstractRuleCard rc = StarterRuleCardInstances[i];
                ret += $"\n{rc.Name}";
            }
        }
        ret += "\n";
        ret += "\nAction Cards:";
        if (StarterActionCards.Length == 0)
        {
            ret += "\nNone";
        }
        else
        {
            for (int i = 0; i < StarterActionCardInstances.Length; i++)
            {
                AbstractActionCard ac = StarterActionCardInstances[i];
                ac.gameObject.SetActive(false);
                ret += $"\n{ac.Name}";
                UnityEngine.Object.Destroy(ac.gameObject);
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