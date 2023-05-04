using GameAnalyticsSDK;
using Source.Commands;

public class GCDeliveryComplete : QueueItemBase
{
    public override void Enter()
    {
        base.Enter();

        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "delivery_" + Game.world.deliveryIndex);
        Game.world.inventory.TakeAll(Game.world.delivery.definition.item);

        subqueue.Add(new GCQueue(Game.world.delivery.definition.finalPoint.queue()));
        subqueue.Add(new GCDeliveryCompletedAnnouncement());
        subqueue.Add(new GCCall(() => { Game.world.deliveryIndex++; }));
        subqueue.Add(new GCCall(Complete));
    }

    public override void Exit()
    {
        base.Exit();

        Game.StartNextDelivery();
    }
}