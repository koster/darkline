using Source.Game.Deliveries;
using UnityEngine;

public class Main : MonoBehaviour
{
    public Game game;

    void Start()
    {
        EnemyDatabase.Initialize();
        ItemDatabase.Initialize();
        ScavengingEventsDatabase.Initialize();
        Story_Atmosphere.Initialize();
        CombatDatabase.Initialize();
        RandomEventsDatabase.Initialize();
        
        DeliveriesDatabase.Initialize();
        
        game = new Game();
        game.Start();
    }

    void FixedUpdate()
    {
        game.Tick();
    }
}