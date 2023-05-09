using System.Collections.Generic;
using Source.Commands;
using Source.Game.Deliveries;
using Source.GameQueue;
using Source.Util;
using UnityEngine;

public static class Story_Scavenging
{
    public static GameQueue GasStation()
    {
        var q = new GameQueue();
        q.Add(new GCAlert("You found an abandoned gas station!"));
        q.Add(new GCChoices()
            .Add("Search the garage", queue =>
            {
                queue.Add(new GCNarrative("The garage smells like machine oil."));
                queue.Add(new GCAddItem(ScavengingItems.GetCombatItem()));
            }, new ChoiceCost { stamina = 5 })
            .Add("Search the store shelves", queue =>
            {
                queue.Add(new GCNarrative("The shelves are all empty, but you keep searching."));
                queue.Add(new GCAddItem(ScavengingItems.GetFoodItem()));
            }, new ChoiceCost { stamina = 5 })
            .Add("Break in to personal room", queue =>
            {
                queue.Add(new GCNarrative("The rooms are all abandoned, but you keep searching."));
                queue.Add(new GCAddItem(ScavengingItems.GetRandomHumanItem()));
            }, new ChoiceCost { stamina = ItemDatabase.crowbar.IsPresent() ? 5 : 10 })
        );
        return q;
    }
    
    public static GameQueue OldChurch()
    {
        var q = new GameQueue();
        q.Add(new GCAlert("You found an old church!"));
        q.Add(new GCChoices()
            .Add("Examine the crypt", queue =>
            {
                queue.Add(new GCNarrative("The crypt is filled with musty air."));
                queue.Add(new GCAddItem(ScavengingItems.GetCombatBuff()));
            }, new ChoiceCost { stamina = 10 })
            .Add("Search the cleric chambers", queue =>
            {
                queue.Add(new GCNarrative("The chambers are filled with religious artifacts."));
                queue.Add(new GCAddItem(ScavengingItems.GetFoodItem()));
            }, new ChoiceCost { stamina = 5 })
            .Add("Climb up to the bell tower", queue =>
            {
                queue.Add(new GCNarrative("The view from the bell tower is breathtaking."));
                queue.Add(new GCAddItem(ScavengingItems.GetFirearmWeapon()));
            }, new ChoiceCost { stamina = 25 })
        );
        return q;
    }
    
    public static GameQueue RuinedHospital()
    {
        var q = new GameQueue();
        q.Add(new GCAlert("You found a ruined hospital!"));
        q.Add(new GCChoices()
            .Add("Check the emergency room", queue =>
            {
                queue.Add(new GCNarrative("You find some medical supplies in the emergency room."));
                queue.Add(new GCAddItem(ScavengingItems.GetRandomHealthItem()));
            }, new ChoiceCost { stamina = 5 })
            .Add("Search the pharmacy", queue =>
            {
                queue.Add(new GCNarrative("You search the pharmacy and find some medicine."));
                queue.Add(new GCAddItem(ScavengingItems.GetRandomMedication()));
            }, new ChoiceCost { stamina = 10 })
            .Add("Investigate the morgue", queue =>
            {
                queue.Add(new GCNarrative("You find some interesting items in the morgue."));
                queue.Add(new GCAddItem(ScavengingItems.GetRandomHumanItem()));
            }, new ChoiceCost { stamina = 15 })
        );
        return q;
    }
    
    public static GameQueue DerelictWarehouse()
    {
        var q = new GameQueue();
        q.Add(new GCAlert("You found a derelict warehouse!"));
        q.Add(new GCChoices()
            .Add("Look in crates and containers", queue =>
            {
                queue.Add(new GCNarrative("You found a few rusty crates and containers."));
                queue.Add(new GCAddItem(ScavengingItems.GetCombatItem()));
            }, new ChoiceCost { stamina = 5 })
            .Add("Search staff lockers", queue =>
            {
                queue.Add(new GCNarrative("You found a few locked lockers, but you managed to break in."));
                queue.Add(new GCAddItem(ScavengingItems.GetFoodItem()));
                queue.Add(new GCAddItem(ItemDatabase.money, Random.Range(50, 100)));
            }, new ChoiceCost { stamina = 10 })
            .Add("Break in to director room", queue =>
            {
                queue.Add(new GCNarrative("You found a locked door, but you managed to break in with the crowbar."));
                queue.Add(new GCAddItem(ScavengingItems.GetFirearmWeapon()));
            }, new ChoiceCost { stamina = ItemDatabase.crowbar.IsPresent() ? 5 : 30 })
        );
        return q;
    }
    
    public static GameQueue SewerSystem()
    {
        var q = new GameQueue();
        q.Add(new GCAlert("You found a sewer system!"));
        q.Add(new GCChoices()
            .Add("Look around", queue =>
            {
                queue.Add(new GCNarrative("The place is dark and damp."));
                queue.Add(new GCAddItem(ItemDatabase.money, Random.Range(10, 20)));
                queue.Add(new GCAddItem(ItemDatabase.waterBottle, 1));
            }, new ChoiceCost { stamina = 5 })
            .Add("Break in to maintenance room", queue =>
            {
                queue.Add(new GCNarrative("The maintenance room is locked, but you manage to break in."));
                queue.Add(new GCAddItem(ScavengingItems.GetRandomHumanItem()));
            }, new ChoiceCost { stamina = ItemDatabase.crowbar.IsPresent() ? 10 : 30 })
        );
        return q;
    }
    
    public static GameQueue Cemetery()
    {
        var q = new GameQueue();
        q.Add(new GCAlert("You found a cemetery!"));
        q.Add(new GCChoices()
            .Add("Examine the crypts", queue =>
            {
                queue.Add(new GCNarrative("You start looking for any valuable items in the crypts."));
                queue.Add(new GCAddItem(ScavengingItems.GetRandomCryptItem()));
            }, new ChoiceCost { stamina = 15 })
            .Add("Dig the graves", queue =>
            {
                if (ItemDatabase.shovel.IsPresent())
                    queue.Add(new GCNarrative("You start digging graves with your shovel."));
                else
                    queue.Add(new GCNarrative("You start digging graves with your bare hands."));
                
                queue.Add(new GCAddItem(ScavengingItems.GetRandomGraveItem()));
                queue.Add(new GCQueue(CombatDatabase.Combat_Cemetery()));
                
            }, new ChoiceCost { stamina = ItemDatabase.shovel.IsPresent() ? 5 : 30 })
            .Add("Break in to the caretaker cabin", queue =>
            {
                queue.Add(new GCNarrative("You break in to the caretaker's cabin."));
                if (ItemDatabase.lockpicks.IsPresent())
                    queue.Add(new GCNarrative("It's easy with the lockpicks"));
                queue.Add(new GCAddItem(ScavengingItems.GetRandomHumanItem()));
            }, new ChoiceCost { stamina = ItemDatabase.lockpicks.IsPresent() ? 5 : 30 })
        );
        return q;
    }
    
    public static GameQueue AbandonedMilitaryBase()
    {
        var q = new GameQueue();
        q.Add(new GCAlert("You found an abandoned military base!"));
        q.Add(new GCChoices()
            .Add("Search the barracks", queue =>
            {
                queue.Add(new GCNarrative("The barracks are empty and quiet."));
                queue.Add(new GCAddItem(ScavengingItems.GetToolItem()));
            }, new ChoiceCost { stamina = 5 })
            .Add("Check the kitchen", queue =>
            {
                queue.Add(new GCNarrative("The kitchen is ransacked, but you manage to find some food."));
                queue.Add(new GCAddItem(ScavengingItems.GetFoodItem()));
            }, new ChoiceCost { stamina = 5 })
            .Add("Break into the armory cage", queue =>
            {
                queue.Add(new GCNarrative("You use your lockpicks to break into the armory cage."));
                queue.Add(new GCAddItem(ScavengingItems.GetFirearmWeapon()));
            }, new ChoiceCost { stamina = ItemDatabase.lockpicks.IsPresent() ? 5 : 30 })
        );
        return q;
    }
    
    
    public static GameQueue WorkingShoppingMall()
    {
        var q = new GameQueue();
        q.Add(new GCAlert("You found a working shopping mall!"));
        q.Add(new GCChoices()
            .Add("Steal something", queue =>
            {
                queue.Add(new GCNarrative("You try to steal something from the store."));
                queue.Add(new GCAddItem(ScavengingItems.GetFoodItem()));
                queue.Add(new GCQueue(CombatDatabase.Combat_ShoppingMall()));
            }, new ChoiceCost { stamina = 5 })
            .Add("Buy some food", queue =>
            {
                queue.Add(new GCNarrative("You buy some food from the store."));
                queue.Add(new GCAddItem(ScavengingItems.GetFoodItem()));
                queue.Add(new GCAddItem(ItemDatabase.money, -50, GiveItemStyle.ALERT));
            }, new ChoiceCost { money = 50 })
            .Add("Buy instruments", queue =>
            {
                queue.Add(new GCNarrative("You buy some instruments from the store."));
                queue.Add(new GCAddItem(ScavengingItems.GetToolItem()));
                queue.Add(new GCAddItem(ItemDatabase.money, -100, GiveItemStyle.ALERT));
            }, new ChoiceCost { money = 100 })
        );
        return q;
    }
}

public struct GiveItem
{
    public InventoryItemDefinition def;
    public int amount;

    public GiveItem(InventoryItemDefinition d, int a)
    {
        def = d;
        amount = a;
    }
}

public static class ScavengingItems
{
    public static GiveItem GetCombatItem()
    {
        var pool = new List<GiveItem>
        {
            new(ItemDatabase.shovel, 1),
            new(ItemDatabase.crowbar, 1)
        };

        return pool.GetRandom();
    }

    public static GiveItem GetFoodItem()
    {
        var pool = new List<GiveItem>
        {
            new(ItemDatabase.apple, 1),
            new(ItemDatabase.apple, 2),
            new(ItemDatabase.waterBottle, 1),
            new(ItemDatabase.waterBottle, 2)
        };
        
        return pool.GetRandom();
    }

    public static GiveItem GetRandomHumanItem()
    {
        var pool = new List<GiveItem>
        {
            new(ItemDatabase.apple, 1),
            new(ItemDatabase.apple, 2),
            new(ItemDatabase.waterBottle, 1),
            new(ItemDatabase.waterBottle, 2),
            new(ItemDatabase.comic, 1),
            new(ItemDatabase.cocaine, 1),
            new(ItemDatabase.coldPizza, 1),
            new(ItemDatabase.vape, 1),
            new(ItemDatabase.cannedCoffee, 1)
        };
        
        return pool.GetRandom();
    }

    public static GiveItem GetFirearmWeapon()
    {
        var pool = new List<GiveItem>
        {
            new(ItemDatabase.ammo, 3)
        };
        
        return pool.GetRandom();
    }

    public static GiveItem GetCombatBuff()
    {
        var pool = new List<GiveItem>
        {
            new(ItemDatabase.chewingTobacco, 1),
            new(ItemDatabase.dust, 1)
        };
        
        return pool.GetRandom();
    }

    public static GiveItem GetRandomMedication()
    {
        var pool = new List<GiveItem>
        {
            new(ItemDatabase.medkit, 1),
            new(ItemDatabase.tranquilizers, 1),
            new(ItemDatabase.psychedelics, 1),
            new(ItemDatabase.bandage, 1)
        };
        
        return pool.GetRandom();
    }

    public static GiveItem GetRandomHealthItem()
    {
        var pool = new List<GiveItem>
        {
            new(ItemDatabase.medkit, 1),
            new(ItemDatabase.bandage, 1),
            new(ItemDatabase.tranquilizers, 1)
        };
        
        return pool.GetRandom();
    }

    public static GiveItem GetRandomGraveItem()
    {
        var pool = new List<GiveItem>
        {
            new (ItemDatabase.shovel, 1)
        };
        
        return pool.GetRandom();
    }

    public static GiveItem GetRandomCryptItem()
    {
        var pool = new List<GiveItem>
        {
            new (ItemDatabase.shovel, 1),
            new (ItemDatabase.money, 1),
            new (ItemDatabase.monsterMeat, 1)
        };
        
        return pool.GetRandom();
    }

    public static GiveItem GetToolItem()
    {
        var pool = new List<GiveItem>
        {
            new (ItemDatabase.lockpicks, 1),
            new (ItemDatabase.shovel, 1),
            new (ItemDatabase.crowbar, 1)
        };
        
        return pool.GetRandom();
    }

    public static GiveItem GetDefaultCombatLoot()
    {
        var pool = new List<GiveItem>
        {
            new(ItemDatabase.monsterMeat, 1),
            new(ItemDatabase.bandage, 1),
            new(ItemDatabase.waterBottle, 1),
            new(ItemDatabase.vape, 1),
            new(ItemDatabase.apple, 1),
            new(ItemDatabase.ammo, 1),
            new(ItemDatabase.chewingTobacco, 1),
            new(ItemDatabase.dust, 1)
        };
        
        return pool.GetRandom();
    }
}