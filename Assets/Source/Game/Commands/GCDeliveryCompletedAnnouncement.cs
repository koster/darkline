using UnityEngine;

namespace Source.Commands
{
    public class GCDeliveryCompletedAnnouncement : QueueItemBase
    {
        readonly int index_;

        public GCDeliveryCompletedAnnouncement(int index)
        {
            index_ = index;
        }
        
        public override void Enter()
        {
            UIState.DoState(UI_STATES.NOTHING);
            
            subqueue.Add(new GCCall(() => { DeliveryCompletedUI.Show(index_); }));
            subqueue.Add(new GCWait(3f));
            subqueue.Add(new GCCall(DeliveryCompletedUI.Hide));
            subqueue.Add(new GCCall(() => { global::Game.world.deliveryIndex++; }));
            subqueue.Add(new GCWait(1f));
            subqueue.Add(new GCCall(Complete));
        }
    }
}