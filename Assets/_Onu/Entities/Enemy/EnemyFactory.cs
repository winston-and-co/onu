using System;
using System.Collections.Generic;

public enum EnemyPool
{
    Easy1,
    Hard1,
    Boss1,
}

public class EnemyFactory
{
    static readonly Dictionary<EnemyPool, List<Func<EnemyEntity>>> Pools = new()
    {
        [EnemyPool.Easy1] = new()
        {
            // Baby,
            Ken,
            LostKid
        },
        [EnemyPool.Hard1] = new()
        {
            LightweightLarry,
        },
        [EnemyPool.Boss1] = new()
        {
            OldMaster,
            ADog,
        },
    };

    readonly static Random rand = new();
    public static EnemyEntity RandomFromPool(EnemyPool pool)
    {
        var funcs = Pools[pool];
        var idx = rand.Next(funcs.Count);
        var func = funcs[idx];
        var enemy = func();
        return enemy;
    }

    static EnemyEntity Baby()
    {
        return EnemyEntity.New(
            enemyName: "Baby",
            deckType: DeckType.Standard,
            maxHP: 1,
            maxMana: 1,
            startingHandSize: 7,
            patternType: typeof(SimpleAI),
            spriteName: "Enemy/ken"
        );
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
