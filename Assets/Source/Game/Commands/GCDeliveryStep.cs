using Source.Commands;

public class GCDeliveryStep : QueueItemBase
{
    public override void Enter()
    {
        if (Game.world.deliveryIndex == 0)
        {
            // delivery tutorial
            
            Game.contextQueue.Add(new GCAlert("Your goal is to make a delivery."));
            Game.contextQueue.Add(new GCAlert("Your progress is displayed at the bottom of the screen."));
            Game.contextQueue.Add(new GCAlert("'Distance to goal'"));
            Game.contextQueue.Add(new GCAlert("Click 'Walk' to start walking towards the target."));
            Game.contextQueue.Add(new GCCall(() => Game.world.tutorial.highlightWalk = true));
        }
    }
    
    public override void Update()
    {
        if (Game.world.delivery.isReached)
        {
            Complete();
        }
    }
}