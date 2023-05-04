using UnityEngine;

public class GCWait : QueueItemBase
{
    float _wait;

    public GCWait(float f)
    {
        _wait = f;
    }

    public override void Update()
    {
        _wait -= Time.deltaTime;

        if (_wait < 0)
        {
            Complete();
        }
    }
}