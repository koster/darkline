class GCAlert : QueueItemBase
{
    readonly string _msg;
    AlertUI _alert;

    public GCAlert(string hello1)
    {
        _msg = hello1;
    }

    public override void Enter()
    {
        base.Enter();
        
        _alert = AlertUI.Show(_msg);
        _alert.OnHideCallback += OnAlertHidden;
    }

    void OnAlertHidden()
    {
        Complete();
    }

    public override void Exit()
    {
        _alert.OnHideCallback -= OnAlertHidden;
        
        base.Exit();
    }
}