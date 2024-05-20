using System;
using System.Collections.Generic;

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

public class EnemyFactory
{
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

    public static EnemyEntity RandomFromPool(EnemyPool pool)
    {
        List<EnemyCharacter> chars = Pools[pool];
        var character = chars[UnityEngine.Random.Range(0, chars.Count)];
        var enemy = MakeEnemy(character);
        return enemy;
    }

    public static EnemyEntity MakeEnemy(EnemyCharacter character)
    {
        return character switch
        {
            EnemyCharacter.Ken => Ken(),
            EnemyCharacter.LostKid => LostKid(),
            EnemyCharacter.LightweightLarry => LightweightLarry(),
            EnemyCharacter.OldMaster => OldMaster(),
            EnemyCharacter.ADog => ADog(),
            _ => throw new ArgumentException("Invalid EnemyCharacter"),
        };
    }

    static EnemyEntity Ken()
    {
        return EnemyEntity.New(
            enemyName: "Ken",
            deckType: DeckType.Standard,
            maxHP: 60,
            maxMana: 11,
            startingHandSize: 7,
            patternType: typeof(SimpleAI),
            spriteName: "Enemy/ken"
        );
    }

    static EnemyEntity LostKid()
    {
        return EnemyEntity.New(
            enemyName: "Lost Kid",
            deckType: DeckType.Standard,
            maxHP: 30,
            maxMana: 10,
            startingHandSize: 7,
            patternType: typeof(SimpleAI),
            spriteName: "Enemy/lost_kid"
        );
    }

    static EnemyEntity LightweightLarry()
    {
        return EnemyEntity.New(
            enemyName: "Lightweight Larry",
            deckType: DeckType.Standard,
            maxHP: 80,
            maxMana: 15,
            startingHandSize: 7,
            patternType: typeof(SimpleAI),
            spriteName: "Enemy/lightweight_larry"
        );
    }

    static EnemyEntity OldMaster()
    {
        return EnemyEntity.New(
            enemyName: "Old Master",
            deckType: DeckType.Standard,
            maxHP: 70,
            maxMana: 20,
            startingHandSize: 7,
            patternType: typeof(SimpleAI),
            spriteName: "Enemy/old_master"
        );
    }

    static EnemyEntity ADog()
    {
        return EnemyEntity.New(
            enemyName: "A dog?",
            deckType: DeckType.Standard,
            maxHP: 100,
            maxMana: 16,
            startingHandSize: 5,
            patternType: typeof(SimpleAI),
            spriteName: "Enemy/a_dog"
        );
    }
}
