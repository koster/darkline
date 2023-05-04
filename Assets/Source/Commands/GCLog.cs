using UnityEngine;

public class GCLog : QueueItemBase
{
    readonly string msg;

    public GCLog(string hello1)
    {
        msg = hello1;
    }

    public override void Enter()
    {
        base.Enter();
        
        Debug.Log(msg);
        
        Complete();
    }
}