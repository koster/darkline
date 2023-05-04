using UnityEngine.SceneManagement;

namespace Source.Game.Deliveries
{
    public class GCLoadScene : QueueItemBase
    {
        readonly string scn;

        public GCLoadScene(string creditsScreen)
        {
            scn = creditsScreen;
        }

        public override void Enter()
        {
            base.Enter();

            SceneManager.LoadScene(scn);
            Complete();
        }
    }
}