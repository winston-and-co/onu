using UnityEngine;

public enum PlayerCharacter
{
    Character1,
}

public class CharacterSelect : MonoBehaviour
{
    private Entity preview;
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
        if (preview != null) Destroy(preview);
        var player = Instantiate(playerPrefab).GetComponent<Entity>();
        if (player == null) throw new System.Exception("Player prefab missing Entity script component");
        switch (character)
        {
            case PlayerCharacter.Character1:
                Character1(player);
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
        player.e_name = "player";
        player.isPlayer = true;
        // base stats
        player.maxHP = 60;
        player.hp = player.maxHP;
        player.maxMana = 13;
        player.mana = player.maxMana;
        player.startingHandSize = 7;
        player.gameRules = new();
        // player.gameRules.Add(new RuleCards.Purple());
        // player.gameRules.Add(new RuleCards.Chartreuse());

        // PlayerData.GetInstance().AddActionCard("AC0_Wild");
        // PlayerData.GetInstance().AddActionCard("AC0_Wild");

        // base deck
        var dg = DeckGenerator.GetInstance();
        if (dg == null) throw new System.Exception("Deck generator not found.");
        var deck = dg.Generate(DeckType.Standard, player);
        player.deck = deck;
    }
}