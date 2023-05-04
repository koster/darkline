using UnityEngine;

namespace Source.Commands
{
    public class GCNarrative : QueueItemBase
    {
        string narrative;
        
        public GCNarrative(string txt)
        {
            narrative = txt;
        }
        
        public override void Enter()
        {
            var textHandle = NarrativeBoxUI.Write(narrative);
            textHandle.onComplete += Complete;
        }
    }
    
    public class GCImage : QueueItemBase
    {
        Sprite narrative;
        bool isInstant;
        
        public GCImage(Sprite txt, bool instant = false)
        {
            isInstant = instant;
            narrative = txt;
        }
        
        public override void Enter()
        {
            NarrativeImageUI.Show(narrative, isInstant);
            
            if (isInstant)
            {
                Complete();
            }
            else
            {
                subqueue.Add(new GCWait(0.5f));
                subqueue.Add(new GCCall(Complete));
            }
        }
    }
    
    public class GCImageHide : QueueItemBase
    {
        public override void Enter()
        {
            NarrativeImageUI.Hide();
            subqueue.Add(new GCWait(1f));
            subqueue.Add(new GCCall(Complete));
        }
    }
}