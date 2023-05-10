using System;
using System.Collections.Generic;
using System.Linq;
using GameAnalyticsSDK;
using Source.Commands;
using Source.Game.Deliveries;
using Source.GameQueue;
using Source.Util;
using UnityEngine;
using UnityEngine.Events;

public class CombatActionDefinition
{
    public Func<CombatEnemy, GameQueue> queue;
}

public class CombatEnemyDefiniton
{
    public List<CombatActionDefinition> combatActions = new List<CombatActionDefinition>();
    public List<COMBAT_FEATURE> combatFeatures = new List<COMBAT_FEATURE>();
    
    public Sprite image;
    public string name;
    public int health;
    public int lvl;
    public float scale = 1f;
    public List<string> deathSound = new() { "sound/combat/ogre1", "sound/combat/ogre2", "sound/combat/ogre3", "sound/combat/ogre4", "sound/combat/ogre5" };
}

public class CombatEntity
{
    public int damage;
    public int actionPoints = 2;
}

public enum CombatStatus
{
    STUNNED,
    BLEEDING,
    BLINDED,
    VULNERABLE
}

public class CombatEnemy : CombatEntity
{
    public CombatEnemyDefiniton definition;
    public List<CombatStatus> statuses = new List<CombatStatus>();
    public List<string> say = new List<string>();

    public int turnNumber;

    public bool IsDead()
    {
        return damage >= definition.health;
    }

    public void AddStatus(CombatStatus status)
    {
        if (!statuses.Contains(status))
        {
            statuses.Add(status);
            
            var tp = CombatUI.GetView(this).transform.position;
            FloatingText.Show(tp, status.ToString());
        }
    }

    public void RemoveStatus(CombatStatus status)
    {
        if (statuses.Contains(status))
        {
            statuses.Remove(status);
        }
    }

    public float HitChance()
    {
        foreach (var s in statuses)
        {
            switch (s)
            {
                case CombatStatus.BLINDED:
                    return 0f;
            }
        }

        return 0.8f;
    }

    public GameQueue ProcessTurn()
    {
        var q = new GameQueue();
        
        foreach (var s in statuses)
        {
            switch (s)
            {
                case CombatStatus.STUNNED:
                    q.Add(new GCNarrative($"{definition.name} is stunned."));
                    return q;

                case CombatStatus.BLEEDING:
                    q.Add(new GCNarrative($"{definition.name} is bleeding."));
                    q.Add(new GCCombatDealDamage(this, 50));
                    break;
            }
        }

        foreach (var f in definition.combatFeatures)
        {
            switch (f)
            {
                case COMBAT_FEATURE.SPAWN_BABIES_AFTER_3_TURNS:
                    if (turnNumber > 2)
                    {
                        var num = 4 - Game.world.combat.enemies.Count;
                        for (var i = 0; i < num; i++)
                        {
                            Game.world.combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.child });
                        }
                    }

                    Kill();
                    
                    q.Add(new GCNarrative($"{definition.name} has exploded!"));
                    return q;
            }
        }

        q.Add(new GCQueue(definition.combatActions.GetRandom().queue(this)));
        return q;
    }

    void Kill()
    {
        damage = definition.health;
    }

    public GameQueue ProcessEndOfTurn()
    {
        var q = new GameQueue();

        if (IsDead())
            return q;
        
        for (var index = statuses.Count - 1; index >= 0; index--)
        {
            var s = statuses[index];
            switch (s)
            {
                case CombatStatus.STUNNED:
                    TryRemove(q, 0.5f, CombatStatus.STUNNED);
                    break;

                case CombatStatus.BLEEDING:
                    TryRemove(q, 0.25f, CombatStatus.BLEEDING);
                    break;

                case CombatStatus.BLINDED:
                    TryRemove(q, 0.25f, CombatStatus.BLINDED);
                    break;
            }
        }

        return q;
    }

    void TryRemove(GameQueue q, float f, CombatStatus combatStatus)
    {
        if (f.Roll())
        {
            q.Add(new GCNarrative($"{definition.name} is no longer {combatStatus}."));
            statuses.Remove(combatStatus);
        }
    }

    public bool TryHit()
    {
        return HitChance().Roll();
    }
}

public class CombatPlayer : CombatEntity
{
}

[Serializable]
public class Combat : QueueItemBase
{
    public string id;

    public CombatEntity player = new CombatPlayer();
    public List<CombatEnemy> enemies = new List<CombatEnemy>();
    public int turn;

    public UnityAction RefreshState;
    public UnityAction OnPlayerTurn;
    public UnityAction<int> OnEnemyTurn;
    
    public List<InventoryItemDefinition> itemsUsedThisTurn = new List<InventoryItemDefinition>();

    public Func<GameQueue> Loot;

    UI_STATES _oldState;

    public override void Enter()
    {
        base.Enter();
        
        GameAnalytics.NewDesignEvent("start_combat_" + Game.world.combat.id);

        _oldState = UIState.CurrentState;
        UIState.DoState(UI_STATES.COMBAT);
        OptionPanelUI.Close();
        
        CombatUI.Show(player, enemies.ToList<CombatEntity>());

        if (Game.world.combatTutorial == false)
        {
            subqueue.Add(new GCAlert("You're in a combat now."));
            subqueue.Add(new GCAlert("Select your actions at the bottom of the screen."));
            subqueue.Add(new GCAlert("Select Attack. Then Fist. Then click on the enemy."));
        }

        foreach (var e in enemies)
        {
            foreach (var say in e.say)
            {
                subqueue.Add(new GCNarrative(e.definition.name + ": \"" + say + "\""));
            }
        }

        player.actionPoints = CalculatePlayerAP();
        
        StartTurn();
    }

    public void StartTurn()
    {
        if (IsEveryEnemyDead())
        {
            GameAnalytics.NewDesignEvent("win_combat_" + Game.world.combat.id);
            Complete();
            return;
        }

        // for (var index = 0; index < enemies.Count; index++)
        // {
        //     var e = enemies[index];
        //     if (e.IsDead())
        //         enemies.Remove(e);
        // }
        
        // CombatUI.Show(player, enemies.ToList<CombatEntity>(), fadeIn: false);

        if (turn == 0)
        {
            OnPlayerTurn?.Invoke();
            subqueue.Add(new GCCombatPlayerTurn());
        }
        else if (turn <= enemies.Count)
        {
            var combatEnemy = enemies[turn - 1];

            combatEnemy.turnNumber++;
            
            if (combatEnemy.IsDead())
            {
                turn++;
                StartTurn();
                return;
            }

            OnEnemyTurn?.Invoke(turn - 1);
            subqueue.Add(new GCCombatEnemyTurn(combatEnemy));
        }
        else
        {
            turn = 0;
            StartTurn();
        }
    }

    bool IsEveryEnemyDead()
    {
        foreach (var e in enemies)
        {
            if (!e.IsDead())
            {
                return false;
            }
        }

        return true;
    }

    public void EndTurn()
    {
        if (turn == 0)
        {
            if (Game.world.combatTutorial == false)
            {
                subqueue.Add(new GCAlert("Attacks cost AP (Pction Points)"));
                subqueue.Add(new GCAlert("You have 1 AP left."));
                subqueue.Add(new GCAlert("When you've spent all of them, your turn ends."));
                
                Game.world.combatTutorial = true;
            }

            if (player.actionPoints == 0)
            {
                //                 |||
                // actual end turn VVV
                turn++;
             
                Game.world.combat.itemsUsedThisTurn.Clear();

                var playerActionPoints = CalculatePlayerAP();
                Game.world.combat.player.actionPoints = playerActionPoints;
            }
        }
        else
        {
            turn++;
        }

        StartTurn();
    }

    int CalculatePlayerAP()
    {
        var calculatePlayerAP = Game.world.player.GetMaxAP();
        var decBy = 0;
        
        foreach (var e in enemies)
        {
            if (e.IsDead()) continue;
            if (e.definition.combatFeatures.Contains(COMBAT_FEATURE.DECREASES_PLAYER_AP))
            {
                decBy++;
                break;
            }
        }

        return calculatePlayerAP - decBy;
    }

    public override void Exit()
    {
        base.Exit();
        
        // just end bleeding after combat, or it's fucked
        Game.world.status.Remove(EnumPlayerStatuses.BLEEDING);
        
        UIState.DoState(_oldState);
    }
}

public class GCCombatPlayerTurn : QueueItemBase
{
    public override void Enter()
    {
        base.Enter();

        UIState.DoState(UI_STATES.COMBAT_ENEMY_TURN);
        
        subqueue.Add(new GCNarrative("Your turn!"));
        subqueue.Add(new GCQueue(ProcessPlayerStatuses()));
        subqueue.Add(new GCCall(ShowOptions));
    }

    GameQueue ProcessPlayerStatuses()
    {
        var q = new GameQueue();

        if (Game.world.status.Has(EnumPlayerStatuses.BLEEDING))
        {
            q.Add(new GCDealDamageToPlayer(10));
        }
        
        return q;
    }

    void ShowOptions()
    {
        var optionModel = new OptionPanelModel();

        var attackSection = new OptionSectionModel
        {
            sectionName = "Attack",
            actions = new List<OptionActionModel> { }
        };

        foreach (var i in Game.world.inventory.items)
        {
            if (i.amount == 0) continue;

            if (i.definition.weaponAction != null)
            {
                attackSection.actions.Add(new OptionActionModel
                {
                    optionName = i.definition.name,
                    itemDefinition = i.definition,
                    weaponAction = i.definition.weaponAction,
                    onPicked = () =>
                    {
                        subqueue.Add(new GCCombatPickTarget(i.definition));
                        subqueue.Add(new GCCall(Complete));
                    }
                });
            }
        }

        optionModel.sections.Add(attackSection);

        optionModel.sections.Add(new OptionSectionModel
        {
            sectionName = "Actions",
            actions = new List<OptionActionModel>
            {
                new OptionActionModel
                {
                    optionName = "Use Item",
                    onPicked = () =>
                    {
                        subqueue.Add(new GCUIState(UI_STATES.COMBAT_INVENTORY));
                        subqueue.Add(new GCShowInventory());
                        subqueue.Add(new GCUIState(UI_STATES.COMBAT));
                    }
                }
            }
        });

        optionModel.actionPoints = Game.world.combat.player.actionPoints;
        
        OptionPanelUI.Show(optionModel);

        UIState.DoState(UI_STATES.COMBAT);
    }

    public override void Exit()
    {
        base.Exit();
        
        Game.world.combat.EndTurn();

        UIState.DoState(UI_STATES.COMBAT_ENEMY_TURN);
    }
}

public class GCCombatPickTarget : QueueItemBase
{
    readonly WeaponActionDefinition action;
    readonly InventoryItemDefinition item;

    public GCCombatPickTarget(InventoryItemDefinition itemdef)
    {
        item = itemdef;
        action = itemdef.weaponAction;
    }

    public override void Enter()
    {
        base.Enter();

        if (action.noTarget)
        {
            UIState.DoState(UI_STATES.COMBAT_ENEMY_TURN);
            OnTargetPicker(null);
        }
        else
        {
            UIState.DoState(UI_STATES.COMBAT_PICK_TARGET);
            CombatUI.EngagePickTargetMode(OnTargetPicker);
        }
    }

    void OnTargetPicker(CombatEntity arg0)
    {
        UITooltip.Hide();

        GameAnalytics.NewDesignEvent("pick_item_" + item.name);
        if (arg0 is CombatEnemy enemy)
            GameAnalytics.NewDesignEvent("pick_target_" + enemy.definition.name);
        
        Game.world.combat.itemsUsedThisTurn.Add(item);
        if (action.ammo != null)
            subqueue.Add(new GCAddItem(action.ammo, -1, GiveItemStyle.SILENT));
        subqueue.Add(new GCQueue(action.attackAction.Invoke(item, arg0 as CombatEnemy)));
        subqueue.Add(new GCCall(Complete));
    }

    public override void Exit()
    {
        Game.world.combat.player.actionPoints -= action.apCost;
        Game.world.player.AddStat(EnumPlayerStats.STAMINA, -action.staminaCost);
        
        base.Exit();
    }
}

class GCCombatDealDamage : QueueItemBase
{
    CombatEnemy _enmy;
    int _dmg;
    InventoryItemDefinition _item;

    public int GetDamage()
    {
        return _dmg;
    }
    
    public GCCombatDealDamage(CombatEnemy combatEnemy, int i)
    {
        _enmy = combatEnemy;
        _dmg = i;
    }
    
    public GCCombatDealDamage(InventoryItemDefinition item, CombatEnemy combatEnemy, int i)
    {
        _item = item;
        _enmy = combatEnemy;
        _dmg = i;
    }

    public override void Enter()
    {
        base.Enter();

        if (_item != null)
        {
            if (_item.weaponAction.missChance.Roll())
            {
                subqueue.Add(new GCNarrative("You miss!"));
                subqueue.Add(new GCCall(Complete));
                return;
            }
        }

        var special = DamageSpecial.NORMAL;
        
        if (_enmy.statuses.Contains(CombatStatus.VULNERABLE))
            _dmg *= 2;

        _item?.weaponAction.defaultSound.PlayClip();

        _enmy.damage += _dmg;

        subqueue.Add(new GCCall(() => CombatUI.ShowDamage(_enmy, _dmg, special)));
        subqueue.Add(new GCNarrative("You deal " + _dmg + " damage!"));

        if (_enmy.definition.combatFeatures.Contains(COMBAT_FEATURE.STUN_WHEN_DAMAGED))
            _enmy.AddStatus(CombatStatus.STUNNED);
        
        if (_enmy.IsDead())
        {
            subqueue.Add(new GCNarrative("Enemy dies!"));
            subqueue.Add(new GCCall(() => CombatUI.EnemyDies(_enmy)));
            subqueue.Add(new GCWait(1f));
        }

        subqueue.Add(new GCCall(Complete));
    }
}

public class GCCombatEnemyTurn : QueueItemBase
{
    readonly CombatEnemy enmy;

    public GCCombatEnemyTurn(CombatEnemy enemy)
    {
        enmy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        subqueue.Add(new GCNarrative("Enemy turn!"));
        subqueue.Add(new GCQueue(enmy.ProcessTurn()));
        subqueue.Add(new GCQueue(enmy.ProcessEndOfTurn()));
        subqueue.Add(new GCCall(Complete));
    }

    public override void Exit()
    {
        base.Exit();
        Game.world.combat.EndTurn();
    }
}