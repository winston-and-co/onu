using UnityEngine;

public enum PlayerCharacter
{
    Character1,
    Character2,
    Character3,
}

public class CharacterSelect : MonoBehaviour
{
    static Entity preview;
    [SerializeField] GameObject playerPrefab;

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

    public Entity PreviewPlayerCharacter(PlayerCharacter character)
    {
        if (preview != null) Destroy(preview.gameObject);
        var player = Instantiate(playerPrefab).GetComponent<Entity>();
        if (player == null) throw new System.Exception("Player prefab missing Entity script component");
        switch (character)
        {
            case PlayerCharacter.Character1:
                Character1(player);
                break;
            case PlayerCharacter.Character2:
                Character2(player);
                break;
            case PlayerCharacter.Character3:
                Character3(player);
                break;
            default:
                throw new System.NotImplementedException("Fell through player entity initialization with invalid PlayerCharacter value.");
        }
        preview = player;
        player.deck.transform.SetParent(player.transform, false);
        player.deck.transform.position = new Vector3(5.55f, -3.4812f, 0.0f);
        player.hand = player.GetComponentInChildren<Hand>();
        return player;
    }

    void Character1(Entity player)
    {
        player.e_name = "Character1";
        player.isPlayer = true;
        // base stats
        player.maxHP = 60;
        player.hp = player.maxHP;
        player.maxMana = 13;
        player.mana = player.maxMana;
        player.startingHandSize = 7;
        player.gameRules = new();
        // base deck
        var dg = DeckGenerator.GetInstance();
        if (dg == null) throw new System.Exception("Deck generator not found.");
        var deck = dg.Generate(DeckType.Standard, player);
        player.deck = deck;
    }

    void Character2(Entity player)
    {
        player.e_name = "Character2";
        player.isPlayer = true;
        // base stats
        player.maxHP = 60;
        player.hp = player.maxHP;
        player.maxMana = 9;
        player.mana = player.maxMana;
        player.startingHandSize = 7;
        player.gameRules = new();
        // base deck
        var dg = DeckGenerator.GetInstance();
        if (dg == null) throw new System.Exception("Deck generator not found.");
        var deck = dg.Generate(DeckType.Standard, player);
        player.deck = deck;
        // action cards
        PlayerData.GetInstance().AddActionCard("AC0_Wild");
        PlayerData.GetInstance().AddActionCard("AC0_Wild");
        PlayerData.GetInstance().AddActionCard("AC0_Wild");
    }

    void Character3(Entity player)
    {
        player.e_name = "Character3";
        player.isPlayer = true;
        // base stats
        player.maxHP = 40;
        player.hp = player.maxHP;
        player.maxMana = 7;
        player.mana = player.maxMana;
        player.startingHandSize = 7;
        player.gameRules = new();
        // initial rule cards
        player.gameRules.Add(new RuleCards.Purple());
        // base deck
        var dg = DeckGenerator.GetInstance();
        if (dg == null) throw new System.Exception("Deck generator not found.");
        var deck = dg.Generate(DeckType.Standard, player);
        player.deck = deck;
    }
}