using UnityEngine;

namespace Source.Commands
{
    public class GCDeliveryCompletedAnnouncement : QueueItemBase
    {
        public override void Enter()
        {
            UIState.DoState(UI_STATES.NOTHING);
            
            subqueue.Add(new GCCall(() => { DeliveryCompletedUI.Show(global::Game.world.deliveryIndex); }));
            subqueue.Add(new GCWait(3f));
            subqueue.Add(new GCCall(DeliveryCompletedUI.Hide));
            subqueue.Add(new GCWait(1f));
            subqueue.Add(new GCAddItem(ItemDatabase.ambrosia, 1));
            subqueue.Add(new GCCall(Complete));
        }
    }
}