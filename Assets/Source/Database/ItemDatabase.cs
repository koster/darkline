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
    public Func<GameQueue> OnExamine;
    
    public bool stackable;
    public bool hidden;
    public WeaponActionDefinition weaponAction;

    public bool IsPresent()
    {
        return Game.world.inventory.HasItem(this);
    }

    public string GetStatsLabel()
    {
        var useQueue = OnUse?.Invoke();
        var s = "";
        
        if (useQueue != null)
        {
            var items = useQueue.Items;

            foreach (var i in items)
            {
                if (i is GCAddStat addStat)
                {
                    s += addStat.ToString()+ "\n";
                }
                if (i is GCCombatGiveActionPoints)
                {
                    s += "+1 AP IN COMBAT"+ "\n";
                }
            }
        }

        return s;
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
    public static InventoryItemDefinition letter;
    public static InventoryItemDefinition powder;
    public static InventoryItemDefinition blood;
    
    //
    
    public static InventoryItemDefinition apple;
    public static InventoryItemDefinition ambrosia;
    public static InventoryItemDefinition holyWater;

    //
    public static InventoryItemDefinition bandage;
    public static InventoryItemDefinition waterBottle;
    public static InventoryItemDefinition burger;
    public static InventoryItemDefinition lunch;
    public static InventoryItemDefinition comic;
    public static InventoryItemDefinition cannedCoffee;
    public static InventoryItemDefinition medkit;
    public static InventoryItemDefinition coldPizza;
    public static InventoryItemDefinition beer;
    public static InventoryItemDefinition vape;
    public static InventoryItemDefinition energyDrink;
    public static InventoryItemDefinition tranquilizers;
    public static InventoryItemDefinition painkillers;
    public static InventoryItemDefinition coin;
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

        coin = all.New();
        coin.name = "Coin";
        coin.stackable = true;
        coin.icon = "items/coin".LoadSprite();
        coin.desc = "An unusual and heavy coin, made of an unknown metal that feels both warm and cold to the touch. Emblazoned with a pentagram, the coin exudes an eerie aura that seems to whisper of arcane knowledge and hidden power. Its origins and purpose remain unclear.";

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
        comic.desc = "A tattered, vintage comic book with fading colors and frayed edges. Lose yourself in a world of imagination and adventure.";
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
            .Add(new GCAddStat(EnumPlayerStats.THIRST, 50, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.STAMINA, 25, AddStatMode.FLOAT_TEXT_ALERT));
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
            .Add(new GCCombatStunChance(enemy, 1f))
            .Add(new GCCombatDealDamage(item, enemy, 25));
    }

    static void Drugs()
    {
        vape = all.New();
        vape.name = "Vape";
        vape.icon = "items/vape".LoadSprite();
        vape.desc = "A compact, modern electronic vaporizer emitting a faint, sweet-smelling mist. Its calming effects offer a momentary escape.";
        vape.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You take a hit from your vape. It makes you feel better."))
            .Add(new GCAddStat(EnumPlayerStats.MENTAL, 40, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.STAMINA, 40, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.HEALTH, -10, AddStatMode.FLOAT_TEXT_ALERT));

        cocaine = all.New();
        cocaine.name = "Cocaine";
        cocaine.icon = "items/cocaine".LoadSprite();
        cocaine.desc = "A dangerous, illicit substance known for its potent, short-lived effects. When consumed, it provides a temporary surge of energy and heightened focus, but the consequences can be severe.";
        cocaine.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You take a hit of cocaine. Your energy levels skyrocket and you feel unstoppable."))
            .Add(new GCAddStat(EnumPlayerStats.STAMINA, 100, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.THIRST, -50, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStatus(EnumPlayerStatuses.STAMINA_LOCK));

        psychedelics = all.New();
        psychedelics.name = "Psychedelics";
        psychedelics.icon = "items/psychedelics".LoadSprite();
        psychedelics.desc = "A small bag of colorful, hallucinogenic tablets. In a world that already challenges the boundaries of reality, consuming these may either provide insight or plunge you further into madness. Effects unpredictable.";
        psychedelics.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You take the psychedelics. Your mind opens up, but you start to feel thirsty."))
            .Add(new GCAddStat(EnumPlayerStats.MENTAL, 50, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.THIRST, -50, AddStatMode.FLOAT_TEXT_ALERT));
    }

    static void Medical()
    {
        bandage = all.New();
        bandage.name = "Bandage";
        bandage.icon = "items/bandage".LoadSprite();
        bandage.desc = "A roll of clean, white gauze designed to provide protection and support to wounds and injuries. Having a reliable means to treat cuts, scrapes, and abrasions is essential for survival.";
        bandage.stackable = true;
        bandage.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You apply the bandage to your wounds. It stops the bleeding."))
            .Add(new GCAddStat(EnumPlayerStats.HEALTH, 25, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCRemoveStatus(EnumPlayerStatuses.BLEEDING));

        medkit = all.New();
        medkit.name = "Medkit";
        medkit.icon = "items/medkit".LoadSprite();
        medkit.desc = "A compact, durable case containing essential medical supplies needed to treat a variety of injuries and ailments. Having access to a medkit can be the difference between life and death.";
        medkit.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You apply the bandage to your wounds. It stops the bleeding."))
            .Add(new GCAddStat(EnumPlayerStats.HEALTH, 70, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCRemoveStatus(EnumPlayerStatuses.ALL_NEGATIVE));

        tranquilizers = all.New();
        tranquilizers.name = "Tranquilizers";
        tranquilizers.icon = "items/tranquilizers".LoadSprite();
        tranquilizers.desc = "A small bottle of potent tranquilizer pills. They provide a temporary reprieve from the constant fear and anxiety, allowing for moments of calm.";
        tranquilizers.stackable = true;
        tranquilizers.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You take some tranquilizers. You start feeling relaxed and your wounds heal."))
            .Add(new GCAddStat(EnumPlayerStats.HEALTH, 50, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.MENTAL, 50, AddStatMode.FLOAT_TEXT_ALERT));

        painkillers = all.New();
        painkillers.name = "Painkillers";
        painkillers.icon = "items/painkillers".LoadSprite();
        painkillers.desc = "A bottle of potent pain-relieving tablets. In a place filled with suffering and torment, these small pills provide a fleeting respite from the relentless pain. ";
        painkillers.stackable = true;
        painkillers.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You take some painkillers. You start feeling relaxed and your pain numbs."))
            .Add(new GCAddStat(EnumPlayerStats.HEALTH, 50, AddStatMode.FLOAT_TEXT_ALERT));
    }

    static void Food()
    {
        burger = all.New();
        burger.name = "Burger";
        burger.icon = "items/burger".LoadSprite();
        burger.desc = "A slightly charred, yet appetizing burger with a juicy beef patty nestled between two soft buns. Topped with wilted lettuce and a hint of a mysterious sauce. Surprisingly satisfying in this grim reality.";
        burger.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You take a bite of the burger. It's juicy and delicious."))
            .Add(new GCAddStat(EnumPlayerStats.HUNGER, 50, AddStatMode.FLOAT_TEXT_ALERT));
        
        lunch = all.New();
        lunch.name = "Lunch";
        lunch.icon = "items/lunch".LoadSprite();
        lunch.desc = "A simple, crumpled brown paper bag containing a humble meal. Though unassuming, the familiar flavors bring a sense of normalcy and comfort amidst the chaos. Restores 30 hunger and provides a slight boost to mental well-being.";
        lunch.OnUse = () => new GameQueue()
            .Add(new GCNarrative("Lunch is still warm and tasty."))
            .Add(new GCNarrative("You find a little note inside."))
            .Add(new GCNarrative("\"I love you <3\""))
            .Add(new GCAddStat(EnumPlayerStats.HUNGER, 50, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.MENTAL, 5, AddStatMode.FLOAT_TEXT_ALERT));

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
        waterBottle.desc = "A simple plastic water bottle filled with clear, untainted water. Can be a lifesaver. ";
        waterBottle.stackable = true;
        waterBottle.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You take a sip of water! It's cold and refreshing."))
            .Add(new GCAddStat(EnumPlayerStats.THIRST, 50, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.STAMINA, 25, AddStatMode.FLOAT_TEXT_ALERT));

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
        tomatoes.desc = "A small cluster of ripe, red tomatoes, bursting with juicy flavor and packed with essential nutrients. In the bleak and landscape you're navigating, finding fresh produce like these tomatoes can be a rare and welcome surprise. ";
        tomatoes.stackable = true;
        tomatoes.OnUse = () => new GameQueue()
            .Add(new GCNarrative("This tomato is really juicy."))
            .Add(new GCAddStat(EnumPlayerStats.HUNGER, 10, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.THIRST, 15, AddStatMode.FLOAT_TEXT_ALERT));

        coldPizza = all.New();
        coldPizza.name = "Cold Pizza";
        coldPizza.icon = "items/coldpizza".LoadSprite();
        coldPizza.desc = "A leftover slice of pizza, its once-melty cheese now congealed and its once-crisp crust slightly chewy. Despite the less-than-ideal state it's in, the familiar taste of this cold pizza provides a fleeting moment of comfort and normalcy.";
        coldPizza.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You take a bite of the cold pizza. It's not as good as fresh but it still satisfies your hunger."))
            .Add(new GCAddStat(EnumPlayerStats.HUNGER, 40, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.MENTAL, 5, AddStatMode.FLOAT_TEXT_ALERT));

        cannedCoffee = all.New();
        cannedCoffee.name = "Canned Coffee";
        cannedCoffee.icon = "items/cannedCoffee".LoadSprite();
        cannedCoffee.desc = "A compact, aluminum can filled with a rich, aromatic coffee blend. The familiar taste of this caffeinated beverage provides a much-needed sense of comfort and normalcy.";
        cannedCoffee.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You take a sip of canned coffee! It's strong and energizing."))
            .Add(new GCAddStat(EnumPlayerStats.STAMINA, 50, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.THIRST, 30, AddStatMode.FLOAT_TEXT_ALERT));

        beer = all.New();
        beer.name = "Beer";
        beer.icon = "items/beer".LoadSprite();
        beer.desc = "A chilled bottle of amber liquid, offering a familiar and comforting taste amid the nightmarish surroundings. While indulging in this alcoholic beverage may provide temporary relief from the stress and anxiety of this harrowing world, it also has the potential to impair your judgment, coordination, and reaction time.";
        beer.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You take a sip of beer, it's cold and refreshing."))
            .Add(new GCAddStat(EnumPlayerStats.THIRST, 50, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.STAMINA, 20, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.MENTAL, 20, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCRemoveStatus(EnumPlayerStatuses.DRUNK));

        energyDrink = all.New();
        energyDrink.name = "Energy Drink";
        energyDrink.icon = "items/energydrink".LoadSprite();
        energyDrink.desc = "Boosts stamina and reduces hunger";
        energyDrink.stackable = true;
        energyDrink.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You drink an energy drink! It's sweet and gives you a burst of energy."))
            .Add(new GCAddStat(EnumPlayerStats.THIRST, 35, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.STAMINA, 70, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.HUNGER, -20, AddStatMode.FLOAT_TEXT_ALERT));

        monsterMeat = all.New();
        monsterMeat.name = "Monster Meat";
        monsterMeat.icon = "items/monstermeat".LoadSprite();
        monsterMeat.desc = "A pile of brownish red flesh oozing with black goo. Who would eat that?\n";
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
        suitcase.desc = "An enigmatic, black suitcase with a sleek and sturdy exterior, locked tight with a combination mechanism. Entrusted to you by a solemn officer, the case was given to you with explicit instructions to deliver it, though its contents and purpose remain shrouded in mystery. ";
        suitcase.OnExamine = () => new GameQueue()
            .Add(new GCNarrative("What am I doing with this?"))
            .Add(new GCNarrative("What is that phone?"))
            .Add(new GCNarrative("You don't know, but you're naturally curious."));
        
        shotgun = all.New();
        shotgun.name = "Shotgun";
        shotgun.icon = "items/shotgun".LoadSprite();
        shotgun.desc = "It looks oddly familiar, and tip of the barrel is rusty from blood. There is name of weapon scribed on one side \"Salvation\" and the initials of the owner 'L.R.' on the other.";

        letter = all.New();
        letter.name = "Letter";
        letter.icon = "items/letter".LoadSprite();
        letter.desc = "An unsent, folded letter addressed to Lieutenant Redcliff, a military friend from times past. The earnest words within speak of camaraderie, hope, and an urgent plea to reconsider a tragic decision. The unsent envelope bears witness to the unfortunate timing of its delivery.";
        letter.OnExamine = () => new GameQueue()
            .Add(new GCNarrative("Your response to Red."))
            .Add(new GCNarrative("You never sent it."))
            .Add(new GCNarrative("How could you?"))
            .Add(new GCNarrative("You don't know. You just hope it's not too late."));
        
        powder = all.New();
        powder.name = "Powder";
        powder.icon = "items/powder".LoadSprite();
        powder.desc = "Label says it is is medicine for lungs. But oddly you heart crumbles on thousand pieces when you look at this small vile..";

        medal = all.New();
        medal.name = "Medal of Honor";
        medal.icon = "items/medal".LoadSprite();
        medal.desc = "A once-prestigious military decoration, now dulled and worn from the passage of time. This medal was awarded for acts of valor and bravery in the face of overwhelming odds, but its faded glory now serves as a haunting reminder of the sacrifices made by you and your comrades. Its presence evokes a mix of pride and sorrow, stirring both determination and a sense of loss.";
        medal.OnExamine = () => new GameQueue()
            .Add(new GCNarrative("Your medal of honor."))
            .Add(new GCNarrative("You have mixed feelings about it."))
            .Add(new GCNarrative("On one hand, you feel the recognition."))
            .Add(new GCNarrative("On the other hand, there is no honor in war."));
        
        ring = all.New();
        ring.name = "Golden Ring";
        ring.icon = "items/wedding_ring".LoadSprite();
        ring.desc = "A once-shining wedding band, now slightly tarnished by time and the harrowing experiences you've endured. This ring, unmistakably yours, stirs memories of a love long lost, echoing the whispers of a distant past. Its presence serves as a bittersweet reminder of the love and commitment that still lingers in your heart, fueling your determination to uncover the truth and find solace.";
        ring.OnExamine = () => new GameQueue()
            .Add(new GCNarrative("Your wedding ring."))
            .Add(new GCNarrative("You've decided to give it back to Emily instead of pawning it."))
            .Add(new GCNarrative("It makes some sense in your head. You want closure"));
        
        pills = all.New();
        pills.name = "Pills";
        pills.icon = "items/pills".LoadSprite();
        pills.desc = "About a dozen boxes with some kind of pills. Each pack has an inscription on both sides, on one side \"Exit\" on the other \"No exit\"";
        
        blood = all.New();
        blood.name = "Blood";
        blood.icon = "items/demon_blood".LoadSprite();
        blood.desc = "A small glass vial containing a disturbing mixture of crimson liquid and a viscous black goo. The unsettling realization that this may be your own blood raises questions about how it was collected and the origin of the mysterious black substance.";
        blood.OnExamine = () => new GameQueue()
            .Add(new GCNarrative("A vial of your blood."))
            .Add(new GCNarrative("It was given to you by a field medic after you've had a gunshot wound and passed out."))
            .Add(new GCNarrative("You've kept it, as a reminder of that day, you nearly died."))
            .Add(new GCNarrative("Memento mori."));
    }

    public static InventoryItemDefinition dust;
    public static InventoryItemDefinition chewingTobacco;
    public static InventoryItemDefinition pills;
    public static InventoryItemDefinition medal;
    public static InventoryItemDefinition ring;

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
        
        chewingTobacco.OnUse = () => new GameQueue()
            .Add(new GCNarrative("You chew some tobacco, it's energizing."))
            .Add(new GCAddStat(EnumPlayerStats.MENTAL, 25, AddStatMode.FLOAT_TEXT_ALERT))
            .Add(new GCAddStat(EnumPlayerStats.STAMINA, 50, AddStatMode.FLOAT_TEXT_ALERT));
        
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
        pistol.weaponAction.apCost = 0;
        pistol.weaponAction.ammo = ammo;
        pistol.weaponAction.attackAction = (item, enemy) => new GameQueue()
            .Add(new GCScreenShake(1f))
            .Add(new GCSound("sound/combat/pistol"))
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

class GCScreenShake : QueueItemBase
{
    readonly float _shake;

    public GCScreenShake(float f)
    {
        _shake = f;
    }

    public override void Enter()
    {
        base.Enter();
        GameCamera.PunchShake(_shake);
        Complete();
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