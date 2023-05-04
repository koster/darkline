using UnityEngine;

class GCWaitForInput : QueueItemBase
{
    public override void Enter()
    {
        base.Enter();

        Debug.Log("enter wait for input");
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0))
        {
            Complete();
        }
    }
}