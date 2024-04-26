using UnityEngine;

public enum EnemyCharacter
{
    Ken
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

    public Entity Generate(EnemyCharacter character)
    {
        var enemy = Instantiate(enemyPrefab).GetComponent<Entity>();
        if (enemy == null) throw new System.Exception("Enemy prefab missing Entity script component");
        switch (character)
        {
            case EnemyCharacter.Ken:
                Ken(enemy);
                break;
            default:
                throw new System.NotImplementedException("Fell through player entity initialization with invalid EnemyCharacter value.");
        }
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
        enemy.maxHP = 30;
        enemy.hp = enemy.maxHP;
        enemy.maxMana = 9;
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
        var sprite = Resources.Load<Sprite>("Sprites/Alpha/ken");
        r.sprite = sprite;
    }
}