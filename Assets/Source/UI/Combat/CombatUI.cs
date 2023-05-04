using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Source.Util;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum DamageSpecial
{
    NORMAL,
    VULNERABLE,
    CRIT
}

public class UniversalCombatEntity
{
    readonly CombatEntity _entity;

    public UniversalCombatEntity(CombatEntity entity)
    {
        _entity = entity;
    }

    public int GetHP()
    {
        switch (_entity)
        {
            case CombatEnemy combatEnemy:
                return combatEnemy.definition.health - combatEnemy.damage;
            case CombatPlayer combatPlayer:
                return Game.world.player.GetStat(EnumPlayerStats.HEALTH);
            default:
                throw new ArgumentOutOfRangeException(nameof(_entity));
        }
    }

    public Sprite GetIcon()
    {
        switch (_entity)
        {
            case CombatEnemy combatEnemy:
                return combatEnemy.definition.image;
            case CombatPlayer combatPlayer:
                return "combat/player_front".LoadSprite();
            default:
                throw new ArgumentOutOfRangeException(nameof(_entity));
        }
    }

    public string GetName()
    {
        switch (_entity)
        {
            case CombatEnemy combatEnemy:
                return combatEnemy.definition.name + " lvl" + combatEnemy.definition.lvl;
            case CombatPlayer combatPlayer:
                return "YOU";
            default:
                throw new ArgumentOutOfRangeException(nameof(_entity));
        }
    }

    public float GetHPNormalized()
    {
        switch (_entity)
        {
            case CombatEnemy combatEnemy:
                return (GetHP() / (float)combatEnemy.definition.health).C01();
            case CombatPlayer combatPlayer:
                return (Game.world.player.GetStat(EnumPlayerStats.HEALTH) / 100f).C01();
            default:
                throw new ArgumentOutOfRangeException(nameof(_entity));
        }
    }

    public CombatEntity GetRaw()
    {
        return _entity;
    }

    public Vector3 GetScale()
    {
        switch (_entity)
        {
            case CombatEnemy combatEnemy:
                return combatEnemy.definition.scale * Vector3.one;
            case CombatPlayer combatPlayer:
                return Vector3.one;
            default:
                throw new ArgumentOutOfRangeException(nameof(_entity));
        }
    }

    public string GetStatusString()
    {
        switch (_entity)
        {
            case CombatEnemy combatEnemy:
                var s = "";
                foreach (var ss in combatEnemy.statuses)
                    s += ss + "\n";
                return s;
            case CombatPlayer combatPlayer:
                var sp = "";
                foreach (var ss in Game.world.status.list)
                    sp += ss + "\n";
                return "";
            default:
                throw new ArgumentOutOfRangeException(nameof(_entity));
        }
    }
}

public class CombatUI : UILayerItemBase
{
    static CombatUI i;

    public Image solidBackground;
    public Image flashBackground;
    
    public Image turnPicker;

    public CombatantView player;
    public List<CombatantView> enemies;
    
    float turnPos;

    void Awake()
    {
        i = this;
        Hide();
    }

    protected override void OnShow()
    {
        base.OnShow();
        i.solidBackground.enabled = true;
        i.flashBackground.enabled = true;
    }

    protected override void OnHide()
    {
        base.OnHide();
        i.solidBackground.enabled = false;
        i.flashBackground.enabled = false;
    }

    public static void Show(CombatEntity player, List<CombatEntity> enemies, bool fadeIn = true)
    {
        Game.world.combat.OnPlayerTurn -= OnPlayerTurn;
        Game.world.combat.OnPlayerTurn += OnPlayerTurn;
        Game.world.combat.OnEnemyTurn -= OnEnemyTurn;
        Game.world.combat.OnEnemyTurn += OnEnemyTurn;

        i.player.Show(player, fadeIn);

        for (var j = 0; j < i.enemies.Count; j++)
        {
            if (j < enemies.Count)
            {
                i.enemies[j].Show(enemies[j], fadeIn);
            }
            else
            {
                i.enemies[j].Hide();
            }
        }

        i.Show();
    }

    public static void OnPlayerTurn()
    {
        i.turnPos = i.player.transform.position.x;
        i.turnPicker.color = Color.white;
        // i.turnPicker.transform.DOMoveX(turn, 0.5f);

        UpdateTurnPicker();
    }

    public static void OnEnemyTurn(int enemy)
    {
        i.turnPos = i.enemies[enemy].transform.position.x;
        i.turnPicker.color = Color.red;
        // i.turnPicker.transform.DOMoveX(i.turnPos, 0.5f);

        UpdateTurnPicker();
    }

    static void UpdateTurnPicker()
    {
        var transformPosition = i.turnPicker.transform.position;
        transformPosition.x = i.turnPos;
        // transformPosition.x = Mathf.MoveTowards(transformPosition.x, turnPos, Time.fixedDeltaTime * 10f);
        i.turnPicker.transform.position = transformPosition;
    }

    public static void Close()
    {
        i.Hide();
    }

    public static void Restore()
    {
        i.Show();
    }

    public static void EngagePickTargetMode(UnityAction<CombatEntity> cb)
    {
        foreach (var e in i.enemies)
        {
            e.PrimeForTargetPicking(cb);
        }
    }

    public static void EnemyDies(CombatEnemy enmy)
    {
        foreach (var e in i.enemies)
        {
            if (e.Is(enmy))
            {
                enmy.definition.deathSound.GetRandom().PlayClip();
                e.Die();
            }
        }
    }

    public static void DisengagePickTargetMode()
    {
        foreach (var e in i.enemies)
        {
            e.DisengageForTargetPicking();
        }
    }
    
    public static void ShowDamage(CombatEnemy enmy, int dmg, DamageSpecial special = DamageSpecial.NORMAL)
    {
        foreach (var e in i.enemies)
        {
            if (e.Is(enmy))
            {
                e.VisualizeDamage(dmg, special);
            }
        }
    }
    
    public static void ShowPlayerDamage(int dmg)
    {
        i.player.VisualizeDamage(dmg, DamageSpecial.NORMAL);
    }

    void Update()
    {
        var transformPosition = i.turnPicker.transform.position;
        transformPosition.x = turnPos;
        // transformPosition.x = Mathf.MoveTowards(transformPosition.x, turnPos, Time.fixedDeltaTime * 10f);
        i.turnPicker.transform.position = transformPosition;
        
        flashBackground.color = new Color(1f, 1f, 1f, Mathf.Sin(Time.time) * 0.2f);
    }

    public static CombatantView GetView(CombatEnemy enmy)
    {
        foreach (var e in i.enemies)
        {
            if (e.Is(enmy))
            {
                return e;
            }
        }

        return null;
    }
}