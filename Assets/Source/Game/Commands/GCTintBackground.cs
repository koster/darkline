using UnityEngine;

namespace Source.Game.Deliveries
{
    public class GCTintBackground : QueueItemBase
    {
        readonly Color color;
        readonly float time;

        public GCTintBackground(Color clr, float time)
        {
            this.time = time;
            this.color = clr;
        }

        public override void Enter()
        {
            base.Enter();

            NarrativeImageUI.Tint(color, time);

            if (time==0)
                subqueue.Add(new GCWait(time));
            subqueue.Add(new GCCall(Complete));

        }

        public override void Update()
        {
            base.Update();

            if (subqueue.IsEmpty())
                Complete();
        }
    }
}