using System.Collections;
using System.Collections.Generic;

namespace Source.GameQueue
{
    public class GameQueue
    {
        Queue<IQueueItem> queue = new Queue<IQueueItem>();
        IQueueItem currentItem;

        public Queue<IQueueItem> Items => queue;

        public GameQueue Add(IQueueItem qi)
        {
            queue.Enqueue(qi);
            return this;
        }

        public void Update()
        {
            if (currentItem == null)
            {
                if (queue.TryDequeue(out var peekItem))
                {
                    peekItem.Enter();
                    currentItem = peekItem;
                }
            }
            
            if (currentItem != null)
            {
                currentItem.Update();

                if (currentItem?.IsComplete() ?? false)
                {
                    currentItem.Exit();
                    currentItem = null;
                }
            }

        }

        public bool IsNotEmpty()
        {
            return queue.Count > 0 || currentItem != null;
        }
        
        public bool IsEmpty()
        {
            return !IsNotEmpty();
        }

        public void Flush()
        {
            queue.Clear();
            currentItem = null;
        }
    }
}