using GameAnalyticsSDK;
using Source.Commands;

public class GCGameOver : QueueItemBase
{
    public override void Enter()
    {
        base.Enter();

        GameAnalytics.NewDesignEvent("game_over");

        Game.contextQueue.Flush();

        UIState.DoState(UI_STATES.NARRATIVE_ONLY);
        Game.contextQueue.Add(new GCImage(null));
        Game.contextQueue.Add(new GCNarrative("You see a flash of light..."));
        Game.contextQueue.Add(new GCNarrative("You find yourself somewhere you've been before..."));
        Game.contextQueue.Add(new GCRestart());
        
        Complete();
    }
}

public class GCRestart : QueueItemBase
{
    public override void Enter()
    {
        base.Enter();

        Game.world.status.Reset();
        
        Game.world.player.SetStat(EnumPlayerStats.HEALTH, 50, silent: true);
        Game.world.player.SetStat(EnumPlayerStats.MENTAL, 50, silent: true);
        Game.world.player.SetStat(EnumPlayerStats.HUNGER, 50, silent: true);
        Game.world.player.SetStat(EnumPlayerStats.STAMINA, 100, silent: true);
        Game.world.player.SetStat(EnumPlayerStats.THIRST, 100, silent: true);
        Game.world.player.SetStat(EnumPlayerStats.TIME, 0, silent: true);

        subqueue.Add(new GCCall(Game.DeliverySetback));
        subqueue.Add(new GCImageHide());
        subqueue.Add(new GCUIState(UI_STATES.DELIVERY));
    }

    public override void Update()
    {
        base.Update();
        
        if (subqueue.IsEmpty())
            Complete();
    }
}