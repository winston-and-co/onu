using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EnemyCharacter
{
    Ken,
    LostKid,
    LightweightLarry,
    OldMaster,
    ADog,
}

public enum EnemyPool
{
    Easy1,
    Hard1,
    Boss1,
}

public class EnemyGenerator : MonoBehaviour
{
    static EnemyGenerator Instance;
    public static EnemyGenerator GetInstance() => Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] GameObject enemyPrefab;

    static readonly Dictionary<EnemyPool, List<EnemyCharacter>> Pools = new()
    {
        [EnemyPool.Easy1] = new()
        {
            EnemyCharacter.Ken,
            EnemyCharacter.LostKid
        },
        [EnemyPool.Hard1] = new()
        {
            EnemyCharacter.LightweightLarry,
        },
        [EnemyPool.Boss1] = new()
        {
            EnemyCharacter.OldMaster,
            EnemyCharacter.ADog,
        },
    };

    public Entity RandomFromPool(EnemyPool pool)
    {
        List<EnemyCharacter> chars = Pools[pool];
        var character = chars[UnityEngine.Random.Range(0, chars.Count)];
        var enemy = Generate(character);
        return enemy;
    }

    public Entity Generate(EnemyCharacter character)
    {
        var enemy = Instantiate(enemyPrefab).GetComponent<Entity>();
        if (enemy == null) throw new Exception("Enemy prefab missing Entity script component");

        Action<Entity> builderFn = character switch
        {
            EnemyCharacter.Ken => Ken,
            EnemyCharacter.LostKid => LostKid,
            EnemyCharacter.LightweightLarry => LightweightLarry,
            EnemyCharacter.OldMaster => OldMaster,
            EnemyCharacter.ADog => ADog,
            _ => (_) => throw new ArgumentException("Invalid EnemyCharacter"),
        };
        builderFn(enemy);
        enemy.deck.transform.SetParent(enemy.transform, false);
        enemy.deck.transform.position = new Vector3(-3.91f, -1.44f, 0.0f);
        enemy.hand = enemy.GetComponentInChildren<Hand>();
        return enemy;
    }

    void Ken(Entity enemy)
    {
        enemy.e_name = "Ken";
        enemy.isPlayer = false;
        // base stats
        enemy.maxHP = 60;
        enemy.hp = enemy.maxHP;
        enemy.maxMana = 11;
        enemy.mana = enemy.maxMana;
        enemy.startingHandSize = 7;
        enemy.gameRules = new();

        // base deck
        var dg = DeckGenerator.GetInstance();
        if (dg == null) throw new System.Exception("Deck generator not found.");
        var deck = dg.Generate(DeckType.Standard, enemy);
        enemy.deck = deck;

        // sprite
        var r = enemy.GetComponentInChildren<SpriteRenderer>();
        var sprite = Resources.Load<Sprite>("Sprites/Alpha/Enemy/ken");
        r.sprite = sprite;
    }

    void LostKid(Entity enemy)
    {
        enemy.e_name = "Lost Kid";
        enemy.isPlayer = false;
        // base stats
        enemy.maxHP = 30;
        enemy.hp = enemy.maxHP;
        enemy.maxMana = 10;
        enemy.mana = enemy.maxMana;
        enemy.startingHandSize = 7;
        enemy.gameRules = new();

        // base deck
        var dg = DeckGenerator.GetInstance();
        if (dg == null) throw new System.Exception("Deck generator not found.");
        var deck = dg.Generate(DeckType.Standard, enemy);
        enemy.deck = deck;

        // sprite
        var r = enemy.GetComponentInChildren<SpriteRenderer>();
        var sprite = Resources.Load<Sprite>("Sprites/Alpha/Enemy/lost_kid");
        r.sprite = sprite;
    }

    void LightweightLarry(Entity enemy)
    {
        enemy.e_name = "Lightweight Larry";
        enemy.isPlayer = false;
        // base stats
        enemy.maxHP = 80;
        enemy.hp = enemy.maxHP;
        enemy.maxMana = 15;
        enemy.mana = enemy.maxMana;
        enemy.startingHandSize = 7;
        enemy.gameRules = new();

        // base deck
        var dg = DeckGenerator.GetInstance();
        if (dg == null) throw new System.Exception("Deck generator not found.");
        var deck = dg.Generate(DeckType.Standard, enemy);
        enemy.deck = deck;

        // sprite
        var r = enemy.GetComponentInChildren<SpriteRenderer>();
        var sprite = Resources.Load<Sprite>("Sprites/Alpha/Enemy/lightweight_larry");
        r.sprite = sprite;
    }

    void OldMaster(Entity enemy)
    {
        enemy.e_name = "Old Master";
        enemy.isPlayer = false;
        // base stats
        enemy.maxHP = 70;
        enemy.hp = enemy.maxHP;
        enemy.maxMana = 20;
        enemy.mana = enemy.maxMana;
        enemy.startingHandSize = 7;
        enemy.gameRules = new();

        // base deck
        var dg = DeckGenerator.GetInstance();
        if (dg == null) throw new System.Exception("Deck generator not found.");
        var deck = dg.Generate(DeckType.Standard, enemy);
        enemy.deck = deck;

        // sprite
        var r = enemy.GetComponentInChildren<SpriteRenderer>();
        var sprite = Resources.Load<Sprite>("Sprites/Alpha/Enemy/old_master");
        r.sprite = sprite;
    }

    void ADog(Entity enemy)
    {
        enemy.e_name = "A dog?";
        enemy.isPlayer = false;
        // base stats
        enemy.maxHP = 100;
        enemy.hp = enemy.maxHP;
        enemy.maxMana = 16;
        enemy.mana = enemy.maxMana;
        enemy.startingHandSize = 7;
        enemy.gameRules = new();

        // base deck
        var dg = DeckGenerator.GetInstance();
        if (dg == null) throw new System.Exception("Deck generator not found.");
        var deck = dg.Generate(DeckType.Standard, enemy);
        enemy.deck = deck;

        // sprite
        var r = enemy.GetComponentInChildren<SpriteRenderer>();
        var sprite = Resources.Load<Sprite>("Sprites/Alpha/Enemy/a_dog");
        r.sprite = sprite;
    }
}
