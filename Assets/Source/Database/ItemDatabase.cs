using System;
using System.Collections.Generic;
using Source.Commands;
using Source.GameQueue;
using Source.Util;
using UnityEngine;

public class InventoryItemDefinition
{
    public Sprite icon;
    public string name;
    public string desc;
    public Func<GameQueue> OnUse;
    public bool stackable;
    public bool hidden;
    public WeaponActionDefinition weaponAction;

    public bool IsPresent()
    {
        return Game.world.inventory.HasItem(this);
    }
}

public static class ItemDatabase
{
    public static List<InventoryItemDefinition> all = new List<InventoryItemDefinition>();
    
    // generic items
    
    public static InventoryItemDefinition money;
    
    //
    
    // weapon items
    
    public static InventoryItemDefinition fist;
    public static InventoryItemDefinition rustyPipe;
    public static InventoryItemDefinition plankOfWood;
    public static InventoryItemDefinition knife;
    public static InventoryItemDefinition crowbar;
    
    // firearm
    
    public static InventoryItemDefinition pistol;
    public static InventoryItemDefinition rifle;
    
    //
    
    // delivery items
    
    public static InventoryItemDefinition roses;
    public static InventoryItemDefinition suitcase;
    public static InventoryItemDefinition shotgun;
    public static InventoryItemDefinition powder;
    
    //
    
    public static InventoryItemDefinition apple;
    public static InventoryItemDefinition ambrosia;
    public static InventoryItemDefinition holyWater;

    //
    public static InventoryItemDefinition bandage;
    public static InventoryItemDefinition waterBottle;
    public static InventoryItemDefinition burger;
    public static InventoryItemDefinition comic;
    public static InventoryItemDefinition cannedCoffee;
    public static InventoryItemDefinition medkit;
    public static InventoryItemDefinition coldPizza;
    public static InventoryItemDefinition beer;
    public static InventoryItemDefinition vape;
    public static InventoryItemDefinition energyDrink;
    public static InventoryItemDefinition tranquilizers;
    public static InventoryItemDefinition monsterMeat;
    public static InventoryItemDefinition demonBlood;
    public static InventoryItemDefinition psychedelics;
    public static InventoryItemDefinition cocaine;
    public static InventoryItemDefinition mushrooms;
    public static InventoryItemDefinition shovel;
    public static InventoryItemDefinition lockpicks;
    public static InventoryItemDefinition ammo;
    public static InventoryItemDefinition tomatoes;

    //
    
    public static void Initialize()
    {
        fist = all.New();
        fist.name = "Fist";
        fist.hidden = true;
        fist.stackable = true;
        fist.weaponAction = new WeaponActionDefinition();
        fist.weaponAction.apCost = 1;
        fist.icon = "items/fist".LoadSprite();
        fist.weaponAction.attackAction = (item, enemy) => new GameQueue()
            .Add(new GCSound("sound/combat/swing3"))
            .Add(new GCNarrative($"You swing your fist at the {enemy.definition.name}!"))
            .Add(new GCCombatDealDamage(item, enemy, 25));
        
        money = all.New();
        money.name = "Money";
        money.stackable = true;
        money.icon = "items/money".LoadSprite();
        money.desc = "A stack of weathered, grimy banknotes. In the twisted landscape you find yourself in, their value is uncertain. Perhaps they can still be traded or used as kindling for a fire.";

        // gives you 2 ap
        // doubles incoming damage
        
        Tools();

        CombatConsumables();
        
        Weapons();

        Deliveries();

        Food();

        Drugs();

        Medical();

        Etc();
    }

    static void Etc()
    {
        comic = all.New();
        comic.name = "Comic";
        comic.icon = "items/comic".LoadSprite();
        comic.desc = "A tattered, vintage comic book with fading colors and frayed edges. Lose yourself in a world of imagination and adventure. Restores 30 mental.";
        comic.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You start reading the comic. It's funny and entertaining."))
            .Add(new GCAddStat(EnumPlayerStats.MENTAL, 30, AddStatMode.FLOAT_TEXT_ALERT));

        ambrosia = all.New();
        ambrosia.name = "Ambrosia";
        ambrosia.icon = "items/roses".LoadSprite();
        ambrosia.desc = "A shimmering, golden nectar with an intoxicating scent that seems to lift the very air around it. Eat it.";
        ambrosia.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You eat the amborosia. Warm energy builds up inside of you."))
            .Add(new GCAddStat(EnumPlayerStats.HUNGER, 50, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.MENTAL, 10, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.HEALTH, 50, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.STAMINA,50, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.THIRST, 50, AddStatMode.FLOAT_TEXT_ALERT));

        holyWater = all.New();
        holyWater.name = "Holy Water";
        holyWater.icon = "items/holywater".LoadSprite();
        holyWater.desc = "Looks just like regular water. But you feel some sort of radiance coming off of it...";
        holyWater.weaponAction = new WeaponActionDefinition();
        holyWater.weaponAction.apCost = 0;
        holyWater.weaponAction.attackAction = (item, enemy) => new GameQueue()
            .Add(new GCNarrative($"You splash holy water on the {enemy.definition.name}"))
            .Add(new GCCombatItemConsumed(item))
            .Add(new GCCombatStatusChance(enemy, CombatStatus.VULNERABLE, 1f));
        holyWater.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You take a sip of water! It's cold and refreshing."))
            .Add(new GCAddStat(EnumPlayerStats.THIRST, 10, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.STAMINA, 10, AddStatMode.FLOAT_TEXT_ALERT));
    }

    static void Tools()
    {
        lockpicks = all.New();
        lockpicks.name = "Lockpicks";
        lockpicks.icon = "items/lockpicks".LoadSprite();
        lockpicks.desc = "A set of tools used to pick locks";
        lockpicks.icon = "items/lockpicks".LoadSprite();

        shovel = all.New();
        shovel.name = "Shovel";
        shovel.icon = "items/shovel".LoadSprite();
        shovel.desc = "A sturdy, well-worn shovel with a splintered wooden handle and a rusted metal blade. ";
        shovel.weaponAction = new WeaponActionDefinition();
        shovel.weaponAction.attackAction = (item, enemy) => new GameQueue()
            .Add(new GCSound("sound/combat/swing3"))
            .Add(new GCNarrative($"You swing the shovel at the {enemy.definition.name}!"))
            .Add(new GCCombatDealDamage(item, enemy, 125));
    }

    static void Drugs()
    {
        vape = all.New();
        vape.name = "Vape";
        vape.icon = "items/vape".LoadSprite();
        vape.desc = "Adds 40 mental but reduces 10 health";
        vape.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You take a hit from your vape. It makes you feel better."))
            .Add(new GCAddStat(EnumPlayerStats.MENTAL, 40, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.HEALTH, -10, AddStatMode.FLOAT_TEXT_ALERT));

        cocaine = all.New();
        cocaine.name = "Cocaine";
        cocaine.icon = "items/cocaine".LoadSprite();
        cocaine.desc = "Restores 100 stamina, stamina doesn't deplete for the next 30 minutes, reduces 50 thirst";
        cocaine.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You take a hit of cocaine. Your energy levels skyrocket and you feel unstoppable."))
            .Add(new GCAddStat(EnumPlayerStats.STAMINA, 100, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.THIRST, -50, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStatus(EnumPlayerStatuses.STAMINA_LOCK));

        psychedelics = all.New();
        psychedelics.name = "Psychedelics";
        psychedelics.icon = "items/psychedelics".LoadSprite();
        psychedelics.desc = "Restores 30 mental, increases thirst by 50";
        psychedelics.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You take the psychedelics. Your mind opens up, but you start to feel thirsty."))
            .Add(new GCAddStat(EnumPlayerStats.MENTAL, 30, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.THIRST, 50, AddStatMode.FLOAT_TEXT_ALERT));
    }

    static void Medical()
    {
        bandage = all.New();
        bandage.name = "Bandage";
        bandage.icon = "items/bandage".LoadSprite();
        bandage.desc = "Restores 10 HP and stops bleeding.";
        bandage.stackable = true;
        bandage.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You apply the bandage to your wounds. It stops the bleeding."))
            .Add(new GCAddStat(EnumPlayerStats.HEALTH, 10, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCRemoveStatus(EnumPlayerStatuses.BLEEDING));

        medkit = all.New();
        medkit.name = "Medkit";
        medkit.icon = "items/medkit".LoadSprite();
        medkit.desc = "Restores 10 HP and stops bleeding.";
        medkit.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You apply the bandage to your wounds. It stops the bleeding."))
            .Add(new GCAddStat(EnumPlayerStats.HEALTH, 30, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCRemoveStatus(EnumPlayerStatuses.ALL_NEGATIVE));

        tranquilizers = all.New();
        tranquilizers.name = "Tranquilizers";
        tranquilizers.icon = "items/tranquilizers".LoadSprite();
        tranquilizers.desc = "Restores 40 HP and increases 20 brain, but takes 30 minutes to consume";
        tranquilizers.stackable = true;
        tranquilizers.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You take some tranquilizers. You start feeling relaxed and your wounds heal."))
            .Add(new GCAddStat(EnumPlayerStats.HEALTH, 40, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.MENTAL, 20, AddStatMode.FLOAT_TEXT_ALERT));
    }

    static void Food()
    {
        burger = all.New();
        burger.name = "Burger";
        burger.icon = "items/burger".LoadSprite();
        burger.desc = "A slightly charred, yet appetizing burger with a juicy beef patty nestled between two soft buns. Topped with wilted lettuce and a hint of a mysterious sauce. Surprisingly satisfying in this grim reality. Restores 30 hunger.";
        burger.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You take a bite of the burger. It's juicy and delicious."))
            .Add(new GCAddStat(EnumPlayerStats.HUNGER, 30, AddStatMode.FLOAT_TEXT_ALERT));

        mushrooms = all.New();
        mushrooms.name = "Mushrooms";
        mushrooms.icon = "items/mushrooms".LoadSprite();
        mushrooms.desc = "A handful of earthly-toned, wild mushrooms. Their familiar appearance offers a sense of comfort in this bleak environment.";
        mushrooms.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You eat the mushrooms. Suddenly, you feel a surge of energy coursing through your veins."))
            .Add(new GCAddStat(EnumPlayerStats.STAMINA, 40, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.THIRST, -20, AddStatMode.FLOAT_TEXT_ALERT));

        waterBottle = all.New();
        waterBottle.name = "Water Bottle";
        waterBottle.icon = "items/water".LoadSprite();
        waterBottle.desc = "A simple plastic water bottle filled with clear, untainted water. In this nightmarish world, even the simplest things can be a lifesaver. Restores 20 thirst 20 stamina.";
        waterBottle.stackable = true;
        waterBottle.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You take a sip of water! It's cold and refreshing."))
            .Add(new GCAddStat(EnumPlayerStats.THIRST, 20, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.STAMINA, 20, AddStatMode.FLOAT_TEXT_ALERT));

        apple = all.New();
        apple.name = "Apple";
        apple.icon = "items/apple".LoadSprite();
        apple.desc = "A ripe, red apple, untouched by the malevolence that envelops the world. Its sweet and refreshing taste is a momentary escape from the horrors around you. Restores 15 hunger and 10 thirst.";
        apple.stackable = true;
        apple.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You bite an apple. It's sour but it quenches your hunger a little."))
            .Add(new GCAddStat(EnumPlayerStats.HUNGER, 10, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.THIRST, 10, AddStatMode.FLOAT_TEXT_ALERT));

        tomatoes = all.New();
        tomatoes.name = "Tomatoes";
        tomatoes.icon = "items/tomatoes".LoadSprite();
        tomatoes.desc = "Restores 10 hunger and 15 thirst.";
        tomatoes.stackable = true;
        tomatoes.OnUse = () => new GameQueue()
            .Add(new GCNarrative("This tomato is really juicy."))
            .Add(new GCAddStat(EnumPlayerStats.HUNGER, 10, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.THIRST, 15, AddStatMode.FLOAT_TEXT_ALERT));

        coldPizza = all.New();
        coldPizza.name = "Cold Pizza";
        coldPizza.icon = "items/coldpizza".LoadSprite();
        coldPizza.desc = "Restores 40 hunger";
        coldPizza.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You take a bite of the cold pizza. It's not as good as fresh but it still satisfies your hunger."))
            .Add(new GCAddStat(EnumPlayerStats.HUNGER, 40, AddStatMode.FLOAT_TEXT_ALERT));

        cannedCoffee = all.New();
        cannedCoffee.name = "Canned Coffee";
        cannedCoffee.icon = "items/cannedCoffee".LoadSprite();
        cannedCoffee.desc = "Restores stamina and thirst";
        cannedCoffee.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You take a sip of canned coffee! It's strong and energizing."))
            .Add(new GCAddStat(EnumPlayerStats.STAMINA, 50, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.THIRST, 5, AddStatMode.FLOAT_TEXT_ALERT));

        beer = all.New();
        beer.name = "Beer";
        beer.icon = "items/beer".LoadSprite();
        beer.desc = "A cold beer, it relaxes you but reduces your focus.";
        beer.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You take a sip of beer, it's cold and refreshing."))
            .Add(new GCAddStat(EnumPlayerStats.THIRST, 30, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.STAMINA, 20, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCRemoveStatus(EnumPlayerStatuses.DRUNK));

        energyDrink = all.New();
        energyDrink.name = "Energy Drink";
        energyDrink.icon = "items/energy_drink".LoadSprite();
        energyDrink.desc = "Boosts stamina and reduces hunger";
        energyDrink.stackable = true;
        energyDrink.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You drink an energy drink! It's sweet and gives you a burst of energy."))
            .Add(new GCAddStat(EnumPlayerStats.STAMINA, 70, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.HUNGER, -20, AddStatMode.FLOAT_TEXT_ALERT));

        monsterMeat = all.New();
        monsterMeat.name = "Monster Meat";
        monsterMeat.icon = "items/monstermeat".LoadSprite();
        monsterMeat.desc = "A pile of brownish red flesh oozing with black goo. Who would eat that?\nRestores 50 hunger, reduces 20 mental";
        monsterMeat.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You eat the monster meat. It's filling but makes you feel a bit off."))
            .Add(new GCAddStat(EnumPlayerStats.HUNGER, 50, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.MENTAL, -20, AddStatMode.FLOAT_TEXT_ALERT));

        demonBlood = all.New();
        demonBlood.name = "Demon Blood";
        demonBlood.icon = "items/demon_blood".LoadSprite();
        demonBlood.desc = "Restores 90 thirst, reduces 30 hunger";
        demonBlood.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You drink the demon blood. It's thick and revitalizes you, but it makes you feel less hungry."))
            .Add(new GCAddStat(EnumPlayerStats.THIRST, 90, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.HUNGER, -30, AddStatMode.FLOAT_TEXT_ALERT));
    }

    static void Deliveries()
    {
        roses = all.New();
        roses.name = "Roses";
        roses.icon = "items/roses".LoadSprite();
        roses.desc = "Blood red roses, they smell foul though.";

        suitcase = all.New();
        suitcase.name = "Suitcase";
        suitcase.icon = "items/suitcase".LoadSprite();
        suitcase.desc = "Mysterious suitcase. Has a 6 digit lock on it.";

        shotgun = all.New();
        shotgun.name = "Shotgun";
        shotgun.icon = "items/shotgun".LoadSprite();
        shotgun.desc = "It looks oddly familiar, and tip of the barrel is rusty from blood. There is name of weapon scribed on a side \"salvation\".";

        powder = all.New();
        powder.name = "Powder";
        powder.icon = "items/powder".LoadSprite();
        powder.desc = "Label says it is is medicine for lungs. But oddly you heart crumbles on thousand pieces when you look at this small vile..";

        uniform = all.New();
        uniform.name = "Uniform";
        uniform.icon = "items/uniform".LoadSprite();
        uniform.desc = "It is your old military uniform with medals... They want to display it? Finally some appreciation";

        pills = all.New();
        pills.name = "Pills";
        pills.icon = "items/pills".LoadSprite();
        pills.desc = "About a dozen boxes with some kind of pills. Each pack has an inscription on both sides, on one side \"Exit\" on the other \"No exit\"";
    }

    public static InventoryItemDefinition dust;
    public static InventoryItemDefinition chewingTobacco;
    public static InventoryItemDefinition pills;
    public static InventoryItemDefinition uniform;

    static void CombatConsumables()
    {
        dust = all.New();
        dust.name = "Dust";
        dust.icon = "items/dust".LoadSprite();
        dust.weaponAction = new WeaponActionDefinition();
        dust.weaponAction.apCost = 0;
        dust.desc = "A pile of file grey dust. Looks like ashes, has some fragments of white and black stuff in it. You prefer not to think abot what this might be.";
        dust.weaponAction.attackAction = (item, enemy) => new GameQueue()
            .Add(new GCNarrative($"You throw a pile of dust into {enemy.definition.name}'s eyes!"))
            .Add(new GCCombatStatusChance(enemy, CombatStatus.BLINDED, 1f))
            .Add(new GCCombatDealDamage(item, enemy, 10))
            .Add(new GCCombatItemConsumed(item));
        
        chewingTobacco = all.New();
        chewingTobacco.name = "Chewing Tobacco";
        chewingTobacco.icon = "items/chewingtobacco".LoadSprite();
        chewingTobacco.desc = "A worn pouch of coarse, dried tobacco leaves. When placed in the mouth, the strong, bitter taste provides a brief, but potent distraction from the relentless dread of this twisted world. May temporarily increase focus and reduce anxiety.";
        chewingTobacco.weaponAction = new WeaponActionDefinition();
        chewingTobacco.weaponAction.apCost = 0;
        chewingTobacco.weaponAction.noTarget = true;
        chewingTobacco.weaponAction.attackAction = (item, enemy) => new GameQueue()
            .Add(new GCNarrative($"You chew some tobacco and feel a rush of energy!"))
            .Add(new GCCombatGiveActionPoints(1))
            .Add(new GCCombatItemConsumed(item));

    }
    
    static void Weapons()
    {
        ammo = all.New();
        ammo.name = "9mm Ammo";
        ammo.desc = "A handful of tarnished, yet functional ammunition. Each round serves as a lifeline against the nightmarish creatures that lurk in the shadows, waiting to strike. Essential for keeping your firearms loaded and ready for action.";
        ammo.stackable = true;
        ammo.icon = "items/ammo".LoadSprite();
        
        rustyPipe = all.New();
        rustyPipe.name = "Rusty Pipe";
        rustyPipe.icon = "items/rustypipe".LoadSprite();
        rustyPipe.desc = "A long, hollow metal pipe corroded with rust and time. The dents and scrapes suggest it's been used as a makeshift weapon before. Delivers a crushing blow.";
        rustyPipe.weaponAction = new WeaponActionDefinition();
        rustyPipe.weaponAction.apCost = 2;
        rustyPipe.weaponAction.attackAction = (item, enemy) => new GameQueue()
            .Add(new GCSound("sound/combat/swing3"))
            .Add(new GCNarrative($"You swing a rusty pipe at the {enemy.definition.name}!"))
            .Add(new GCCombatStunChance(enemy, 0.25f))
            .Add(new GCCombatDealDamage(item, enemy, 150));

        plankOfWood = all.New();
        plankOfWood.name = "Plank";
        plankOfWood.icon = "items/plankofwood".LoadSprite();
        plankOfWood.desc = "A plank of wood with a nail sticking out of it. Has traces of blood and black goo.";
        plankOfWood.weaponAction = new WeaponActionDefinition();
        plankOfWood.weaponAction.apCost = 1;
        plankOfWood.weaponAction.missChance = 0.1f;
        plankOfWood.weaponAction.attackAction = (item, enemy) => new GameQueue()
            .Add(new GCSound("sound/combat/swing3"))
            .Add(new GCNarrative($"You swing a wooden plank at the {enemy.definition.name}!"))
            .Add(new GCCombatDealDamage(item, enemy, 100));

        knife = all.New();
        knife.name = "Knife";
        knife.icon = "items/knife".LoadSprite();
        knife.desc = "A worn, but sharp hunting knife with a serrated edge. The blade is stained with an unsettling mix of red and black. An essential tool for survival and close encounters.";
        knife.weaponAction = new WeaponActionDefinition();
        knife.weaponAction.apCost = 1;
        knife.weaponAction.missChance = 0.1f;
        knife.weaponAction.attackAction = (item, enemy) => new GameQueue()
            .Add(new GCNarrative($"You stab {enemy.definition.name} with a knife!"))
            .Add(new GCCombatDealDamage(item, enemy, 100))
            .Add(new GCCombatStatusChance(enemy, CombatStatus.BLEEDING, 0.5f));

        crowbar = all.New();
        crowbar.name = "Crowbar";
        crowbar.icon = "items/crowbar".LoadSprite();
        crowbar.desc = "A heavy, iron crowbar coated in rust and dried blood. The curved end looks like it's been used to pry open more than just doors.";
        crowbar.stackable = true;
        crowbar.weaponAction = new WeaponActionDefinition();
        crowbar.weaponAction.apCost = 1;
        crowbar.weaponAction.attackAction = (item, enemy) => new GameQueue()
            .Add(new GCSound("sound/combat/swing3"))
            .Add(new GCNarrative($"You swing a crowbar at the {enemy.definition.name}!"))
            .Add(new GCCombatDealDamage(item, enemy, 100));

        pistol = all.New();
        pistol.name = "1911";
        pistol.icon = "items/pistol".LoadSprite();
        pistol.desc = "An aged, yet reliable semi-automatic handgun. Its dark steel frame has seen better days, but the weight in your hand brings a sense of security.";
        pistol.stackable = true;
        pistol.weaponAction = new WeaponActionDefinition();
        pistol.weaponAction.defaultSound = "sound/combat/pistol";
        pistol.weaponAction.apCost = 0;
        pistol.weaponAction.ammo = ammo;
        pistol.weaponAction.attackAction = (item, enemy) => new GameQueue()
            .Add(new GCNarrative($"You fire the 1911 at the {enemy.definition.name}!"))
            .Add(new GCCombatDealDamage(item, enemy, 150));

        rifle = all.New();
        rifle.name = "Rifle";
        rifle.icon = "items/rifle".LoadSprite();
        rifle.desc = "A long range weapon that deals significant damage.";
        rifle.weaponAction = new WeaponActionDefinition();
        rifle.weaponAction.apCost = 1;
        rifle.weaponAction.attackAction = (item, enemy) => new GameQueue()
            .Add(new GCNarrative($"You fire your rifle at the {enemy.definition.name}!"))
            .Add(new GCCombatDealDamage(item, enemy, 200));
    }
}

class GCCombatGiveActionPoints : QueueItemBase
{
    readonly int _amount;

    public GCCombatGiveActionPoints(int amount)
    {
        _amount = amount;
    }

    public override void Enter()
    {
        base.Enter();

        Game.world.combat.player.actionPoints += _amount;
        
        Complete();
    }

    public int GetAmount()
    {
        return _amount;
    }
}

class GCCombatItemConsumed : QueueItemBase
{
    readonly InventoryItemDefinition idd;

    public GCCombatItemConsumed(InventoryItemDefinition pileOfDust)
    {
        idd = pileOfDust;
    }

    public override void Enter()
    {
        base.Enter();

        subqueue.Add(new GCAddItem(idd, -1, GiveItemStyle.SILENT));
    }

    public override void Update()
    {
        base.Update();
        
        if(subqueue.IsEmpty())
            Complete();
    }
}

class GCBreakChance : QueueItemBase
{
    readonly InventoryItemDefinition idd;
    readonly float chance_;

    public GCBreakChance(InventoryItemDefinition pileOfDust, float chance)
    {
        idd = pileOfDust;
        chance_ = chance;
    }

    public override void Enter()
    {
        base.Enter();

        if (chance_.Roll())
        {
            subqueue.Add(new GCNarrative($"{idd.name} breaks!"));
            subqueue.Add(new GCAddItem(idd, -1, GiveItemStyle.SILENT));
        }
    }

    public override void Update()
    {
        base.Update();
        
        if(subqueue.IsEmpty())
            Complete();
    }

    public float GetChance()
    {
        return chance_;
    }
}

public class GCCombatStunChance : QueueItemBase
{
    readonly float _chance;
    readonly CombatEnemy _enemy;

    public GCCombatStunChance(CombatEnemy enemy, float f)
    {
        _chance = f;
        _enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        if (_chance.Roll())
        {
            _enemy.AddStatus(CombatStatus.STUNNED);
        }

        Complete();
    }

    public float GetChance()
    {
        return _chance;
    }
}

public class GCCombatStatusChance : QueueItemBase
{
    readonly float _chance;
    readonly CombatEnemy _enemy;
    readonly CombatStatus _status;

    public GCCombatStatusChance(CombatEnemy enemy, CombatStatus status, float f)
    {
        _status = status;
        _chance = f;
        _enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        if (_chance.Roll())
        {
            _enemy.AddStatus(_status);
        }

        Complete();
    }

    public float GetChance()
    {
        return _chance;
    }

    public CombatStatus GetStatus()
    {
        return _status;
    }
}