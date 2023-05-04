using System;
using System.Collections.Generic;
using Source.Game.Deliveries;
using Source.GameQueue;
using Source.Util;
using UnityEngine;

public enum WalkPace
{
    WALK,
    RUN,
    SPRINT
}

[Serializable]
public class Delivery
{
    public DeliveryDefinition definition;
    public int position;
    public int dangerEncounter;
    
    public bool isScavengable;
    public bool isReached;
    public bool isWalking;

    public void Step(WalkPace walk)
    {
        "sound/footsteps".PlayClip();
        
        if (position < definition.length)
        {
            position += Game.world.player.speed;
            
            ConsumeResources(walk);

            if (position >= definition.length)
            {
                isReached = true;
            }

            isScavengable = definition.scavengingChance.Roll();
            
            Game.contextQueue.Add(new GCAnimateDeliveryProgress());

            if (Game.world.player.GetStat(EnumPlayerStats.TIME) >= definition.dangerTime)
            {
                var dangerLadder = definition.dangerLadder ?? CombatDatabase.dangerLadder;
                dangerEncounter = Mathf.Clamp(dangerEncounter, 0, dangerLadder.Count - 1);
                Game.contextQueue.Add(new GCQueue(dangerLadder[dangerEncounter]()));
                Game.contextQueue.Add(new GCAddStat(EnumPlayerStats.TIME, -100, AddStatMode.SILENT));
                
                dangerEncounter++;
            }

            foreach (var timeline in definition.timeline)
            {
                if (position == timeline.at)
                {
                    Game.contextQueue.Add(new GCQueue(timeline.queue()));
                }
            }
        }
    }

    void ConsumeResources(WalkPace walk)
    {
        switch (walk)
        {
            case WalkPace.WALK:
                Game.contextQueue.Add(new GCAddStat(EnumPlayerStats.HUNGER, -5));
                Game.contextQueue.Add(new GCAddStat(EnumPlayerStats.THIRST, -10));
                Game.contextQueue.Add(new GCAddStat(EnumPlayerStats.STAMINA, -5));
                Game.contextQueue.Add(new GCAddStat(EnumPlayerStats.TIME, 20));
                break;
            case WalkPace.RUN:
                Game.contextQueue.Add(new GCAddStat(EnumPlayerStats.HUNGER, -5));
                Game.contextQueue.Add(new GCAddStat(EnumPlayerStats.THIRST, -15));
                Game.contextQueue.Add(new GCAddStat(EnumPlayerStats.STAMINA, -20));
                Game.contextQueue.Add(new GCAddStat(EnumPlayerStats.TIME, 0));
                break;
            case WalkPace.SPRINT:
                Game.contextQueue.Add(new GCAddStat(EnumPlayerStats.HUNGER, -10));
                Game.contextQueue.Add(new GCAddStat(EnumPlayerStats.THIRST, -25));
                Game.contextQueue.Add(new GCAddStat(EnumPlayerStats.STAMINA, -30));
                Game.contextQueue.Add(new GCAddStat(EnumPlayerStats.TIME, -10));
                Game.contextQueue.Add(new GCAddStat(EnumPlayerStats.MENTAL, 5));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(walk), walk, null);
        }
    }
}

public class GCAnimateDeliveryProgress : QueueItemBase
{
    public override void Enter()
    {
        base.Enter();

        DeliveryUI.Walk();
        Game.world.delivery.isWalking = true;
        subqueue.Add(new GCWait(2f));
    }

    public override void Update()
    {
        base.Update();
        
        if(subqueue.IsEmpty())
            Complete();
    }

    public override void Exit()
    {
        base.Exit();
        
        Game.world.delivery.isWalking = false;
    }
}