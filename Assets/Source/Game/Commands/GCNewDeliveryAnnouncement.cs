using UnityEngine;

namespace Source.Commands
{
    public class GCNewDeliveryAnnouncement : QueueItemBase
    {
        readonly InventoryItemDefinition item;

        public GCNewDeliveryAnnouncement(InventoryItemDefinition idef)
        {
            item = idef;
        }
        
        public override void Enter()
        {
            UIState.RememberState();
            UIState.DoState(UI_STATES.NOTHING);
            
            subqueue.Add(new GCCall(() => { NewDeliveryUI.Show(item); }));
            subqueue.Add(new GCWait(4f));
            subqueue.Add(new GCCall(NewDeliveryUI.Hide));
            subqueue.Add(new GCWait(1f));
            subqueue.Add(new GCCall(UIState.RestoreState));
            subqueue.Add(new GCCall(Complete));
        }
    }
}