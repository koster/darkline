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

        if (Game.world.status.Has(status))
        {
            subqueue.Add(new GCNarrative($"You are no longer {status}"));
            Game.world.status.Remove(status);
        }
        else
        {
            Complete();
        }
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