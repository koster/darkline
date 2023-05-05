using System;
using System.Collections.Generic;
using Source.Commands;
using Source.Game.Deliveries;
using Source.GameQueue;
using Source.Util;

public static class Story_Atmosphere
{
    public static List<Func<GameQueue>> all = new List<Func<GameQueue>>();

    public static void Initialize()
    {
        all.Add(SomeoneIsLooking);
        all.Add(YouHearANoise);
        all.Add(AShadowyFigure);
        all.Add(MysteriousVoice);
        all.Add(ADeadBird);
        all.Add(Footsteps);
    }
    
    public static GameQueue SomeoneIsLooking()
    {
        var q = new GameQueue();
        q.Add(new GCAlert("You feel like someone is watching you..."));
        q.Add(new GCAddStat(EnumPlayerStats.MENTAL, -10, AddStatMode.FLOAT_TEXT_ALERT));
        return q;
    }
    
    public static GameQueue Footsteps()
    {
        var q = new GameQueue();
        q.Add(new GCAlert("You hear footsteps behind you, but there's no one there."));
        q.Add(new GCAddStat(EnumPlayerStats.MENTAL, -10, AddStatMode.FLOAT_TEXT_ALERT));
        return q;
    }
    
    public static GameQueue Blood()
    {
        var q = new GameQueue();
        q.Add(new GCAlert("You see blood splatters on the pavement."));
        q.Add(new GCAlert("It makes you feel uneasy."));
        q.Add(new GCAddStat(EnumPlayerStats.MENTAL, -10, AddStatMode.FLOAT_TEXT_ALERT));
        return q;
    }
    
    public static GameQueue YouHearANoise()
    {
        var q = new GameQueue();
        q.Add(new GCAlert("You hear an unsettling noise..."));
        q.Add(new GCAddStat(EnumPlayerStats.MENTAL, -5, AddStatMode.FLOAT_TEXT_ALERT));
        return q;
    }
    
    public static GameQueue AShadowyFigure()
    {
        var q = new GameQueue();
        q.Add(new GCAlert("You see a glimpse of a shadowy figure..."));
        q.Add(new GCAddStat(EnumPlayerStats.MENTAL, -10, AddStatMode.FLOAT_TEXT_ALERT));
        return q;
    }
    
    public static GameQueue MysteriousVoice()
    {
        var q = new GameQueue();
        q.Add(new GCAlert("You hear a mysterious voice calling out to you..."));
        q.Add(new GCAddStat(EnumPlayerStats.MENTAL, -5, AddStatMode.FLOAT_TEXT_ALERT));
        return q;
    }
    
    public static GameQueue ADeadBird()
    {
        var q = new GameQueue();
        q.Add(new GCAlert("You found a dead bird on your path..."));
        q.Add(new GCAddStat(EnumPlayerStats.MENTAL, -5, AddStatMode.FLOAT_TEXT_ALERT));
        return q;
    }

    public static GameQueue BloodTracks()
    {
        var q = new GameQueue();

        q.Add(new GCAlert("You stumble upon bloody tracks on the asphalt!"));

        q.Add(new GCChoices()
            .Add("Follow the tracks", (q) =>
            {
                q.Add(new GCNarrative("You decide to follow the tracks, curiosity driving you forward."));
                q.Add(new GCNarrative("The tracks lead you to a mutilated corpse, a gruesome sight."));
                q.Add(new GCNarrative("Despite the horror, you find some valuable loot on the body."));
                q.Add(new GCAddItem(ItemDatabase.bandage, 1));
            })
            .Add("Ignore the tracks", (q) =>
            {
                q.Add(new GCNarrative("You choose to ignore the tracks, unwilling to face whatever lies ahead."));
                q.Add(new GCNarrative("However, the chilling sight takes a toll on your mental state."));
                q.Add(new GCAddStat(EnumPlayerStats.MENTAL, -15));
            }));

        return q;
    }

    public static GameQueue CreepyFeelingIncreases()
    {
        var q = new GameQueue();
        q.Add(new GCAlert("You feel as if the fog is closing in on you..."));
        q.Add(new GCAddStat(EnumPlayerStats.MENTAL, -5, AddStatMode.FLOAT_TEXT_ALERT));
        q.Add(new GCAddStat(EnumPlayerStats.TIME, 100, AddStatMode.SILENT));
        return q;
    }

    public static GameQueue FoundDust()
    {
        var q = new GameQueue();
        
        q.Add(new GCAlert("You found some dust on the ground"));
        q.Add(new GCAlert("You decided to keep some in your pocket"));
        q.Add(new GCAddItem(ItemDatabase.dust, 1));
        q.Add(new GCNarrative("While scrubbing the dust of the pavement you've found a manhole under it."));
        q.Add(new GCNarrative("You've tried to step away but a black hand grabbed your leg."));
        q.Add(new GCNarrative("You started screaming and kicking the hand with all your force."));
        q.Add(new GCNarrative("But a second later, you've realized that it's just a rusty pipe."));
        q.Add(new GCAddItem(ItemDatabase.rustyPipe, 1));
        
        return q;
    }

    public static GameQueue FindHolyWater()
    {
        var q = new GameQueue();
        
        q.Add(new GCAlert("You stumble upon an abandoned church"));
        q.Add(new GCNarrative("There is no one in there."));
        q.Add(new GCNarrative("Looks like there was a funeral."));
        q.Add(new GCNarrative("The casket is empty"));
        q.Add(new GCNarrative("You find a flask of holy water inside of it."));
        q.Add(new GCAddItem(ItemDatabase.holyWater, 1));
        
        return q;
    }

    public static GameQueue DisgustingSmell()
    {
        var q = new GameQueue();
        q.Add(new GCAlert("You smell a disgusting sewer odor."));
        q.Add(new GCAddStat(EnumPlayerStats.MENTAL, -10, AddStatMode.FLOAT_TEXT_ALERT));
        return q;
    }

    public static GameQueue Dogs()
    {
        var q = new GameQueue();
        q.Add(new GCAlert("You hear dogs barking... They sound hungry."));
        q.Add(new GCAddStat(EnumPlayerStats.MENTAL, -10, AddStatMode.FLOAT_TEXT_ALERT));
        return q;
    }
}