using System;
using Source.Commands;
using Source.GameQueue;

public static class Story_Tutorial
{
    public static GameQueue Hunger()
    {
        var q = new GameQueue();
        q.Add(new CGUIStateRemember());
        q.Add(new GCUIState(UI_STATES.NARRATIVE_PLUS_STATS));
        q.Add(new GCNarrative("You feel so hungry..."));
        q.Add(new GCNarrative("It's unsettling..."));
        q.Add(new GCNarrative("When was the last time you had something to eat?"));
        q.Add(new GCNarrative("Maybe there's something in your backback?"));
        q.Add(new GCCall(() => Game.world.tutorial.highlightBackpack = true));
        q.Add(new CGUIStateRestore());
        return q;
    }
}

public class CGUIStateRemember : QueueItemBase
{
    public override void Enter()
    {
        base.Enter();
        UIState.RememberState();
        Complete();
    }
}
public class CGUIStateRestore : QueueItemBase
{
    public override void Enter()
    {
        base.Enter();
        UIState.RestoreState();
        Complete();
    }
}