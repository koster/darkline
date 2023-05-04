using System;
using System.Collections.Generic;

public enum EnumPlayerStats
{
    HEALTH,
    HUNGER,
    MENTAL,
    STAMINA,
    THIRST,
    TIME
}

[Serializable]
public class PlayerStat
{
    public EnumPlayerStats stat;
    public int value;
    public int maxValue = -1;
}

[Serializable]
public class Player
{
    public List<PlayerStat> stats = new List<PlayerStat>();
    public int speed = 1;

    public int GetStat(EnumPlayerStats stat)
    {
        foreach (var ps in stats)
        {
            if (ps.stat == stat)
            {
                return ps.value;
            }
        }

        return 0;
    }

    public void SetStat(EnumPlayerStats stat, int value, bool silent = false)
    {
        AddStat(stat, value - GetStat(stat), silent);
    }

    public void SetMaxValue(EnumPlayerStats stat, int value)
    {
        AddStat(stat, 0);
        foreach (var ps in stats)
        {
            if (ps.stat == stat)
            {
                ps.maxValue = value;
            }
        }
    }

    public void AddStat(EnumPlayerStats stat, int delta, bool silent = false)
    {
        foreach (var ps in stats)
        {
            if (ps.stat == stat)
            {
                ps.value += delta;

                if (ps.value > ps.maxValue && ps.maxValue > 0)
                    ps.value = ps.maxValue;

                if (!silent)
                    StatView.NotifyChanged(stat, delta);
                return;
            }
        }

        stats.Add(new PlayerStat() { stat = stat, value = delta });
    }

    public int GetMaxAP()
    {
        return 2;
    }
}