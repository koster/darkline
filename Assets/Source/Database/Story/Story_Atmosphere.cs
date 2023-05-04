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
    
    public static GameQueue FindPlank()
    {
        var q = new GameQueue();

        if (Game.world.inventory.HasItem(ItemDatabase.plankOfWood))
            return q;

        q.Add(new CGUIStateRemember());
        q.Add(new GCUIState(UI_STATES.NARRATIVE_ONLY));

        q.Add(new GCImage("narrative/hobo".LoadSprite()));

        q.Add(new GCNarrative("You encounter a homeless man on the street, his eyes full of wisdom and madness."));
        q.Add(new GCNarrative("He hands you a stick with a nail sticking out of it."));
        q.Add(new GCNarrative("\"This will be more useful to you than that lying phone of yours,\" he mutters."));

        q.Add(new GCNarrative("As you take the stick, he leans in closer and whispers, \"You're already dead, and no one will help you.\""));
        
        q.Add(new GCImage(null));
        
        q.Add(new GCNarrative("The homeless man vanishes as mysteriously as he appeared, leaving you to ponder his ominous message."));

        q.Add(new GCAddItem(ItemDatabase.plankOfWood, 1));
        
        q.Add(new GCImageHide());
        q.Add(new CGUIStateRestore());

        return q;
    }
    
    public static GameQueue FindKnife()
    {
        var q = new GameQueue();

        if (Game.world.inventory.HasItem(ItemDatabase.knife))
            return q;

        q.Add(new GCNarrative("You are walking past a dark alleyway."));
        q.Add(new GCNarrative("It is so dark that you cannot take your eyes off it."));
        q.Add(new GCNarrative("In the middle of the alleyway stands a barely visible figure. The only thing you can clearly see is the knife she is holding to her throat."));

        q.Add(new GCChoices()
            .Add("Stop, don't do it!", (q) =>
            {
            }));
        
        q.Add(new GCNarrative("The figure freezes. What is it about life that makes people cling to it so desperately?"));
        
        q.Add(new GCChoices()
            .Add("Well, I don't really know. But I don't want you to do it, okay?", (q) =>
            {
            })
            .Add("Just give me the knife, I'll need it more.", (q) =>
            {
            }));
        
        q.Add(new GCNarrative("The figure shrugs. \"If that's what you decided, then so be it.\""));
        q.Add(new GCNarrative("When figure comes closer to give you a knife - for a second it seems that he looks exactly like you"));
        q.Add(new GCNarrative("You take the knife in your hand. It's sharp, and it has just missed chance to take a life. It will want another in return."));

        q.Add(new GCAddItem(ItemDatabase.knife, 1));

        return q;
    }
    
    public static GameQueue FindPistol()
    {
        var q = new GameQueue();

        if (Game.world.inventory.HasItem(ItemDatabase.pistol))
        {
            q.Add(new GCAlert("You found some ammo lying on the ground!"));
            q.Add(new GCAddItem(ItemDatabase.ammo, 4));
            return q;
        }

        q.Add(new GCNarrative("At some point, you notice that the fog on the street has become less dense."));
        q.Add(new GCNarrative("With each subsequent delivery, with each thought you pull out of yourself, it weakens."));
        q.Add(new GCNarrative("Something seems to have stirred right at your chest."));

        q.Add(new GCChoices()
            .Add("Swiftly slip your hand under your jacket. Check what's there without getting nervous.", (q) =>
            {
                q.Add(new GCNarrative("Under your jacket, you find your holster. In it, your trusty 1911 is nestled."));
                q.Add(new GCNarrative("Where did it come from? Has it always been here? Did you just forget that you have the strength?"));
                q.Add(new GCNarrative("The holster with the pistol is pleasantly warm. Let them try to stop you now."));
                q.Add(new GCAddItem(ItemDatabase.pistol, 1));
                q.Add(new GCAddItem(ItemDatabase.ammo, 4));
            }));

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