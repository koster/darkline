public class GCAddStatus : QueueItemBase
{
    readonly EnumPlayerStatuses status;

    public GCAddStatus(EnumPlayerStatuses bleeding)
    {
        status = bleeding;
    }

    public override void Enter()
    {
        base.Enter();

        if (!Game.world.status.Has(status))
        {
            subqueue.Add(new GCAlert("You are " + status + "!"));
            Game.world.status.Add(status);
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