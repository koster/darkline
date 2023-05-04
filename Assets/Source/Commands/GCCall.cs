using System;

public class GCCall : QueueItemBase
{
    readonly Action _act;

    public GCCall(Action endTurn)
    {
        _act = endTurn;
    }

    public override void Enter()
    {
        base.Enter();

        _act?.Invoke();
        Complete();
    }
}