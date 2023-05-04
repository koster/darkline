using GameAnalyticsSDK;
using Source.Commands;
using Source.Game.Deliveries;

public class GCAcceptDelivery : QueueItemBase
{
    public override void Enter()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "delivery_" + Game.world.deliveryIndex);

        var currentDelivery = DeliveriesDatabase.all[Game.world.deliveryIndex];
        
        Game.world.delivery = new Delivery
        {
            definition = currentDelivery,
            position = 0,
        };
        
        Game.world.player.SetStat(EnumPlayerStats.TIME, 0);

        if (!Game.world.inventory.HasItem(currentDelivery.item))
            Game.world.inventory.Give(currentDelivery.item, 1);
        
        subqueue.Add(new GCNewDeliveryAnnouncement(currentDelivery.item));
        subqueue.Add(new GCCall(Complete));
    }

    public override void Exit()
    {
        base.Exit();
        UIState.DoState(UI_STATES.DELIVERY);
    }
}