using System;
using System.Collections.Generic;
using Source.Commands;
using Source.GameQueue;
using Source.Util;
using Random = UnityEngine.Random;

public enum COMBAT_FEATURE
{
    STUN_WHEN_DAMAGED,
    DECREASES_PLAYER_AP,
    SPAWN_BABIES_AFTER_3_TURNS
}

public static class EnemyDatabase
{
    public static List<CombatEnemyDefiniton> all = new List<CombatEnemyDefiniton>();
    
    public static CombatEnemyDefiniton boss_general;
    
    public static CombatEnemyDefiniton evilMonster1;
    public static CombatEnemyDefiniton evilMonster2;
    
    public static CombatEnemyDefiniton dog;     // basic enemy
    public static CombatEnemyDefiniton man;     // basic enemy 2
    
    public static CombatEnemyDefiniton spiker;  // bleeding
    public static CombatEnemyDefiniton heavy;   // stunning

    public static CombatEnemyDefiniton sewer;   // reduces player damage
    public static CombatEnemyDefiniton child;   // strong attack, but gets stunned if receives any damage 
    public static CombatEnemyDefiniton ball;    // spawner
    
    public static CombatEnemyDefiniton meat;    // fat enemy

    public static void Initialize()
    {
        boss_general = all.New();
        boss_general.image = "enemies/creepy_man_1".LoadSprite();
        boss_general.name = "GENERAL";
        boss_general.health = 300;
        boss_general.lvl = 1;
        boss_general.scale = 1.2f;
        boss_general.combatActions.Add(new CombatActionDefinition
        {
            queue = CombatActions.SimpleBite(boss_general, 10, 15)
        });
        
        sewer = all.New();
        sewer.image = "enemies/sewer".LoadSprite();
        sewer.name = "Sewer";
        sewer.health = 150;
        sewer.lvl = 1;
        sewer.scale = 1.2f;
        sewer.combatFeatures.Add(COMBAT_FEATURE.DECREASES_PLAYER_AP);
        sewer.combatActions.Add(new CombatActionDefinition
        {
            queue = CombatActions.SimpleBite(sewer, 10, 15)
        });
        
        ball = all.New();
        ball.image = "enemies/ball".LoadSprite();
        ball.name = "ball";
        ball.health = 300;
        ball.lvl = 1;
        ball.scale = 2f;
        ball.combatFeatures.Add(COMBAT_FEATURE.SPAWN_BABIES_AFTER_3_TURNS);
        ball.combatActions.Add(new CombatActionDefinition
        {
            queue = CombatActions.SimpleBite(ball, 10, 15)
        });
        
        child = all.New();
        child.image = "enemies/child".LoadSprite();
        child.name = "demon";
        child.health = 100;
        child.lvl = 1;
        child.combatFeatures.Add(COMBAT_FEATURE.STUN_WHEN_DAMAGED);
        child.combatActions.Add(new CombatActionDefinition
        {
            queue = CombatActions.SimpleBite(child, 25, 50)
        });
        
        heavy = all.New();
        heavy.image = "enemies/heavy".LoadSprite();
        heavy.name = "Heavy";
        heavy.health = 300;
        heavy.lvl = 1;
        heavy.scale = 1.2f;
        heavy.combatActions.Add(new CombatActionDefinition
        {
            queue = CombatActions.SimpleBite(heavy, 10, 15)
        });
        
        man = all.New();
        man.image = "enemies/man".LoadSprite();
        man.name = "man";
        man.health = 100;
        man.lvl = 1;
        man.scale = 1.2f;
        man.combatActions.Add(new CombatActionDefinition
        {
            queue = CombatActions.SimpleBite(man, 10, 15)
        });
        
        dog = all.New();
        dog.image = "enemies/demon_dog".LoadSprite();
        dog.name = "Dog";
        dog.health = 75;
        dog.lvl = 1;
        dog.scale = 1f;
        dog.combatActions.Add(new CombatActionDefinition
        {
            queue = CombatActions.SimpleBite(dog, 3, 6)
        });
        
        spiker = all.New();
        spiker.image = "enemies/spiker".LoadSprite();
        spiker.name = "Spiker";
        spiker.health = 300;
        spiker.lvl = 1;
        spiker.scale = 1.35f;
        spiker.combatActions.Add(new CombatActionDefinition
        {
            queue = CombatActions.BleedingBite(spiker, 10, 15)
        });
        
        meat = all.New();
        meat.image = "enemies/meat".LoadSprite();
        meat.name = "Meat";
        meat.health = 500;
        meat.lvl = 1;
        meat.scale = 1.35f;
        // meat.combatFeatures.Add(COMBAT_FEATURE.SPAWN_BABIES_AFTER_3_TURNS);
        meat.combatActions.Add(new CombatActionDefinition
        {
            queue = CombatActions.SimpleBite(meat, 10, 15)
        });
        
        evilMonster1 = all.New();
        evilMonster1.image = "enemies/evil_1".LoadSprite();
        evilMonster1.name = "Evil";
        evilMonster1.health = 100;
        evilMonster1.lvl = 1;
        evilMonster1.combatActions.Add(new CombatActionDefinition
        {
            queue = CombatActions.SimpleBite(evilMonster1, 10, 15)
        });
        
        evilMonster2 = all.New();
        evilMonster2.image = "enemies/evil_2".LoadSprite();
        evilMonster2.name = "General";
        evilMonster2.health = 200;
        evilMonster2.lvl = 1;
        evilMonster2.combatActions.Add(new CombatActionDefinition
        {
            queue = CombatActions.SimpleBite(evilMonster2, 10, 15)
        });
    }
}

public static class CombatActions
{
    public static Func<CombatEnemy, GameQueue> SimpleBite(CombatEnemyDefiniton def, int damageMin, int damageMax)
    {
        return combatEnemy =>
        {
            var q = new GameQueue();
            
            BiteLogic(def, damageMin, damageMax, combatEnemy, q);

            return q;
        };
    }

    static bool BiteLogic(CombatEnemyDefiniton def, int damageMin, int damageMax, CombatEnemy combatEnemy, GameQueue q)
    {
        if (combatEnemy.TryHit())
        {
            var damage = Random.Range(damageMin, damageMax);
            
            q.Add(new GCNarrative($"{def.name} bites you for {damage} damage!"));
            q.Add(new GCDealDamageToPlayer(damage));

            return true;
        }

        q.Add(new GCNarrative($"{def.name} is trying to bite, but misses!"));
        q.Add(new GCCall(() => CombatUI.GetView(combatEnemy).ShowFloatText("Misses!")));

        return false;
    }

    public static Func<CombatEnemy,GameQueue> BleedingBite(CombatEnemyDefiniton def, int damageMin, int damageMax)
    {
        return combatEnemy =>
        {
            var q = new GameQueue();
            
            var isHit = BiteLogic(def, damageMin, damageMax, combatEnemy, q);

            if (isHit)
            {
                const float bleedChance = 0.5f;
                if (bleedChance.Roll())
                {
                    q.Add(new GCAddStatus(EnumPlayerStatuses.BLEEDING));
                }
            }

            return q;
        };
    }
}

public class GCDealDamageToPlayer : QueueItemBase
{
    readonly int _dmg;

    public GCDealDamageToPlayer(int damage)
    {
        _dmg = damage;
    }

    public override void Enter()
    {
        base.Enter();
        
        "sound/combat/hurt".PlayClip();
        CombatUI.ShowPlayerDamage(_dmg);
        subqueue.Add(new GCAddStat(EnumPlayerStats.HEALTH, -_dmg));
    }

    public override void Update()
    {
        base.Update();

        if (subqueue.IsEmpty())
        {
            Complete();
        }
    }
}