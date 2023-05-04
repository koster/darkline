using System;
using Source.GameQueue;
using Source.Util;

[Serializable]
public class Scavenging
{
    public GameQueue Scavenge()
    {
        var q = new GameQueue();
        var scavengingPrice = 10;

        Game.world.delivery.isScavengable = false;
        
        q.Add(new GCAlert("Scavenging..."));
        q.Add(new GCQueue(ScavengingResult()));
        q.Add(new GCAddStat(EnumPlayerStats.TIME, scavengingPrice));

        return q;
    }

    GameQueue ScavengingResult()
    {
        var scavengingResult = ScavengingEventsDatabase.all.GetRandom();
        return scavengingResult();
    }
}