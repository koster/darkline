class GCUnlockInput : QueueItemBase
{
    public override void Enter()
    {
        base.Enter();
        UILayer.UnblockInput();
        Complete();
    }
}