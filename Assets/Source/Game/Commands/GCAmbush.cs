using Source.GameQueue;

public class GCAmbush : QueueItemBase
{
    readonly Combat _cmbt;

    public GCAmbush(Combat combat)
    {
        _cmbt = combat;
    }

    public override void Enter()
    {
        base.Enter();

        Game.world.combat = _cmbt;
        subqueue.Add(Game.world.combat);

        if (_cmbt.Loot != null)
            subqueue.Add(new GCQueue(_cmbt.Loot()));
        else
            subqueue.Add(new GCAddItem(ScavengingItems.GetDefaultCombatLoot()));
    }

    public override void Update()
    {
        base.Update();

        if (subqueue.IsEmpty())
            Complete();
    }

    public override void Exit()
    {
        base.Exit();
    }
}