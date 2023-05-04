class GCLockInput : QueueItemBase
{
    public override void Enter()
    {
        base.Enter();
        UILayer.BlockInput();
        Complete();
    }
}