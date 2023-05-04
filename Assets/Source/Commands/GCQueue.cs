using Source.GameQueue;

public class GCQueue : QueueItemBase
{
    public GCQueue(GameQueue timelineQueue)
    {
        subqueue = timelineQueue;
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