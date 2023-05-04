using System;
using Source.Game.Deliveries;

[Serializable]
public class GameWorld
{
    public int deliveryIndex;

    public Delivery delivery = new Delivery();
    public Combat combat = new Combat();
    public Player player = new Player();
    public Inventory inventory = new Inventory();
    public Scavenging scavenging = new Scavenging();
    public Statuses status = new Statuses();
    public Tutorial tutorial = new Tutorial();
    public bool combatTutorial;

    public void NewGame()
    {
        player.SetStat(EnumPlayerStats.HEALTH, 100, silent: true);
        player.SetStat(EnumPlayerStats.MENTAL, 100, silent: true);
        player.SetStat(EnumPlayerStats.HUNGER, 20, silent: true);
        player.SetStat(EnumPlayerStats.THIRST, 100, silent: true);
        player.SetStat(EnumPlayerStats.STAMINA, 100, silent: true);

        player.SetMaxValue(EnumPlayerStats.HEALTH, 100);
        player.SetMaxValue(EnumPlayerStats.MENTAL, 100);
        player.SetMaxValue(EnumPlayerStats.HUNGER, 100);
        player.SetMaxValue(EnumPlayerStats.THIRST, 100);
        player.SetMaxValue(EnumPlayerStats.STAMINA, 100);

        inventory.Give(ItemDatabase.waterBottle, 2);
        inventory.Give(ItemDatabase.burger, 2);

        inventory.Give(ItemDatabase.fist, 1);

        // inventory.Give(ItemDatabase.rustyPipe, 1);
        // inventory.Give(ItemDatabase.plankOfWood, 1);
        // inventory.Give(ItemDatabase.knife, 1);
        // inventory.Give(ItemDatabase.holyWater, 1);

        // inventory.Give(ItemDatabase.dust, 1);

        // inventory.Give(ItemDatabase.fist, 1);
        // inventory.Give(ItemDatabase.rustyPipe, 1);
        // inventory.Give(ItemDatabase.plankOfWood, 1);
        // inventory.Give(ItemDatabase.pistol, 1);
        // inventory.Give(ItemDatabase.rifle, 1);
        // inventory.Give(ItemDatabase.pileOfDust, 1);
        // inventory.Give(ItemDatabase.chewingTobacco, 1);

        // intro +
        // 0     + n/a/ 
        // 1     + wooden plank
        // 2     + rusty pipe
        // 3     + knife
        // 4     + pistol
        // out   ?

        UIState.DoState(UI_STATES.NOTHING);

        // Game.world.deliveryIndex = 1;

        // #if UNITY_EDITOR
        //
        // Game.contextQueue.Add(new GCAcceptDelivery());
        // Game.contextQueue.Add(new GCAcceptDelivery());
        //
        // #endif
        
        Game.contextQueue.Add(new GCQueue(Story_Main.Intro()));

        // Game.contextQueue.Add(new GCQueue(Story_Deliveries.Delivery_04()));
        // Game.contextQueue.Add(new GCQueue(Story_Atmosphere.FindKnife()));
        // Game.contextQueue.Add(new GCQueue(CombatDatabase.Danger_CombatBleeder()));
        // Game.contextQueue.Add(new GCQueue(CombatDatabase.TestCombat()));
        // Game.contextQueue.Add(new GCQueue(Story_Introduction.HomelessMan_Event()));
        // Game.contextQueue.Add(new GCQueue(Story_Deliveries.Final_Delivery02()));
    }
}

public class Tutorial
{
    public bool highlightWalk;
    public bool highlightBackpack;
}

public class GCSound : QueueItemBase
{
    readonly string s;

    public GCSound(string soundCreepyAmbient)
    {
        s = soundCreepyAmbient;
    }

    public override void Enter()
    {
        base.Enter();

        s.PlayClip();
        Complete();
    }
}