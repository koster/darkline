using Source.Commands;

public class GCRemoveStatus : QueueItemBase
{
    readonly EnumPlayerStatuses status;

    public GCRemoveStatus(EnumPlayerStatuses bleeding)
    {
        status = bleeding;
    }

    public override void Enter()
    {
        base.Enter();

        if (status == EnumPlayerStatuses.ALL_NEGATIVE)
        {
            TryRemove(EnumPlayerStatuses.DRUNK);
            TryRemove(EnumPlayerStatuses.BLEEDING);
        }
        else if (Game.world.status.Has(status))
        {
            TryRemove(status);
        }
    }

    void TryRemove(EnumPlayerStatuses enumPlayerStatuses)
    {
        subqueue.Add(new GCNarrative($"You are no longer {enumPlayerStatuses}"));
        Game.world.status.Remove(enumPlayerStatuses);
    }

    public override void Update()
    {
        base.Update();
        
        if (subqueue.IsEmpty())
        {
            Complete();
        }
    }
}