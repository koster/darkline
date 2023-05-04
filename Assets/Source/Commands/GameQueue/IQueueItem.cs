namespace Source.GameQueue
{
    public interface IQueueItem
    {
        public void Enter();
        public void Update();
        public void Exit();
        public bool IsComplete();
    }
}