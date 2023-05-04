class GCShowInventory : QueueItemBase
{
    public override void Enter()
    {
        base.Enter();

        var model = new InventoryModel();
        model.FromInventory(Game.world.inventory);
        model.onHide += Complete;
        model.queue = subqueue;
        InventoryUI.i.Show(model);
    }
}