using Source.GameQueue;

public class Game
{
    public static GameWorld world;

    public static GameQueue phases = new GameQueue();
    public static GameQueue contextQueue = new GameQueue();

    public void Start()
    {
        world = new GameWorld();
        world.NewGame();

        phases = new GameQueue();

        StartNextDelivery();
    }

    public static void StartNextDelivery()
    {
        phases.Add(new GCAcceptDelivery());
        phases.Add(new GCDeliveryStep());
        phases.Add(new GCDeliveryComplete());
    }

    public void Tick()
    {
        if (contextQueue.IsNotEmpty())
        {
            contextQueue.Update();
        }
        else
        {
            phases.Update();
        }
    }

    public static void DeliverySetback()
    {
        world.delivery.position -= 5;
    }
}