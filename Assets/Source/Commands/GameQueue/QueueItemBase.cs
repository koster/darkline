using Source.GameQueue;

public class QueueItemBase : IQueueItem
{
    protected GameQueue subqueue = new GameQueue();

    bool _complete;

    protected void Complete()
    {
        _complete = true;
    }

    public virtual void Enter()
    {
    }

    public virtual void Update()
    {
        subqueue.Update();
    }

    public virtual void Exit()
    {
    }

    public bool IsComplete()
    {
        return _complete;
    }
}