using System;
using Source.Util;
using UnityEditor;

public enum AddStatMode
{
    SILENT,
    FLOAT_TEXT,
    FLOAT_TEXT_ALERT
}

public class GCAddStat : QueueItemBase
{
    EnumPlayerStats stat;
    int dlt;
    AddStatMode md;

    public GCAddStat(EnumPlayerStats statPlayer, int delta, AddStatMode mode = AddStatMode.FLOAT_TEXT)
    {
        stat = statPlayer;
        dlt = delta;
        md = mode;
    }
    
    public override void Enter()
    {
        base.Enter();

        Game.world.player.AddStat(stat, dlt);
        
        switch (md)
        {
            case AddStatMode.SILENT:
                break;
            case AddStatMode.FLOAT_TEXT:
                break;
            case AddStatMode.FLOAT_TEXT_ALERT:
                subqueue.Add(new GCAlert(ToString()));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (Game.world.player.GetStat(stat) <= 0)
        {
            if (stat == EnumPlayerStats.HEALTH)
            {
                subqueue.Add(new GCGameOver());
            }
            else
            {
                subqueue.Add(new GCAddStat(EnumPlayerStats.HEALTH, Game.world.player.GetStat(stat)));
                Game.world.player.SetStat(stat, 0);
            }
        }
    }

    public override void Update()
    {
        base.Update();

        if (subqueue.IsEmpty())
            Complete();
    }

    public override string ToString()
    {
        return StringUtl.Signed(dlt) + " " + stat;
    }
}