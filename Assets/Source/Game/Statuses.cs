using System;
using System.Collections.Generic;

public enum EnumPlayerStatuses
{
    NONE,
    BLEEDING,
    DRUNK,
    ALL_NEGATIVE,
    STAMINA_LOCK
}

[Serializable]
public class Statuses
{
    public List<EnumPlayerStatuses> list = new List<EnumPlayerStatuses>();

    public void Remove(EnumPlayerStatuses status)
    {
        list.Remove(status);
    }

    public void Add(EnumPlayerStatuses status)
    {
        list.Add(status);
    }

    public bool Has(EnumPlayerStatuses status)
    {
        return list.Contains(status);
    }

    public void Reset()
    {
        list.Clear();
    }
}