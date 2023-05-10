using GameAnalyticsSDK;
using Source.Commands;
using Source.Game.Deliveries;

public class GCDeliveryComplete : QueueItemBase
{
    public override void Enter()
    {
        base.Enter();

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "delivery_" + Game.world.deliveryIndex);
        Game.world.inventory.TakeAll(Game.world.delivery.definition.item);

        Game.world.inventory.Give(ItemDatabase.coin, 1);
        
        subqueue.Add(new GCQueue(Game.world.delivery.definition.finalPoint?.Invoke()));
        subqueue.Add(new GCDeliveryCompletedAnnouncement(Game.world.deliveryIndex));
        subqueue.Add(new GCQueue(Story_Main.VendingMachine()));
        subqueue.Add(new GCCall(Complete));
    }

    public override void Exit()
    {
        base.Exit();

        Game.StartNextDelivery();
    }
}