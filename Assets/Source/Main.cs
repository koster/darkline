using System;
using GameAnalyticsSDK;
using Source.Game.Deliveries;
using TMPro;
using UnityEngine;

public class Main : MonoBehaviour
{
    public Game game;

    void Start()
    {
        GameAnalytics.Initialize();
        
        EnemyDatabase.Initialize();
        ItemDatabase.Initialize();
        ScavengingEventsDatabase.Initialize();
        Story_Atmosphere.Initialize();
        CombatDatabase.Initialize();
        RandomEventsDatabase.Initialize();
        CharacterDatabase.Initialize();
        
        DeliveriesDatabase.Initialize();
        
        game = new Game();
        game.Start();
    }

    void FixedUpdate()
    {
        game.Tick();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Game.world.delivery.position = Game.world.delivery.definition.length - 1;
        }
    }
}