public class GCUIState : QueueItemBase
{
    readonly UI_STATES _uistate;

    public GCUIState(UI_STATES narrativeOnly)
    {
        _uistate = narrativeOnly;
    }

    public override void Enter()
    {
        base.Enter();
        UIState.DoState(_uistate);
        Complete();
    }
}