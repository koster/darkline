using System;
using System.Collections.Generic;
using DG.Tweening;
using Source.Game.Deliveries;
using Source.Util;
using UnityEngine;

[Serializable]
public class GameWorld
{
    public int deliveryIndex;

    public Delivery delivery = new Delivery();
    public Combat combat = new Combat();
    public Player player = new Player();
    public Inventory inventory = new Inventory();
    public Scavenging scavenging = new Scavenging();
    public Statuses status = new Statuses();
    public Tutorial tutorial = new Tutorial();
    public bool combatTutorial;

    public void NewGame()
    {
        player.SetStat(EnumPlayerStats.HEALTH, 40, silent: true);
        player.SetStat(EnumPlayerStats.MENTAL, 100, silent: true);
        player.SetStat(EnumPlayerStats.HUNGER, 60, silent: true);
        player.SetStat(EnumPlayerStats.THIRST, 100, silent: true);
        player.SetStat(EnumPlayerStats.STAMINA, 100, silent: true);

        player.SetMaxValue(EnumPlayerStats.HEALTH, 100);
        player.SetMaxValue(EnumPlayerStats.MENTAL, 100);
        player.SetMaxValue(EnumPlayerStats.HUNGER, 100);
        player.SetMaxValue(EnumPlayerStats.THIRST, 100);
        player.SetMaxValue(EnumPlayerStats.STAMINA, 100);

        // all delivery items
        
        inventory.Give(ItemDatabase.suitcase, 1);
        inventory.Give(ItemDatabase.ring, 1);
        inventory.Give(ItemDatabase.letter, 1);
        inventory.Give(ItemDatabase.medal, 1);
        inventory.Give(ItemDatabase.blood, 1);

        inventory.Give(ItemDatabase.waterBottle, 2);
        inventory.Give(ItemDatabase.painkillers, 1);
        inventory.Give(ItemDatabase.coin, 1);

        inventory.Give(ItemDatabase.fist, 1);

        UIState.DoState(UI_STATES.NOTHING);
        Game.world.deliveryIndex = 0;
        
        Game.contextQueue.Add(new GCQueue(Story_Main.Intro()));
    }
}

public class Tutorial
{
    public bool highlightWalk;
    public bool highlightBackpack;
}

public class GCSound : QueueItemBase
{
    readonly string _sound;
    float _vol;

    public GCSound(string sfx, float vol = 1f)
    {
        _vol = vol;
        _sound = sfx;
    }

    public override void Enter()
    {
        base.Enter();

        _sound.PlayClip(_vol);
        Complete();
    }
}

public class GCBackgroundMusic : QueueItemBase
{
    static Dictionary<string, AudioSource> _sources = new Dictionary<string, AudioSource>();

    readonly string _sound;
    float _vol;

    AudioSource source;

    public GCBackgroundMusic(string sfx, float vol = 1f)
    {
        _vol = vol;
        _sound = sfx;

        if (_sources.ContainsKey(sfx))
        {
            source = _sources[sfx];
        }
        else
        {
            var go = new GameObject(sfx);
            var addComponent = go.AddComponent<AudioSource>();
            addComponent.volume = 0;
            _sources.Add(sfx, addComponent);
            source = addComponent;
            source.clip = _sound.Load<AudioClip>();
        }
    }

    public override void Enter()
    {
        base.Enter();

        if (!source.isPlaying)
        {
            source.time = 0;
            source.Play();
        }
        
        source.DOFade(_vol, 1f).OnComplete(() =>
        {
            if (_vol == 0)
                source.Stop();
        });

        Complete();
    }
}