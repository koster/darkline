using System;
using System.Collections.Generic;
using Source.Game.Deliveries;
using Source.GameQueue;
using Source.Util;
using UnityEngine;

[Serializable]
public class Inventory
{
    public List<InventoryItem> items = new List<InventoryItem>();

    public void Give(InventoryItemDefinition idd, int amount)
    {
        if (amount < 0)
        {
            Take(idd, amount);
            return;
        }
        
        if (idd.stackable)
        {
            var itm = GetOrCreateItem(idd);
            itm.amount += amount;
            return;
        }

        items.Add(new InventoryItem
        {
            definition = idd,
            amount = amount
        });
    }

    public bool Take(InventoryItemDefinition idd, int amount)
    {
        var amountToTake = amount.Abs();

        foreach (var item in items)
        {
            if (item.definition == idd)
            {
                if (item.amount >= amountToTake)
                {
                    item.amount -= amountToTake;
                    amountToTake = 0;
                }
                else
                {
                    item.amount = 0;
                    amountToTake -= item.amount;
                }

                if (amountToTake == 0)
                    return true;
            }
        }

        return false;
    }

    InventoryItem GetOrCreateItem(InventoryItemDefinition idd)
    {
        foreach (var i in items)
        {
            if (i.definition == idd)
                return i;
        }

        var inventoryItem = new InventoryItem { definition = idd };
        items.Add(inventoryItem);
        return inventoryItem;
    }

    public bool HasItem(InventoryItemDefinition iid)
    {
        if (!iid.stackable)
        {
            foreach (var f in items)
            {
                if (f.definition == iid)
                    return true;
            }
        }
        
        return GetOrCreateItem(iid).amount > 0;
    }

    public int GetItemAmount(InventoryItemDefinition money)
    {
        return GetOrCreateItem(money).amount;
    }

    public void TakeAll(InventoryItemDefinition definitionItem)
    {
        foreach (var i in items)
        {
            if (i.definition == definitionItem)
                i.amount = 0;
        }
    }
}

public class InventoryItem
{
    public InventoryItemDefinition definition;
    public int amount;
}

public class WeaponActionDefinition
{
    public Func<InventoryItemDefinition, CombatEnemy, GameQueue> attackAction;
    
    public int apCost = 2;
    public int staminaCost = 0;
    
    public bool noTarget;
    public float missChance;
    public InventoryItemDefinition ammo;
    public bool oncePerTurn;
    public string defaultSound = "sound/combat/hurt";

    public TooltipModel GetTooltip(InventoryItemDefinition iid, string optionName)
    {
        var tm = new TooltipModel();

        tm.icon = iid.icon;
        tm.header = optionName;
        tm.lines.Clear();

        var dummy = new CombatEnemy();
        dummy.definition = EnemyDatabase.evilMonster1;
        var queue = attackAction(ItemDatabase.dust, dummy);

        foreach (var qi in queue.Items)
        {
            switch (qi)
            {
                case GCCombatDealDamage dmg:
                    tm.lines.Add($"Deals {dmg.GetDamage()} damage");
                    break;
                case GCCombatStunChance stun:
                    tm.lines.Add($"{stun.GetChance().AsPercent()} chance of stun.");
                    break;
                case GCCombatStatusChance status:
                    tm.lines.Add($"{status.GetChance().AsPercent()} chance of {GetStatusString(status.GetStatus())}.");
                    break;
                case GCCombatItemConsumed consumable:
                    tm.lines.Add("Consumed after use.");
                    break;
                case GCCombatGiveActionPoints ap:
                    tm.lines.Add($"Gives {ap.GetAmount()} AP.");
                    break;
                case GCBreakChance breakable:
                    tm.lines.Add($"{breakable.GetChance().AsPercent()} chance of breaking.");
                    break;
            }
        }

        if (missChance > 0)
            tm.lines.Add($"Miss chance {missChance.AsPercent()}");
        
        if (ammo != null)
            tm.lines.Add($"Consumes {ammo.name} ({Game.world.inventory.GetItemAmount(ammo)} left)");
        
        if (oncePerTurn)
            tm.lines.Add($"Can only be used once per turn.");

        return tm;
    }

    string GetStatusString(CombatStatus getStatus)
    {
        switch (getStatus)
        {
            case CombatStatus.STUNNED:
                return "STUN";
            case CombatStatus.BLINDED:
                return "BLIND";
        }
        
        return getStatus.ToString();
    }
}
