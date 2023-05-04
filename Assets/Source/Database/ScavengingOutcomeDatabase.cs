using System;
using System.Collections.Generic;
using Source.Game.Deliveries;
using Source.GameQueue;

public static class ScavengingEventsDatabase
{
    public static List<Func<GameQueue>> all = new List<Func<GameQueue>>();

    public static void Initialize()
    {
        // all.Add(FoundNothing);
        // all.Add(FoundAmbush);
        
        all.Add(Story_Scavenging.GasStation);
        all.Add(Story_Scavenging.OldChurch);
        all.Add(Story_Scavenging.RuinedHospital);
        all.Add(Story_Scavenging.DerelictWarehouse);
        all.Add(Story_Scavenging.SewerSystem);
        all.Add(Story_Scavenging.Cemetery);
        all.Add(Story_Scavenging.AbandonedMilitaryBase);
        // all.Add(Story_Scavenging.WorkingShoppingMall);
    }

    static GameQueue FoundAmbush()
    {
        return CombatDatabase.Scavenge_Combat01();
    }

    static Func<GameQueue> Found(InventoryItemDefinition idd)
    {
        return () => new GameQueue()
            .Add(new GCAddItem(idd, 1));
    }

    static GameQueue FoundNothing()
    {
        return new GameQueue()
            .Add(new GCAlert("You found nothing..."));
    }
}