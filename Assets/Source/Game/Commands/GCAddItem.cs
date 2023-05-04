using Source.Util;

public enum GiveItemStyle
{
    SILENT,
    ALERT
}

public class GCAddItem : QueueItemBase
{
    readonly InventoryItemDefinition item;
    readonly int amount;
    readonly GiveItemStyle stl;

    public GCAddItem(GiveItem gi, GiveItemStyle style = GiveItemStyle.ALERT)
    {
        item = gi.def;
        amount = gi.amount;
        stl = style;
    }
    
    public GCAddItem(InventoryItemDefinition itm, int i, GiveItemStyle style = GiveItemStyle.ALERT)
    {
        stl = style;
        item = itm;
        amount = i;
    }

    public override void Enter()
    {
        base.Enter();
        Game.world.inventory.Give(item, amount);

        if (amount > 0)
        {
            if (stl == GiveItemStyle.ALERT)
            {
                subqueue.Add(new GCAlert($"You found {item.name}!"));
            }
        }
        else
        {
            if (stl == GiveItemStyle.ALERT)
            {
                subqueue.Add(new GCAlert($"You lost {amount.Abs()} {item.name}!"));
            }
        }
    }

    public override void Update()
    {
        base.Update();
        if (subqueue.IsEmpty())
            Complete();
    }
}